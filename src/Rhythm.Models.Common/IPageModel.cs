namespace Rhythm.Models.Common;

/// <summary>
/// An interface for creating a page.
/// </summary>
public interface IPageModel : IHaveComponents
{
    /// <summary>
    /// Gets the page content.
    /// </summary>
    IReadOnlyCollection<IComponentModel> Content { get; }
}
