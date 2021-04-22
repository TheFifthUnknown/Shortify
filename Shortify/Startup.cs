
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shortify.Context;
using Microsoft.EntityFrameworkCore;

namespace Shortify
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //path - https://stackoverflow.com/questions/43709657/how-to-get-root-directory-of-project-in-asp-net-core-directory-getcurrentdirect
            string connection = Configuration.GetConnectionString("AbsolutePath");
            connection = (connection.Equals("")) ? 
                Configuration.GetConnectionString("DefaultConnection") + Configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + Configuration.GetConnectionString("RelativePath") 
                : Configuration.GetConnectionString("DefaultConnection") + connection;

            services.AddDbContext<ContextLinks>(options => options.UseSqlite(connection));
            services.AddControllers();
            services.AddRazorPages();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
