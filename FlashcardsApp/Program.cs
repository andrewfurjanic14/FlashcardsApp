using Dapper;
using FlashcardsApp.Views;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Reflection;

namespace FlashcardsApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SqlMapper.AddTypeHandler(new DateOnlyHandler());
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var editUI = new EditUI(configuration);
            var studyUI = new StudyUI(configuration);

            var promptInput = new SelectionPrompt<string>()
                    .AddChoices("Manage Decks", "Study", "Exit");
            
            bool exit = false;
            while (!exit)
            {
                var choice = AnsiConsole.Prompt(promptInput);
                switch (choice)
                {
                    case "Manage Decks":
                        await editUI.RunAsync();
                        break;
                    case "Study":
                        await studyUI.RunAsync();
                        break;
                    case "Exit":
                        exit = true;
                        break;
                }
            }
            
        }
    }
}
