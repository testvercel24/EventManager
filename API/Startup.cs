using Services;
using Controller;
using AutoMapper;
using Repository;
using Entity.Dtos;
using Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using NLog;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
namespace API
{
  [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
      });
      services.AddHttpContextAccessor();

      var mapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new AutoMapping());
      });
      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      var config = new MapperConfiguration(cfg =>
      {
        AutoMapping.Configure(cfg);
      });
      services.AddSingleton(config.CreateMapper());

      //Entity framework database connection
      services.AddEntityFrameworkNpgsql()
      .AddDbContext<ApiDbContext>(opt =>
              opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Entity")));
      AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

      services.AddScoped<UserController>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<EventController>();
      services.AddScoped<IEventService, EventService>();
      services.AddScoped<IEventRepository, EventRepository>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
      if (env.IsDevelopment())
      {
        //app.UseDeveloperExceptionPage();
      }

      app.UseExceptionHandler(appError =>
      {
        appError.Run(async context =>
        {
          var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
          if (contextFeature != null)
          {
            context.Response.ContentType = "application/json";
            CustomException? baseException = (CustomException)contextFeature.Error;
            context.Response.StatusCode = baseException.Code;
            await context.Response.WriteAsync(new ErrorDto
            {
              Code = baseException.Code,
              Message = baseException.Messages,
              Description = baseException.Description
            }.ToString());


            // return;
          }
        });
      });
      // MyAppHttpContext.Configure(httpContextAccessor);
      app.UseSwagger();
      app.UseSwaggerUI();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}