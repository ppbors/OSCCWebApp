using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace OSCCWebApp
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200","http://oscc.nolden.biz")
                                      //AllowAnyOrigin();
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                    //   WithOrigins(//"https://localhost:4200","http://localhost:4200",
                                    //                       //"http://localhost:5000",
                                    //                       //"http://nolden.biz", "http://katwijk.nolden.biz",
                                    //                       //"http://oscc.nolden.biz", "https://oscc.lucdh.nl",
                                    //                       "http://oscc.nolden.biz"
                                    //                     )
                                  });
            });
            services.AddDbContext<OSCC_DBContext>();
            services.AddMvc();
            services.AddControllers();
        
            // services.AddHttpsRedirection(options => {
            //     // options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //     options.HttpsPort = 5001;
            // });
        
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                );

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
