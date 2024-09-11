using Microsoft.EntityFrameworkCore;
using StockControl_Repository.Context;
using StockControl_Repository.Repos.Abstract;
using StockControl_Repository.Repos.Concretes;
using StockControl_Service.Services.Abstract;
using StockControl_Service.Services.Concrete;

namespace StockControl_API
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
            builder.Services.AddSwaggerGen();

            //adding db context
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-GHS5S8N\\SQLEXPRESS;Initial Catalog=LayerApi;Integrated Security=True;Trust Server Certificate=True;"
)
            );

            //builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //burda generic olduðu için o tipte istedik burdan generic olduðunu belirtmiþ olduk...
            builder.Services.AddScoped(typeof
                (IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof
                (IGenericService<>), typeof(GenericManager<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
