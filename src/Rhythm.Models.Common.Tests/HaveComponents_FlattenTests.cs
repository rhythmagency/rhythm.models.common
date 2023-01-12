namespace Rhythm.Models.Common.Tests;

using NSubstitute;

public class HaveComponents_FlattenTests
{
    [Test]
    public void Given_HaveComponentsModel_That_Returns_An_Empty_Collection_Then_Flatten_Should_Return_An_Empty_Collection()
    {
        // Arrange
        var model = Substitute.For<IHavePageComponents>();
        model.GetPageComponents().Returns(x => Array.Empty<IPageComponentModel>());

        // Act 
        var flattened = model.Flatten();

        // Assert
        Assert.That(flattened, Is.Empty);
    }

    [Test]
    public void Given_HaveComponentsModel_That_Returns_An_Array_Then_Flatten_Should_Return_Expected_Results()
    {
        // Arrange
        var subPageComponents = new List<IPageComponentModel>()
        {
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
        };

        var subPageComponent = Substitute.For<IPageComponentModel, IHavePageComponents>();
        ((IHavePageComponents)subPageComponent).GetPageComponents().Returns(subPageComponents);

        var pageComponents = new List<IPageComponentModel>()
        {
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            Substitute.For<IPageComponentModel>(),
            subPageComponent
        };

        var model = Substitute.For<IHavePageComponents>();
        model.GetPageComponents().Returns(x => pageComponents);

        // Act 
        var flattened = model.Flatten();

        // Assert
        // Expecting count of 6 as components has 6 items and the sub component has 3 but the sub page component
        // is not configured to return itself from its GetPageComponents() call.
        Assert.That(flattened, Has.Count.EqualTo(6));
        Assert.That(flattened, Does.Not.Contains(null));
    }
}
