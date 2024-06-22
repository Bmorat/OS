using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        var camaras = new List<CamaraSeguridad>
        {
            new CamaraSeguridad(1),
            new CamaraSeguridad(2),
            new CamaraSeguridad(3)
        };

        // Cargar las solicitudes desde el archivo JSON
        var solicitudes = await SolicitudManager.CargarSolicitudesAsync();

        if (solicitudes.Count == 0)
        {
            // Definir las solicitudes si no se cargaron correctamente
            Console.WriteLine("\nNo se encontraron solicitudes cargadas desde el archivo. Definiendo nuevas solicitudes...");

            solicitudes = new List<SolicitudAcceso>
            {
                new SolicitudAcceso { Prioridad = 7, Nombre = "Brian", TiempoDeAnalisis = 1000, Descripcion = "Alumno" },
                new SolicitudAcceso { Prioridad = 7, Nombre = "Alfonso", TiempoDeAnalisis = 1500, Descripcion = "Alumno" },
                new SolicitudAcceso { Prioridad = 1, Nombre = "Agustin", TiempoDeAnalisis = 500, Descripcion = "Alumno VIP" },
                new SolicitudAcceso { Prioridad = 3, Nombre = "Franyer", TiempoDeAnalisis = 1000, Descripcion = "Profe" },
                new SolicitudAcceso { Prioridad = 7, Nombre = "Ingrid", TiempoDeAnalisis = 1500, Descripcion = "Profe" },
                new SolicitudAcceso { Prioridad = 1, Nombre = "Julio", TiempoDeAnalisis = 500, Descripcion = "Profe" }
            };

            // Guardar las solicitudes en el archivo JSON
            await SolicitudManager.GuardarSolicitudesAsync(solicitudes);
        }

        /// Ordenar las solicitudes por prioridad (de menor a mayor)
        var solicitudesOrdenadas = solicitudes.OrderBy(s => s.Prioridad).ToList();

        // Asignar las solicitudes a las cámaras secuencialmente
        for (int i = 0; i < solicitudesOrdenadas.Count; i++)
        {
            var solicitud = solicitudesOrdenadas[i];
            int camaraId = i % camaras.Count; // Utilizar módulo para asignar cíclicamente a las cámaras
            camaras[camaraId].AgregarSolicitud(solicitud);
        }

        // Procesar las solicitudes en todas las cámaras
        var tasks = new List<Task>();
        foreach (var camara in camaras)
        {
            tasks.Add(camara.ProcesarSolicitudesAsync());
        }

        // Esperar que todas las cámaras terminen de procesar
        await Task.WhenAll(tasks);

        Console.WriteLine("\nTodas las cámaras han terminado de procesar sus solicitudes.");
    }
}
