using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pizzaria.API.Infrastructure.Options;
using Pizzaria.API.Repository;
using Pizzaria.API.Repository.Interfaces;
using Pizzaria.API.Services;
using Pizzaria.API.Services.Interfaces;
using System;

namespace Pizzaria.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var swaggerTitle = Configuration.GetValue<string>("SwaggerApiName");

            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = swaggerTitle,
                    Description = "Teste para Desenvolvedor Sênior Logstore",
                    Contact = new OpenApiContact
                    {
                        Name = "Emilio Córdova",
                        Email = "emiliocordova@msn.com",
                        Url = new Uri("http://www.ecordova.com.br"),
                    },
                }); ;
            });
            services.Configure<DatabaseOptions>(Configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<IClientesService, ClientesService>();
            services.AddScoped<IClientesRepository, ClientesRepository>();
            services.AddScoped<IProdutosService, ProdutosService>();
            services.AddScoped<IProdutosRepository, ProdutosRepository>();
            services.AddScoped<IPedidosService, PedidosService>();
            services.AddScoped<IPedidosRepository, PedidosRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var swaggerTitle = Configuration.GetValue<string>("SwaggerApiName");

                c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerTitle);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
