using Autofac.Extensions.DependencyInjection;
using Idis.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;

namespace Idis.Website
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information("Starting up!");

            //Generator.AutoService("D:\\", "Internship.Service", ".cs", new[] {
            //    "Activity",
            //    "Department",
            //    "EntityBase",
            //    "Event",
            //    "EventType",
            //    "Intern",
            //    "Organization",
            //    "Point",
            //    "Question",
            //    "Training",
            //    "User"});

            try
            {
                var host = CreateHostBuilder(args);
                SeedDatabase(host);
                host.Run();

                Log.Information("Stopped cleanly");
                return 200;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
                return -2;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(services => services.AddAutofac())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }

        private static void SeedDatabase(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();

            context.Database.EnsureCreated();
            // Do seed action here
            string[] list_fname = { "Thanh", "Viet", "Quoc", "Lan", "Lura", "Theo", "Jamel", "Lizbeth", "Esmeralda", "Rolf", "Kendall", "Rubi", "Korey", "Debora", "Jarvis", "Madge", "Marquis", "Reta", "Alysa", "Eldora", "Dorene", "Danika", "Tamar", "Domitila" };
            string[] list_lname = { "Laborde", "Turley", "Jensen", "Lafortune", "Enriquez", "Roberson", "Kees", "Rae", "Kibler", "Demar", "Jenney", "Mong", "Mayville", "Ringdahl", "Adcox", "Eberhard", "Dekker", "Diangelo", "Trostle", "Dunkle" };

            if (!context.Interns.Any())
                for (int i = 0; i < 100; i++)
                {
                    int indexer = i + 1;

                    int fid = new Random().Next(0, list_fname.Length);
                    int lid = new Random().Next(0, list_lname.Length);

                    var temp = new Intern
                    {
                        Email = list_fname[fid] + "@" + indexer,
                        FirstName = list_fname[fid],
                        LastName = list_lname[lid],
                        Gender = (indexer % 2) == 0 ? "Male" : "Female",
                        DateOfBirth = "1998-07-20",
                        Phone = "0943154555",
                        Duration = "2021-05-16 - 2021-02-02",
                        TrainingId = 0,
                        DepartmentId = new Random().Next(1, 6),
                        OrganizationId = new Random().Next(1, 4),
                        Avatar = "_intern.jpg",
                        MentorId = new Random().Next(1, 5),
                        Type = "Part time"
                    };
                    context.Interns.Add(temp);
                }
            context.SaveChanges();
        }
    }
}