namespace Rhythm.Models.Common;

public interface IPageModel : IHaveComponents
{
    IReadOnlyCollection<IBlockModel> Blocks { get; }
}
