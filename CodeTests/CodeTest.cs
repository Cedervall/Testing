using System;
using System.Linq;

namespace CodeTests;

public class CodeTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ILayerTwo _layerTwoMock = Substitute.For<ILayerTwo>();

    public CodeTest(WebApplicationFactory<Program> factory)
    {
        _layerTwoMock.Add(Arg.Any<GeneralDto>()).Returns(true);

        _factory = factory;
        _factory = factory.WithWebHostBuilder(c => c.ConfigureTestServices(services =>
        {
            services.AddSingleton(_layerTwoMock);
        }));
    }

    [Fact]
    public void Test1()
    {
        // Arrange
        var generalDto = new GeneralDto();
        var layer = _factory.Services.GetService<ILayerOne>();
        // Act
        layer.GenerateDto();
        // Assert
        _layerTwoMock.Received().Add(Arg.Any<GeneralDto>());
        _layerTwoMock.Received()
            .Add(Arg.Is<GeneralDto>(x => AssertionVerifier.Verify(() => Test(x, generalDto))));
    }

    private void Test(GeneralDto x, GeneralDto generalDto)
    {
        x.Should().BeEquivalentTo(generalDto);
    }
}

public static class AssertionVerifier
{
    public static bool Verify(Action assertion)
    {
        string[] failures;
        using (var assertionScope = new AssertionScope())
        {
            assertion();
            failures = assertionScope.Discard();
        }

        if (failures.Any())
        {
            throw new Exception(string.Join(Environment.NewLine, failures));
        }

        return true;
    }
}
