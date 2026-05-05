using Robust.Client.UserInterface.RichText;

namespace Content.Goobstation.UIKit._Maid.UserInterface.RichText;

public sealed class FancyBorderTag : IMarkupTagHandler
{
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;

    public const string TagName = "fancyborder";
    public string Name => TagName;
}
