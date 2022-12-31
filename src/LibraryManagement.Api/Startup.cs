
using LibraryManagement.Business;
using LibraryManagement.Business.Contracts;
using LibraryManagement.Data;
using LibraryManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace LibraryManagement.Api
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
            var logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
            }).CreateLogger("Library service");

            services.AddSingleton(c => logger);
            services.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));
            services.AddScoped<DbContext>(c => new LibraryDbContext(Configuration
                .GetValue<string>("DataBase:ConnectionString")));
            services.AddScoped<IBookBusinessService, BookBusinessService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddRazorPages();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger, DbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex) when (ex != null)
            {
                logger.LogError(ex.Message);
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseSwaggerUI();
            app.Run((context) => Task.Run(() => context.Response.Redirect("/swagger/v1/swagger.json")));
        }
    }
}
