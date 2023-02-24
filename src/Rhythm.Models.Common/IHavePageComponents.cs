namespace Rhythm.Models.Common;

using System.Collections.Generic;

/// <summary>
/// A contract for classes that have page components.
/// </summary>
public interface IHavePageComponents : IHavePageComponents<IPageComponentModel>
{
}

/// <summary>
/// A contract for classes that have page components.
/// </summary>
/// <typeparam name="TComponent">The type of <see cref="IPageComponentModel" />.</typeparam>
public interface IHavePageComponents<TComponent> where TComponent : class, IPageComponentModel
{
    /// <summary>
    /// Gets the page components of this class.
    /// </summary>
    /// <returns>A collection of page components</returns>
    /// <remarks>If this class is also a <see cref="IPageComponentModel"/> and should be returned by <see cref="FlattenExtensions.Flatten(IEnumerable{IPageComponentModel})"/> or <see cref="FlattenExtensions.Flatten(IHavePageComponents?)"/> then it should be included here.</remarks>
    IReadOnlyCollection<TComponent> GetPageComponents();
}