using System.Numerics;
using Content.Server._Maid.TTS;
using Content.Server.Body.Systems;
using Content.Shared.Eye;
using Content.Shared.Physics;
using Robust.Server.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Events;
using Robust.Shared.Physics.Systems;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server._Maid
{
    public sealed class HateEngine : EntitySystem
    {
        [Dependency] private readonly SharedAudioSystem _audio = default!;
        [Dependency] private readonly SharedTransformSystem _xform = default!;
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly IMapManager _mapManager = default!;
        [Dependency] private readonly FixtureSystem _fixtureSystem = default!;
        [Dependency] private readonly PhysicsSystem _physics = default!;
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        [Dependency] private readonly BodySystem _body = default!;
        [Dependency] private readonly VisibilitySystem _visibility = default!;
        [Dependency] private readonly SharedEyeSystem _eye = default!;

        const int velMultiplier = 25;

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            var query = EntityQueryEnumerator<HateEngineComponent>();
            while (query.MoveNext(out var uid, out var engine))
            {
                if (!engine.Acceleration)
                    continue;
                if (!TryComp<PhysicsComponent>(uid, out var phys))
                    return;
                _physics.SetLinearVelocity(uid, engine.InitialVelocity, body: phys);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<HateEngineComponent, StartCollideEvent>(ThomasCrashEvent);
        }

        private void ThomasCrashEvent(EntityUid uid, HateEngineComponent component, StartCollideEvent args)
        {
            var ent = args.OtherEntity;

            if (!HasComp<HateEngineTargetComponent>(ent))
                return;
            _body.GibBody(ent);
            Del(args.OurEntity);
        }

        public void OnHateEngineCall(EntityUid uid)
        {
            var playerPos = _xform.ToCoordinates(_xform.GetMapCoordinates(uid));

            if (TryComp<FixturesComponent>(uid, out var playerFixtures) &&
                TryComp<PhysicsComponent>(uid, out var playerPhysics))
            {
                foreach (var (id, fixture) in playerFixtures.Fixtures)
                {
                    var currentLayer = fixture.CollisionLayer;
                    _physics.SetCollisionLayer(uid,
                        id,
                        fixture,
                        currentLayer | (int) CollisionGroup.DoorPassable,
                        playerFixtures,
                        playerPhysics);

                    var currentMask = fixture.CollisionMask;
                    _physics.SetCollisionMask(uid,
                        id,
                        fixture,
                        currentMask | (int) CollisionGroup.DoorPassable,
                        playerFixtures,
                        playerPhysics);
                }
            }

            if (TryComp<EyeComponent>(uid, out var eye))
            {
                _eye.SetVisibilityMask(uid, eye.VisibilityMask | (int) VisibilityFlags.Ghost, eye);
            }

            _audio.PlayEntity("/Audio/Effects/pop_high.ogg", uid, uid);
            TrainSpawn(uid, playerPos);
        }

        private void TrainSpawn(EntityUid uid, EntityCoordinates coords)
        {
            var direction = _random.Next(1, 5);

            var offset = direction switch
            {
                1 => new Vector2(-100, 0),
                2 => new Vector2(100, 0),
                3 => new Vector2(0, -100),
                4 => new Vector2(0, 100),
                _ => Vector2.Zero
            };

            var spawnPos = coords.Offset(offset);

            var train = SpawnAtPosition("Thomas", spawnPos);

            if (TryComp<VisibilityComponent>(train, out var visibility))
            {
                _visibility.SetLayer(train, (ushort) VisibilityFlags.Ghost);
            }

            if (TryComp<FixturesComponent>(train, out var trainFixtures) &&
                TryComp<PhysicsComponent>(train, out var trainPhysics))
            {
                foreach (var (id, fixture) in trainFixtures.Fixtures)
                {
                    _physics.SetCollisionLayer(train,
                        id,
                        fixture,
                        (int) CollisionGroup.DoorPassable,
                        trainFixtures,
                        trainPhysics);
                    _physics.SetCollisionMask(train,
                        id,
                        fixture,
                        (int) CollisionGroup.DoorPassable,
                        trainFixtures,
                        trainPhysics);
                    _physics.SetHard(train, fixture, true, trainFixtures);
                }

                _physics.SetBodyType(train, BodyType.KinematicController, trainFixtures, trainPhysics);
                _physics.SetFixedRotation(train, false, true, trainFixtures, trainPhysics);
                _physics.SetBodyStatus(train, trainPhysics, BodyStatus.InAir, true);

                _physics.SetLinearDamping(train, trainPhysics, 0f);
                _physics.SetAngularDamping(train, trainPhysics, 0f);
            }

            var playerWorldPos = coords.Position;
            var trainWorldPos = spawnPos.Position;
            var directionToPlayer = playerWorldPos - trainWorldPos;
            if (directionToPlayer.LengthSquared() > 0)
            {
                var angle = directionToPlayer.ToAngle();
                angle += MathHelper.PiOver2;
                _xform.SetLocalRotation(train, angle);
            }

            // Импульс
            if (TryComp<PhysicsComponent>(train, out var phys))
            {
                if (TryComp<HateEngineComponent>(train, out var comp))
                {
                    var velocity = directionToPlayer.Normalized() * velMultiplier; // ~5 seconds to die
                    comp.InitialVelocity = velocity;
                    _physics.SetLinearVelocity(train, velocity, body: phys);
                }
            }
        }
    }
}
