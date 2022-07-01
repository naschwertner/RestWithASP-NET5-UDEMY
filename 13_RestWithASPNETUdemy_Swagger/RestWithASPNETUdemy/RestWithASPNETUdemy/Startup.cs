using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Services;
using RestWithASPNETUdemy.Services.Implementations;
using RestWithASPNETUdemy.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using RestWithASPNETUdemy.Hypermedia.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration,  IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        
        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //acessa aquivo appsettings le as propriedades, encontra a mySQLConnection, depois encontra a
            //mySQLConnectionString
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];

            //DdContext
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
                .AddXmlSerializerFormatters();

            //HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);


            //Versiona a API
            services.AddApiVersioning();

            //configurações do Swagger 
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API's From 0 to Azure with ASP .NET Core 5 and Docker",
                        Version = "v1",
                        Description = "API RESTful developed in course 'REST API's' From 0 to Azure with ASP .NET Core 5 and Docker",
                        Contact = new OpenApiContact
                        {
                            Name = "Nathalia Schwertner",
                            Url = new Uri("https://github.com/naschwertner")
                        }
                    });
                  
                 });


            //injeção de dependencia
            services.AddScoped<IPersonService, PersonServiceImplementation>();
            services.AddScoped<IBookService, BookServiceImplementation>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

       

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //configuração do Swagger e do SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "REST API's From 0 do Azure with ASP.NET Core 5 and Docker - v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }

    }
}
