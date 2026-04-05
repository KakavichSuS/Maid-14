using System.Numerics;
using Robust.Shared.Prototypes;

namespace Content.Server._Maid;

[RegisterComponent]
public sealed partial class HateEngineComponent : Component
{
    [DataField]
    public bool Acceleration = false;

    [DataField]
    public Vector2 InitialVelocity;
}
