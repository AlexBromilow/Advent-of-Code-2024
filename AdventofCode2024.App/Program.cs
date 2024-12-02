using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AdventofCode.Core.PuzzleInput.Service;
using AdventofCode.Core.PuzzleInput.Service.Interface;
using AdventofCode.Core.PuzzleInput.Repository;
using AdventofCode.Core.Puzzle;
using Advent_of_Code_2024.Day1;
using Advent_of_Code_2024.Day2;

internal class Program
{
    private static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration.AddJsonFile("C:\\Projects\\Advent of Code 2024\\AdventofCode2024.App\\appsettings.json", optional: false, reloadOnChange: true);

        builder.Services.AddSingleton<IPuzzleInputService, PuzzleInputService>();
        builder.Services.AddSingleton<PuzzleInputRepository>();
        builder.Services.AddSingleton<Day2>();
        builder.Services.AddTransient<PuzzleTools>();

        using IHost host = builder.Build();

        using IServiceScope serviceScope = host.Services.CreateScope();

        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        Day2 day2 = serviceProvider.GetRequiredService<Day2>();

        day2.Challenge2();

        await host.RunAsync();
    }
}