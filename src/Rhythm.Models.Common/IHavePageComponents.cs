namespace Rhythm.Models.Common;

using System.Collections.Generic;

/// <summary>
/// A contract for classes that have page components.
/// </summary>
public interface IHavePageComponents
{
    /// <summary>
    /// Gets the page components of this class.
    /// </summary>
    /// <returns>A collection of page components</returns>
    /// <remarks>If this class is also a <see cref="IPageComponentModel"/> and should be returned by <see cref="FlattenExtensions.Flatten(IEnumerable{IPageComponentModel})"/> or <see cref="FlattenExtensions.Flatten(IHavePageComponents?)"/> then it should be included here.</remarks>
    IReadOnlyCollection<IPageComponentModel> GetPageComponents();
}
