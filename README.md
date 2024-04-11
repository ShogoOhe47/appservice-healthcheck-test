App Service HealthCheck Test

Only running on a specific instance responds with unhealthy.

### How to pecify instance.
1. check environment variable %COMPUTERNAME% from Kudu.
2. write %COMPUTERNAME% to %HOME%\ignore_instance.txt (ex. C:\home\ignore_instance.txt)
3. the specified instance returns a Bad Request (400) response on the Health Check page (/Home/HealthCheck).
