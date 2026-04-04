using Robust.Shared.Prototypes;

namespace Content.Server._Maid;

[RegisterComponent]
public sealed partial class HateEngineComponent: Component
{
    [DataField]
    public Angle Rotation;

    [DataField]
    public Direction Direction;


    [DataField]
    public TimeSpan NextUpdate = TimeSpan.Zero;


    [DataField]
    public TimeSpan Goodbye = TimeSpan.FromSeconds(5.0);
}
