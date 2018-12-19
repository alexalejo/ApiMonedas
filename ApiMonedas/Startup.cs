using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ApiMonedas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
namespace ApiMonedas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public Action<MvcJsonOptions> ConfigureJson { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(Options => 
                Options.UseInMemoryDatabase("MonedaDB"));//tions.UseSqlServer(Configuration.GetConnectionString("defaulConnection")//cambiamos para apuntar a una base

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            services.AddMvc();//.AddJsonOptions(ConfigureJson);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            //parfa subir o bajar cotizacion
            double porcentaje = 1.02;
            if (!context.Moneda.Any())
            {
                context.Moneda.AddRange(new List<Moneda>()
                {
                   new Moneda() { Divisa = "DOLAR", Precio =40*porcentaje },
                new Moneda() { Divisa = "EURO", Precio = 44*porcentaje },
                new Moneda() { Divisa = "REAL", Precio = 25*porcentaje},
                new Moneda() { Divisa = "SOL", Precio = 15*porcentaje}
                });
                context.SaveChanges();
            }
        }
    }
}
