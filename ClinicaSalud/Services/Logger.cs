namespace ClinicaSalud.Services;

public static class Logger
{
    public static void LogError(string mensaje, Exception ex)
    {
        string logMsg = $"[{DateTime.Now}] ERROR: {mensaje} | Detalle: {ex.Message}\n";
        Console.WriteLine($"\n[ALERTA DE SISTEMA] {mensaje}. Revise el archivo de logs.");
        File.AppendAllText("errores.log", logMsg); 
    }
}