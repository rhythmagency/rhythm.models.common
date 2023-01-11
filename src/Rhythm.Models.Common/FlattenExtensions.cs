namespace Rhythm.Models.Common;

using System.Collections.Generic;

/// <summary>
/// A collection of extension methods that work with the <see cref="IHavePageComponents"/> and <see cref="IPageComponentModel"/> interfaces to return flattened collections of <see cref="IPageComponentModel"/> classes.
/// </summary>
public static class FlattenExtensions
{
    /// <summary>
    /// Flattens the current <see cref="IHavePageComponents" /> into a <see cref="IReadOnlyCollection{IComponentModel}" />.
    /// </summary>
    /// <param name="model">The current model.</param>
    /// <returns>A read only collection of <see cref="IPageComponentModel" />.</returns>
    public static IReadOnlyCollection<IPageComponentModel> Flatten(this IHavePageComponents? model)
    {
        if (model is null)
        {
            return Array.Empty<IPageComponentModel>();
        }

        return model.GetPageComponents().Flatten().FinalizeFlatten();
    }

    /// <summary>
    /// Flattens the current <see cref="IEnumerable{IPageComponentModel}" /> into a <see cref="IReadOnlyCollection{IPageComponentModel}" />.
    /// </summary>
    /// <param name="pageComponents">The current page components.</param>
    /// <returns>A read only collection of <see cref="IPageComponentModel" />.</returns>
    public static IReadOnlyCollection<IPageComponentModel> Flatten(this IEnumerable<IPageComponentModel?>? pageComponents)
    {
        if (pageComponents is null)
        {
            return Array.Empty<IPageComponentModel>();
        }

        var list = new List<IPageComponentModel?>();

        foreach (var component in pageComponents)
        {
            switch (component)
            {
                case IHavePageComponents container:
                    list.AddRange(container.Flatten());
                    break;
                default:
                    list.Add(component); break;
            }
        }

        return list.FinalizeFlatten();
    }

    /// <summary>
    /// An internal function for finalizing the public Flatten methods.
    /// </summary>
    /// <param name="pageComponents">The page components.</param>
    /// <returns>A read only collection of <see cref="IPageComponentModel" />.</returns>
    private static IReadOnlyCollection<IPageComponentModel> FinalizeFlatten(this IEnumerable<IPageComponentModel?> pageComponents)
    {
        var list = new List<IPageComponentModel>();

        foreach (var component in pageComponents)
        {
            if (component is null)
            {
                continue;
            }
            
            list.Add(component);
        }

        return list.ToArray();
    }
}
