namespace Rhythm.Models.Common;

/// <summary>
/// An interface for creating a page.
/// </summary>
public interface IPageModel : IHavePageComponents
{
    /// <summary>
    /// Gets the page content.
    /// </summary>
    IReadOnlyCollection<IPageComponentModel> Content { get; }
}
