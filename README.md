
# Blazor-Health-Check-Mini-Web-App

**#youdontneedjavascript**

Blazor and core 3.0 playground with health checks.
Application is fully functional. I just implement what i need at the moment. (And abused opportunity to start learning Blazor)

Thanks [Xabaril](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) for UI and great library for health checks

Thanks [Microsoft](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) for Blazor :D

Download project and open it.
Setup appsettings db Connection String

        "DefaultConnection": "{YOUR-DB-CONNECTION}"

In **SystemHealthChecksDbContext.cs**
add connection string to your db (i must fix this)

     var sqlCon = "{YOUR-CONNECTION-STRING}";

run migrations to create db.

Add at least one entry for health check point or app will break on start (have to fix that too :) )

Setup in startup endpoint title:

    settings.AddHealthCheckEndpoint("{YOUR-TITLE}", "https://localhost:5000/healthchecks");

If you want to add webhook for slack to receive notifications setup settings here:

      settings.AddWebhookNotification(
                        "Slack",
                        "{URL-TO-YOUR-SLACK-SERVICE}",
                        "{\"text\":\"The HealthCheck [[LIVENESS]] is failing with the error message [[FAILURE]]. <https://localhost:5000/healthchecks-ui|Click here> to get more details.\",\"channel\":\"#webhook-testing\",\"link_names\": 1,\"username\":\"monkey-bot\",\"icon_emoji\":\":monkey_face:\"}",
                       "{\"text\":\"The HealthCheck [[LIVENESS]] is recovered. All is up and running\",\"channel\":\"#webhook-testing\",\"link_names\": 1,\"username\":\"monkey-bot\",\"icon_emoji\":\":monkey_face\" }"
                        );

**Notice:** This App is a learning project, and small showcase of handling Health Checks and having App UI done in Blazor with zero lines of java script.
If you find something valuable here, i'll be glad.  If not, i don't care because i'm doing this for fun :)

**Geek Warning:** Interfaces and "best practices" are avoided intentionally. 
[YAGNI](https://en.wikipedia.org/wiki/You_aren%27t_gonna_need_it)

