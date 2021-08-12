using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace LayuiTableGenerate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
           //6 services.AddSingleton<ServiceStack.OrmLite.OrmLiteContext>();

            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IDbConnectionFactory>(c =>
    new OrmLiteConnectionFactory(":memory:", SqlServerDialect.Provider));
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new DefaultNamingStrategy()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=test}/{id?}");
            });
        }
    }

}
