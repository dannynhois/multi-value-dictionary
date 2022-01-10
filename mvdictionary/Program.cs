// See https://aka.ms/new-console-template for more information
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
var input = Console.ReadLine();
var parser = serviceProvider.GetService<CommandLineParser>();
parser.SetValues(input).Parse();

while (input != "quit")
{
    Console.Write("> ");
    input = Console.ReadLine();
    parser.SetValues(input).Parse();
}