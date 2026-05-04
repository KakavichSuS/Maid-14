using System.Numerics;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._Maid;

[RegisterComponent]
public sealed partial class HateEngineComponent : Component
{
    [DataField]
    public TimeSpan SpawnTime;

    [DataField]
    public bool Acceleration = false;

    [DataField]
    public Vector2 InitialVelocity;

    [DataField("playingMusic")]
    public SoundSpecifier HateEngine = new SoundCollectionSpecifier("HateEngine");

    [DataField]
    public SoundSpecifier GibEngine = new SoundCollectionSpecifier("GibEngine");
}
