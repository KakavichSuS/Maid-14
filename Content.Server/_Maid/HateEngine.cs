using System.Numerics;
using Content.Server._Maid.TTS;
using Content.Server.Body.Systems;
using Content.Shared.Eye;
using Robust.Server.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Events;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server._Maid
{
    public sealed class HateEngine: EntitySystem
    {
        [Dependency] private readonly SharedAudioSystem _audio = default!;
        [Dependency] private readonly SharedTransformSystem _xform = default!;
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly IMapManager _mapManager = default!;
        [Dependency] private readonly PhysicsSystem _physics = default!;
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        [Dependency] private readonly BodySystem _body = default!;
        [Dependency] private readonly VisibilitySystem _visibility = default!;
        [Dependency] private readonly SharedEyeSystem _eye = default!;

        public override void Initialize()
        {
            base.Initialize();

            // SubscribeLocalEvent<HateEngineComponent, MapInitEvent>(OnMapInit);
            SubscribeLocalEvent<HateEngineComponent, StartCollideEvent>(ThomasCrashEvent);
        }

        // public override void Update(float frameTime)
        // {
        //     base.Update(frameTime);
        //     var curTime = _gameTiming.CurTime;
        //     var query = EntityQueryEnumerator<HateEngineComponent>();
        //     while (query.MoveNext(out var uid, out var timer))
        //     {
        //         var source = timer.NextUpdate + curTime;
        //         if (source >= timer.Goodbye)
        //         {
        //             TryComp<HateEngineTargetComponent>()
        //         }
        //     }
        // }


        // private void OnMapInit(EntityUid uid, HateEngineComponent component, MapInitEvent args)
        // {
        //     if (TryComp(uid, out PhysicsComponent? phys))
        //     {
        //         _physics.SetLinearDamping(uid, phys, 0f);
        //         _physics.SetFriction(uid, phys, 0f);
        //         _physics.SetBodyStatus(uid, phys, BodyStatus.InAir);
        //
        //         var xform = Transform(uid);
        //         var (worldPos, worldRot) = _xform.GetWorldPositionRotation(uid);
        //         var vel = worldRot.ToWorldVec() * component.MaxSpeed;
        //
        //         _physics.ApplyLinearImpulse(uid, vel, body: phys);
        //         xform.LocalRotation = (vel - worldPos).ToWorldAngle() + MathHelper.PiOver2;
        //     }
        // }

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

            var train = SpawnAtPosition("AirCanister", spawnPos);

            if (TryComp<VisibilityComponent>(train, out var visibility))
            {
                _visibility.SetLayer(train, (ushort)VisibilityFlags.Ghost);
            }

            var playerWorldPos = coords.Position;
            var trainWorldPos = spawnPos.Position;
            var directionToPlayer = playerWorldPos - trainWorldPos;

            if (!(directionToPlayer.LengthSquared() > 0))
                return;
            var angle = directionToPlayer.ToAngle();

            if (TryComp<TransformComponent>(train, out var transform))
            {
                transform.LocalRotation = angle;
            }

            if (!TryComp<PhysicsComponent>(train, out var phys))
                return;
            var velocity = directionToPlayer.Normalized() * 500;
            _physics.ApplyLinearImpulse(train, velocity, body: phys);
        }
    }
}
