using AirportProject.Context;
using AirportProject.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace AirportProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;
        private ILog _logger;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "cors", builder => { builder.WithOrigins("*"); });
            });
            services.AddControllers();
            services.AddMvc();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            _logger = LogManager.GetLogger("AirportService");
            services.AddSingleton<ILog>(_logger);
            services.AddScoped<IProcessAirportService, ProcessAirportService>();
            services.AddDbContext<DBContext>(options => options.UseSqlServer(_configuration.GetConnectionString("default")));
        }

        private void OnStop()
        {
            _logger.Info("Stop");
        }

        private void OnStart()
        {
            _logger.Info("Start");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(OnStart);
            applicationLifetime.ApplicationStopped.Register(OnStop);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cors");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
