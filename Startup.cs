using Fiap01.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap01
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<PerguntasContext>(db=> db.UseSqlServer(connection));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "autor",
                //    template: "autor/{nome}",
                //    defaults: new { controller = "Autor", action = "Index" });
                //routes.MapRoute(
                //    name: "autoresdoano",
                //    template: "{ano:int}/autor",
                //    defaults: new { controller = "Autor", action = "ListaDosAutoresDoAno" });
                //routes.MapRoute(
                //    name: "topicosdacategoria",
                //    template: "{categoria}/{topico}",
                //    defaults: new { controller = "Topicos", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
