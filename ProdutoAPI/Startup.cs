using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProdutoAPI.Configuracao;
using ProdutoAPI.Token;
using ProdutoRepositorio.Repositorio;
using ProdutoServico.Interfaces;
using ProdutoServico.Servicos;
using System.IO;
using System.Text;

namespace ProdutoAPI
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
            services.AddTransient<IProdutoRepositorio, ProdutosRepositorio>();
            services.AddTransient<IProdutoServico, ProdutosServicos>();
            
            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Segredo").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API JWT",
                    Version = "v1",
                    Description = "Usar o JWT para estudo"
                });

                string caminhoApp = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeApp = PlatformServices.Default.Application.ApplicationName;
                string caminhoDoc = Path.Combine(caminhoApp, $"{nomeApp}.xml");

                c.IncludeXmlComments(caminhoDoc);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Ativa swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estudo JWT");
            });
        }
    }
}
