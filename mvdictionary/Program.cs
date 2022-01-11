// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using mvdictionary;

// dependency injection
var services = new ServiceCollection();
var serviceProvider = services
    .AddSingleton<IMultiValueDictionary<string>, MultiValueDictionary<string>>()
    .AddTransient<CommandLineParser>()
    .BuildServiceProvider();

Console.WriteLine("Enter a command:");
Console.Write("> ");
var parser = serviceProvider.GetService<CommandLineParser>();
if (parser is null)
{
    Console.WriteLine("Error loading parser.");
    System.Environment.Exit(Environment.ExitCode);
}
var input = Console.ReadLine();
parser.SetValues(input!).Parse();

while (input != "quit")
{
    Console.Write("> ");
    input = Console.ReadLine();
    parser = serviceProvider.GetService<CommandLineParser>();
    if (parser is null)
    {
        Console.WriteLine("Error loading parser.");
        System.Environment.Exit(Environment.ExitCode);
    }
    parser.SetValues(input!).Parse();
}