using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*
 * dotnet ef dbcontext scaffold "server=katwijk.nolden.biz;port=3306;user=Ycreak;password=YcreakPasswd26!;database=OSCC_NEW" MySql.Data.EntityFrameworkCore
 * 
 * (string used to scaffold db)
 * 
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}
