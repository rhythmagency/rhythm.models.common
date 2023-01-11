namespace Rhythm.Models.Common;

public interface IPageModel : IHaveComponents
{
    IReadOnlyCollection<IComponentModel> Content { get; }
}
