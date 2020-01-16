
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



The MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

