using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace MusicStore.HealthCheckApi.HealthChecks
{
    public class PingHealthCheck : IHealthCheck
    {
        private readonly string _host;

        public PingHealthCheck(string host)
        {
            _host=host;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var ping = new Ping();

            HealthCheckResult result;
            try
            {
                var reply = await ping.SendPingAsync(_host,2000);
                switch (reply.Status)
                {
                    case IPStatus.Success:
                        result = HealthCheckResult.Healthy($"El host {_host} funciona a la perfeccion");
                        break;
                    case IPStatus.BadDestination:
                        result = HealthCheckResult.Unhealthy($"El host {_host} no es accesible");
                        break;
                    case IPStatus.TimedOut:
                    case IPStatus.TimeExceeded:
                        result = HealthCheckResult.Degraded($"El host {_host} no responde rapidamente");
                        break;
                    default:
                        result = HealthCheckResult.Unhealthy($"El host {_host} no es accesible");
                        break;
                }
            }
            catch (Exception ex)
            {

                result = HealthCheckResult.Unhealthy(ex.Message);
            }
            return result;
        }
    }
}
