using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using pozdravliator.Application.AppServices.Contexts.Repositories;
using pozdravliator.Application.AppServices.Contexts.Services;
using pozdravliator.Contracts.Person;
using pozdravliator.Contracts.Photo;
using pozdravliator.Hosts.Api.Controllers;
using pozdravliator.Infrastructure.DataAccess;
using pozdravliator.Infrastructure.DataAccess.Data;
using pozdravliator.Infrastructure.DataAccess.Repositories;
using pozdravliator.Infrastructure.Repository;

namespace pozdravliator.Hosts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pozdravliator", Version = "v1" });

                var includeDocsTypesMarkers = new[]
                {
                    typeof(PersonDto),
                    typeof(CreatePersonDto),
                    typeof(EditPersonDto),
                    typeof(PersonController),

                    typeof(PhotoDto),
                    typeof(PhotoInfoDto),
                    typeof(PhotoController),
                };

                foreach (var marker in includeDocsTypesMarkers)
                {
                    var xmlName = $"{marker.Assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);

                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                }
            });

            builder.Services.AddTransient<IPersonService, PersonService>();
            builder.Services.AddTransient<IPersonRepository, PersonRepository>();

            builder.Services.AddTransient<IPhotoService, PhotoService>();
            builder.Services.AddTransient<IPhotoRepository, PhotoRepository>();

            #region DB

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            builder.Services.AddScoped(typeof(DbContext), typeof(DataContext));
            builder.Services.AddScoped<IDbInitializer, EFDbInitializer>();

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseNpgsql(builder.Configuration.GetConnectionString("EFCoreDb"));
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}