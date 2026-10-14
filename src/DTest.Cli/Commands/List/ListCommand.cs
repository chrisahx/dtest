using DTest.Commands.Shared;
using Spectre.Console.Cli;

namespace DTest.Commands.List;

internal class ListCommand : Command<ListCommand.Settings>
{
    public class Settings : GlobalSettings
    {
    }
  
    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        throw new NotImplementedException();

        // if (settings.Verbose)
        // {
        //     System.Console.WriteLine("Reading project file...");
        // }
        //
        // System.Console.WriteLine("Packages:");
        // System.Console.WriteLine("  Newtonsoft.Json (13.0.1)");
        // System.Console.WriteLine("References:");
        // System.Console.WriteLine("  ../MyLib/MyLib.csproj");
        // return 0;
    }
}
