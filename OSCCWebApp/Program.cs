using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

/*
 * dotnet ef dbcontext scaffold "server=katwijk.nolden.biz;port=3306;user=Ycreak;password=YcreakPasswd26!;database=OSCC_DB" MySql.Data.EntityFrameworkCore
 * 
 * (string used to scaffold db)
 * DON'T USE THE "-o Models" parameter: this will create an OSCCWebApp.Models namespace which is unkown.
 */
namespace OSCCWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.ConfigureKestrel(serverOptions => {
                        serverOptions.Listen(IPAddress.Any, 5000);
                        serverOptions.Listen(IPAddress.Any, 5001, 
                            listenOptions =>
                            {
                                listenOptions.UseHttps("/etc/ssl/mycerts/certificate.pfx", 
                                    "hoi");
                            });
                        });          
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseUrls("https://*:5001"); // Kestrel host port




                });
    }
}
