using ApiMoneda.Data;
using ApiMoneda.Services.Implementations;
using ApiMoneda.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using ApiMoneda.Service.Implementations;
using ApiMoneda.Services.Interface;

namespace ApiMoneda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //crea un nuevo constructor de la aplicación web
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( setupAction =>
            {
                //Esto va a permitir usar swagger con el token.
                setupAction.AddSecurityDefinition("ConversorApiBearerAuth", new OpenApiSecurityScheme() 
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Acá pegar el token generado al loguearse."
                });
                //asegura que Swagger requerirá el token de autenticación para acceder a los endpoints de la API.
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ConversorApiBearerAuth" //Tiene que coincidir con el id seteado arriba en la definición
                            }
                        }, new List<string>()
                    }
                });

            });

            //Agrega el contexto de base de datos a los servicios de la aplicación.
            builder.Services.AddDbContext<ConversorContext>(dbContextOptions => dbContextOptions.UseSqlite(
            builder.Configuration["ConnectionStrings:ConversorAPIDBConnectionString"]));

            builder.Services.AddAuthentication("Bearer") //Esto indica que se utilizará la autenticación basada en tokens de tipo "Bearer".
                .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                    };
                }
            );
            
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IConversionService, ConversionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //permite que el servidor indique que cualquier dominio o puerto diferente al suyo puede cargar recursos.
            app.UseCors(
                  options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}