namespace Rhythm.Models.Common;

using System.Collections.Generic;

/// <summary>
/// A contract for classes that have components.
/// </summary>
public interface IHaveComponents
{
    /// <summary>
    /// Gets the components of this class.
    /// </summary>
    /// <returns>A collection of components</returns>
    /// <remarks>If this class is also a <see cref="IComponentModel"/> and should be returned by <see cref="FlattenExtensions.Flatten(IEnumerable{IComponentModel})"/> or <see cref="FlattenExtensions.Flatten(IHaveComponents?)"/> then it should be included here.</remarks>
    IReadOnlyCollection<IComponentModel?>? GetComponents();
}
