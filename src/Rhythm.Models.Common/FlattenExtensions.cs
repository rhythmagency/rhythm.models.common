namespace Rhythm.Models.Common;

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// A collection of extension methods that work with the <see cref="IHavePageComponents"/> and <see cref="IPageComponentModel"/> interfaces to return flattened collections of <see cref="IPageComponentModel"/> classes.
/// </summary>
public static class FlattenExtensions
{
    /// <summary>
    /// Flattens the current <see cref="IHavePageComponents{TComponent}" /> into a <see cref="IReadOnlyCollection{TComponent}" />.
    /// </summary>
    /// <param name="model">The current model.</param>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <returns>A read only collection of <typeparamref name="TComponent"/>.</returns>
    public static IReadOnlyCollection<TComponent> Flatten<TComponent>(this IHavePageComponents<TComponent>? model) where TComponent : class, IPageComponentModel
    {
        if (model is null)
        {
            return Array.Empty<TComponent>();
        }

        return model.GetPageComponents().Flatten().FinalizeFlatten();
    }

    /// <summary>
    /// Flattens the current <see cref="IHavePageComponents" /> into a <see cref="IReadOnlyCollection{IPageComponentModel}" />.
    /// </summary>
    /// <param name="model">The current model.</param>
    /// <returns>A read only collection of <see cref="IPageComponentModel"/>.</returns>
    public static IReadOnlyCollection<IPageComponentModel> Flatten(this IHavePageComponents? model)
    {
        return model.Flatten<IPageComponentModel>();
    }

    /// <summary>
    /// Flattens the current <see cref="IEnumerable{TComponent}" /> into a <see cref="IReadOnlyCollection{TComponent}" />.
    /// </summary>
    /// <param name="pageComponents">The current page components.</param>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <returns>A read only collection of <typeparamref name="TComponent"/>.</returns>
    public static IReadOnlyCollection<TComponent> Flatten<TComponent>(this IEnumerable<TComponent>? pageComponents) where TComponent : class, IPageComponentModel
    {
        if (pageComponents is null)
        {
            return Array.Empty<TComponent>();
        }

        var list = new List<TComponent>();

        foreach (var component in pageComponents)
        {
            switch (component)
            {
                case IHavePageComponents<TComponent> container:
                    list.AddRange(container.Flatten());
                    break;
                default:
                    list.Add(component); 
                    break;
            }
        }

        return list.FinalizeFlatten();
    }

    /// <summary>
    /// Flattens the current <see cref="IEnumerable{IPageComponentModel}" /> into a <see cref="IReadOnlyCollection{IPageComponentModel}" />.
    /// </summary>
    /// <param name="pageComponents">The current page components.</param>
    /// <returns>A read only collection of <see cref="IPageComponentModel" />.</returns>
    public static IReadOnlyCollection<IPageComponentModel> Flatten(this IEnumerable<IPageComponentModel>? pageComponents)
    {
        return pageComponents.Flatten<IPageComponentModel>();
    }

    /// <summary>
    /// An internal function for finalizing the public Flatten methods.
    /// </summary>
    /// <param name="pageComponents">The page components.</param>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <returns>A read only collection of <see cref="IPageComponentModel" />.</returns>
    private static IReadOnlyCollection<TComponent> FinalizeFlatten<TComponent>(this IEnumerable<TComponent?> pageComponents) where TComponent : class, IPageComponentModel
    {
        var list = new List<TComponent>();

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
