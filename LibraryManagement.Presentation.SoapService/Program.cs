using SoapCore;
using LibraryManagement.Presentation.SoapService.ServiceContract;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagement.Presentation.SoapService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSoapCore();
            builder.Services.AddSingleton<ILibraryService, LibraryService>();
            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb"));
            });


            var app = builder.Build();

            app.UseRouting();

            app.UseSoapEndpoint<ILibraryService>("/LibraryService.asmx", new SoapEncoderOptions());

            app.MapGet("/", () => "Hello World!");




            app.Run();
        }
    }
}
