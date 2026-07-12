using FlashcardsApp.Views;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FlashcardsApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var editUI = new EditUI(configuration);

            await editUI.RunAsync();
        }
    }
}
