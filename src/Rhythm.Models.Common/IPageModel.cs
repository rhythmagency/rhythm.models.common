namespace Rhythm.Models.Common;

/// <summary>
/// An interface for creating a page.
/// </summary>
public interface IPageModel : IPageModel<IPageComponentModel>
{
}

/// <summary>
/// An interface for creating a page.
/// </summary>
/// <typeparam name="TComponent">The type of <see cref="IPageComponentModel" />.</typeparam>
public interface IPageModel<TComponent> : IHavePageComponents<TComponent> where TComponent : class, IPageComponentModel
{
    /// <summary>
    /// Gets the page content.
    /// </summary>
    IReadOnlyCollection<TComponent> Content { get; }
}