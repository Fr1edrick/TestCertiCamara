using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestCertiCamara.Logic;
using TestCertiCamara.Models;

namespace TestCertiCamara
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("policyToCors",
                  builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
      });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestCertiCamara", Version = "v1" });
      });
      services.AddDbContext<LogQueriesContext>(con =>
          con.UseSqlServer(Configuration["ConnectionStrings:logQuery"])
          );
      services.AddAutoMapper(typeof(Startup));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestCertiCamara v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors("policyToCors");

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
