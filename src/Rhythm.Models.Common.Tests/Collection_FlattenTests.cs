namespace Rhythm.Models.Common.Tests;

using NSubstitute;

public class Collection_FlattenTests
{
    [Test]
    public void Given_A_Null_Collection_Flatten_Should_Return_An_Empty_Collection()
    {
        // Arrange
        IPageComponentModel[]? collection = null;

        // Act 
        var flattened = collection.Flatten();

        // Assert
        Assert.That(flattened, Is.Empty);
    }

    [Test]
    public void Given_A_Collection_With_Sub_Components_Then_Flatten_Should_Return_Expected_Results()
    {
        // Arrange
        var subComponents = new List<IPageComponentModel>()
        {
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
        };

        var subComponent = Substitute.For<IPageComponentModel, IHavePageComponents>();
        ((IHavePageComponents)subComponent).GetPageComponents().Returns(subComponents);

        var components = new List<IPageComponentModel>()
        {
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            subComponent
        };

        // Act 
        var flattened = components.Flatten();

        // Assert
        // Expecting count of 6 as components has 6 items and the sub component has 3 but the sub component
        // is not configured to return itself from its GetPageComponents() call.
        Assert.That(flattened, Has.Count.EqualTo(6));
        Assert.That(flattened, Does.Not.Contains(null));
    }
}
