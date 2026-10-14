using DTest.Commands.List;
using Shouldly;

namespace DTest.UnitTests;

public class ListCommandTests
{
    [Fact]
    public void Execute_writes_project_dependencies_and_returns_success()
    {
        var command = new ListCommand();
        var settings = new ListCommand.Settings();
        var originalOutput = Console.Out;
        using var output = new StringWriter();

        try
        {
            Console.SetOut(output);

            var result = command.Execute(null!, settings, CancellationToken.None);

            result.ShouldBe(0);
            output.ToString().ShouldBe(
                $"Packages:{Environment.NewLine}" +
                $"  Newtonsoft.Json (13.0.1){Environment.NewLine}" +
                $"References:{Environment.NewLine}" +
                $"  ../MyLib/MyLib.csproj{Environment.NewLine}");
        }
        finally
        {
            Console.SetOut(originalOutput);
        }
    }
}
