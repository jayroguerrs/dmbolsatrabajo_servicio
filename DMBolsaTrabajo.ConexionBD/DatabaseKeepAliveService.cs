using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ConexionBD;
using MySqlConnector;

public class DatabaseKeepAliveService : BackgroundService
{
    private readonly IMySQLConexion _mysqlConexion;
    private readonly ILogger<DatabaseKeepAliveService> _logger;

    public DatabaseKeepAliveService(IMySQLConexion mysqlConexion, ILogger<DatabaseKeepAliveService> logger)
    {
        _mysqlConexion = mysqlConexion;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var connection = _mysqlConexion.GetConnection();
                await connection.OpenAsync(stoppingToken);

                using var cmd = new MySqlCommand("SELECT 1", connection);
                await cmd.ExecuteNonQueryAsync(stoppingToken);

                _logger.LogInformation("Keep-alive ejecutado correctamente.");

                // Espera 5 minutos
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el keep-alive.");
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // igual espera aunque haya error
            }
        }
    }
}
