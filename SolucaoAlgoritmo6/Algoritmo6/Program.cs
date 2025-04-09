using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Algoritmo6
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var builder = Host.CreateApplicationBuilder();

            //builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var apiUrl = builder.Configuration.GetSection("ApiSettings:BaseUrl").Value;

            // Adiciona HttpClient com base address
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://suaapi.com/api/");
            });

            // Registra a tela principal e outros forms/serviços
            builder.Services.AddTransient<Form1>();

            var host = builder.Build();

            // Resolve a instância do Form1 com DI
            var form = host.Services.GetRequiredService<Form1>();
            Application.Run(form);
        }
    }
}