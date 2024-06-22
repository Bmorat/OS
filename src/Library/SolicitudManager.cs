using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class SolicitudManager
{
    private const string FilePath = "solicitudes.json";

    public static async Task GuardarSolicitudesAsync(List<SolicitudAcceso> solicitudes)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(solicitudes, options);
            await File.WriteAllTextAsync(FilePath, jsonString);
            Console.WriteLine($"Solicitudes guardadas correctamente en {FilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar las solicitudes en {FilePath}: {ex.Message}");
        }
    }

    public static async Task<List<SolicitudAcceso>> CargarSolicitudesAsync()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                var jsonString = await File.ReadAllTextAsync(FilePath);
                var solicitudes = JsonSerializer.Deserialize<List<SolicitudAcceso>>(jsonString);
                return solicitudes;
            }

            Console.WriteLine($"No se encontró el archivo {FilePath}. Retornando lista vacía.");
            return new List<SolicitudAcceso>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar las solicitudes desde {FilePath}: {ex.Message}");
            return new List<SolicitudAcceso>();
        }
    }
}
