using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMcom.Teste.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AMcom.Teste.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            //Adiciona appsettings com base no ambiente
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddJsonFile($"appsettings.Development.json", optional: true);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Registra a injeção de dependência nativa das classes do projeto
            services.RegisterServices();
            services.AddMvc();

            //Adiciona swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Desafio AMCOM",
                    Description = "Desafio AMCOM swagger",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //Configura UI do swagger
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio AMCOM");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}