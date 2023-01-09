namespace Rhythm.Models.Common;

using System.Collections.Generic;

/// <summary>
/// A collection of extension methods that work with the <see cref="IHaveComponents"/> and <see cref="IComponentModel"/> interfaces to return flattened collections of <see cref="IComponentModel"/> classes.
/// </summary>
public static class FlattenExtensions
{
    /// <summary>
    /// Flattens the current <see cref="IHaveComponents" /> into a <see cref="IReadOnlyCollection{IComponentModel}" />.
    /// </summary>
    /// <param name="model">The current model.</param>
    /// <returns>A read only collection of <see cref="IComponentModel" />.</returns>
    public static IReadOnlyCollection<IComponentModel> Flatten(this IHaveComponents? model)
    {
        if (model is null)
        {
            return Array.Empty<IComponentModel>();
        }

        return model.GetComponents().Flatten().FinalizeFlatten();
    }

    /// <summary>
    /// Flattens the current <see cref="IEnumerable{IComponentModel}" /> into a <see cref="IReadOnlyCollection{IComponentModel}" />.
    /// </summary>
    /// <param name="model">The current components.</param>
    /// <returns>A read only collection of <see cref="IComponentModel" />.</returns>
    public static IReadOnlyCollection<IComponentModel> Flatten(this IEnumerable<IComponentModel?>? components)
    {
        if (components is null)
        {
            return Array.Empty<IComponentModel>();
        }

        var list = new List<IComponentModel?>();

        foreach (var component in components)
        {
            switch (component)
            {
                case IHaveComponents container:
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
    /// <param name="components">The components.</param>
    /// <returns>A read only collection of <see cref="IComponentModel" />.</returns>
    private static IReadOnlyCollection<IComponentModel> FinalizeFlatten(this IEnumerable<IComponentModel?> components)
    {
        var list = new List<IComponentModel>();

        foreach (var component in components)
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
