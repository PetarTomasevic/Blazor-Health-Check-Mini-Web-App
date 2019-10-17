using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemHealthChecks.Web.Areas.Identity;
using SystemHealthChecks.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using SystemHealthChecks.Domain.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SystemHealthChecks.Domain.Services.HealthChecks;
using SystemHealthChecks.Domain.Services.DatabaseHealthChecks;
using SystemHealthChecks.Domain.Services.ApiHealthCheck;

namespace SystemHealthChecks.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddLogging();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<SystemHealthChecksDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<SystemHealthChecksDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<CategoryQueries>();
            services.AddSingleton<DatabaseHealthCheckCommands>();
            services.AddSingleton<DatabaseHealthCheckQueries>();
            services.AddSingleton<UrlApiHealthCheckCommands>();
            services.AddSingleton<UrlApiHealthCheckQueries>();

            //Dohvati Sve Baze za provjeru statusa iz baze
            DatabaseHealthCheckQueries dhcq = new DatabaseHealthCheckQueries();
            var databaseHealthchecks = dhcq.GetListDatabaseHealthChecks().Result;
            //generiraj health check pointe iz liste dobivene iz baze.
            foreach (var item in databaseHealthchecks)
            {
                services.AddHealthChecks().AddCheck(item.DatabaseName, new DBHealthCheck(item.DatabaseConnectionString, item.DatabaseTestQuery, item.DatabaseName));
            }
            //Dohvati Sve API Pointe za provjeru statusa iz baze
            UrlApiHealthCheckQueries uhcq = new UrlApiHealthCheckQueries();
            var urlApiHealthchecks = uhcq.GetListUrlApiHealthCheck().Result;
            //generiraj health check pointe iz liste dobivene iz baze.
            foreach (var item in urlApiHealthchecks)
            {
                services.AddHealthChecks().AddCheck(item.Description, new APIHealthCheck(item.HostUrl, item.TestApiPath));
            }
            //system Memory Health Check
            services.AddHealthChecks().AddCheck("System Memory Health Check", new SystemMemoryHealthCheck());
            services.AddHealthChecksUI("healthchecksdb", settings =>
            {
                settings.SetEvaluationTimeInSeconds(20);
                settings.SetMinimumSecondsBetweenFailureNotifications(60);
                settings.SetHealthCheckDatabaseConnectionString("Data Source=App_Data\\healthchecksdb");
                settings.AddHealthCheckEndpoint("{YOUR-TITLE}", "https://localhost:5000/healthchecks");
                settings.AddWebhookNotification(
                    "Slack",
                    "{URL-TO-YOUR-SLACK-SERVICE}",
                    "{\"text\":\"The HealthCheck [[LIVENESS]] is failing with the error message [[FAILURE]]. <https://localhost:5000/healthchecks-ui|Click here> to get more details.\",\"channel\":\"#webhook-testing\",\"link_names\": 1,\"username\":\"monkey-bot\",\"icon_emoji\":\":monkey_face:\"}",
                   "{\"text\":\"The HealthCheck [[LIVENESS]] is recovered. All is up and running\",\"channel\":\"#webhook-testing\",\"link_names\": 1,\"username\":\"monkey-bot\",\"icon_emoji\":\":monkey_face\" }"
                    );
            }).AddAuthorization();
            
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/healthchecks",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
                );
            app.UseHealthChecksUI(setup => { setup.ApiPath = "/healthchecks"; setup.UIPath = "/healthcheckui"; }).UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapHealthChecks("/healthchecks", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI().RequireAuthorization();
            });
        }
    }
}
