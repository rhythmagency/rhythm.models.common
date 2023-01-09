namespace Rhythm.Models.Common.Tests;

using NSubstitute;

public class HaveComponents_FlattenTests
{
    [Test]
    public void Given_HaveComponentsModel_That_Returns_An_Empty_Collection_Then_Flatten_Should_Return_An_Empty_Collection()
    {
        // Arrange
        var model = Substitute.For<IHaveComponents>();
        model.GetComponents().Returns(x => Array.Empty<IComponentModel>());

        // Act 
        var flattened = model.Flatten();

        // Assert
        Assert.That(flattened, Is.Empty);
    }

    [Test]
    public void Given_HaveComponentsModel_That_Returns_Default_Then_Flatten_Should_Return_An_Empty_Collection()
    {
        // Arrange
        var model = Substitute.For<IHaveComponents>();
        model.GetComponents().Returns(x => default);

        // Act 
        var flattened = model.Flatten();

        // Assert
        Assert.That(flattened, Is.Empty);
    }

    [Test]
    public void Given_HaveComponentsModel_That_Returns_A_Collection_Of_Defaults_Then_Flatten_Should_Return_An_Empty_Collection()
    {
        // Arrange
        var model = Substitute.For<IHaveComponents>();
        model.GetComponents().Returns(x => new IComponentModel?[]
        {
            default,
            default,
            default,
        });

        // Act 
        var flattened = model.Flatten();

        // Assert
        Assert.That(flattened, Is.Empty);
    }

    [Test]
    public void Given_HaveComponentsModel_That_Returns_An_Array_Then_Flatten_Should_Return_Expected_Results()
    {
        // Arrange
        var subComponents = new List<IComponentModel?>()
        {
            null,
            Substitute.For<IComponentModel>(),
            Substitute.For<IComponentModel>(),
        };

        var subComponent = Substitute.For<IComponentModel, IHaveComponents>();
        ((IHaveComponents)subComponent).GetComponents().Returns(subComponents);

        var components = new List<IComponentModel?>()
        {
            null,
            Substitute.For<IComponentModel>(),
            Substitute.For<IComponentModel>(),
            Substitute.For<IComponentModel>(),
            Substitute.For<IComponentModel>(),
            subComponent
        };

        var model = Substitute.For<IHaveComponents>();
        model.GetComponents().Returns(x => components);

        // Act 
        var flattened = model.Flatten();

        // Assert
        // Expecting count of 6 as components has 6 items and the sub component has 3 but the sub component
        // is not configured to return itself from its GetComponents() call and nulls are ignored.
        Assert.That(flattened, Has.Count.EqualTo(6));
        Assert.That(flattened, Does.Not.Contains(null));
    }
}
