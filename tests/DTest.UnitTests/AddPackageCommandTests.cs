using DTest.Commands.Add;
using DTest.Services;
using NSubstitute;
using Shouldly;
using Spectre.Console.Testing;

namespace DTest.UnitTests;

public class AddPackageCommandTests
{
    [Fact]
    public void Execute_checks_the_requested_package_writes_a_confirmation_and_returns_success()
    {
        var console = new TestConsole();
        var packageService = Substitute.For<IPackageService>();
        packageService.CheckPackage("Spectre.Console", "0.57.2")
            .Returns("Package found");
        var command = new AddPackageCommand(console, packageService);
        var settings = new AddPackageCommand.Settings
        {
            PackageName = "Spectre.Console",
            Version = "0.57.2"
        };

        var result = command.Execute(null!, settings, CancellationToken.None);

        result.ShouldBe(0);
        packageService.Received(1).CheckPackage("Spectre.Console", "0.57.2");
        console.Output.ShouldContain("Added package Spectre.Console v0.57.2");
        console.Output.ShouldContain("Package found");
    }
}
