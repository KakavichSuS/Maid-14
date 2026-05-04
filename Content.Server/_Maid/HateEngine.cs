using System.Numerics;
using Content.Server.Body.Components;
using Content.Server.Body.Systems;
using Content.Shared.Eye;
using Content.Shared.Physics;
using Robust.Server.GameObjects;
using Robust.Server.GameStates;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Events;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server._Maid
{
    public sealed class HateEngine : EntitySystem
    {
        [Dependency] private readonly PvsOverrideSystem _pvs = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;
        [Dependency] private readonly SharedTransformSystem _xform = default!;
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly PhysicsSystem _physics = default!;
        [Dependency] private readonly BodySystem _body = default!;
        [Dependency] private readonly VisibilitySystem _visibility = default!;
        [Dependency] private readonly SharedEyeSystem _eye = default!;
        [Dependency] private readonly IGameTiming _timing = default!;

        private const int VelMultiplier = 25;
        private const float DespawnDelaySeconds = 15f;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<HateEngineComponent, StartCollideEvent>(ThomasCrashEvent);
            SubscribeLocalEvent<HateEngineComponent, ComponentStartup>(OnStartup);
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            var query = EntityQueryEnumerator<HateEngineComponent>();
            while (query.MoveNext(out var uid, out var engine))
            {
                if (engine.SpawnTime != TimeSpan.Zero)
                {
                    var elapsed = _timing.CurTime - engine.SpawnTime;
                    if (elapsed.TotalSeconds >= DespawnDelaySeconds)
                    {
                        Del(uid);
                        continue;
                    }
                }

                if (!engine.Acceleration)
                    continue;
                if (!TryComp<PhysicsComponent>(uid, out var phys))
                    continue;
                _physics.SetLinearVelocity(uid, engine.InitialVelocity, body: phys);
            }
        }

        private void OnStartup(EntityUid uid, HateEngineComponent comp, ComponentStartup args)
        {
            if (!comp.Acceleration)
                return;
            _pvs.AddGlobalOverride(uid);
            _audio.PlayPvs(comp.HateEngine, uid, AudioParams.Default.WithMaxDistance(125f));
        }

        private void ThomasCrashEvent(EntityUid uid, HateEngineComponent comp, StartCollideEvent args)
        {
            var player = args.OtherEntity;
            var coords = Transform(player).Coordinates;

            if (!HasComp<HateEngineTargetComponent>(player))
                return;

            var organs = _body.GetBodyOrganEntityComps<TransformComponent>(args.OtherEntity);
            foreach (var organ in organs)
            {
                if (HasComp<BrainComponent>(organ.Owner))
                    Del(organ);
            }

            _body.GibBody(player);
            _audio.PlayPvs(comp.GibEngine, coords);
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
                    _physics.SetCollisionLayer(uid, id, fixture, currentLayer | (int) CollisionGroup.DoorPassable,
                        playerFixtures, playerPhysics);

                    var currentMask = fixture.CollisionMask;
                    _physics.SetCollisionMask(uid, id, fixture, currentMask | (int) CollisionGroup.DoorPassable,
                        playerFixtures, playerPhysics);
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
            // Braindamage code below
            var direction = _random.Next(1, 9);
            var offset = direction switch
            {
                1 => new Vector2(-125, 0), 2 => new Vector2(125, 0),
                3 => new Vector2(0, -125), 4 => new Vector2(0, 125),
                5 => new Vector2(-125, -125), 6 => new Vector2(125, -125),
                7 => new Vector2(-125, 125), 8 => new Vector2(125, 125),
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
                    _physics.SetCollisionLayer(train, id, fixture, (int) CollisionGroup.DoorPassable,
                        trainFixtures, trainPhysics);
                    _physics.SetCollisionMask(train, id, fixture, (int) CollisionGroup.DoorPassable,
                        trainFixtures, trainPhysics);
                    _physics.SetHard(train, fixture, false, trainFixtures);
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

            if (TryComp<PhysicsComponent>(train, out var phys))
            {
                if (TryComp<HateEngineComponent>(train, out var comp))
                {
                    var velocity = directionToPlayer.Normalized() * VelMultiplier; // ~5 seconds to die
                    comp.InitialVelocity = velocity;
                    _physics.SetLinearVelocity(train, velocity, body: phys);
                    comp.SpawnTime = _timing.CurTime;
                }
            }
        }
    }
}
