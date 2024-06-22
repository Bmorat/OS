using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CamaraSeguridad
{
    private readonly int _id;
    private readonly SortedSet<SolicitudAcceso> _solicitudes;
    private readonly object _lockObject = new object();
    private bool _isProcessing = false;

    public CamaraSeguridad(int id)
    {
        _id = id;
        _solicitudes = new SortedSet<SolicitudAcceso>();
    }

    public void AgregarSolicitud(SolicitudAcceso solicitud)
    {
        lock (_lockObject)
        {
            solicitud.TiempoDeLlegada = DateTime.Now;
            _solicitudes.Add(solicitud);
        }
    }

    public async Task ProcesarSolicitudesAsync()
    {
        while (true)
        {
            SolicitudAcceso solicitud = null;
            lock (_lockObject)
            {
                if (_solicitudes.Any() && !_isProcessing)
                {
                    solicitud = _solicitudes.First();
                    _solicitudes.Remove(solicitud);
                    _isProcessing = true;
                }
            }

            if (solicitud != null)
            {
                try
                {
                    Console.WriteLine($"Cámara {_id} procesando solicitud de {solicitud.Nombre} con prioridad {solicitud.Prioridad} y descripción {solicitud.Descripcion}");
                    solicitud.TiempoDeRetorno = solicitud.TiempoDeEspera + TimeSpan.FromMilliseconds(solicitud.TiempoDeAnalisis);
                    await Task.Delay(solicitud.TiempoDeAnalisis);
                    Console.WriteLine($"Cámara {_id} completó solicitud de {solicitud.Nombre}. Tiempo de espera: {solicitud.TiempoDeEspera.TotalMilliseconds} ms. Tiempo de retorno: {solicitud.TiempoDeRetorno.TotalMilliseconds} ms.");
                }
                finally
                {
                    lock (_lockObject)
                    {
                        _isProcessing = false;
                    }
                }
            }
            else
            {
                // No hay más solicitudes para procesar, salir del bucle
                break;
            }
        }

        Console.WriteLine($"Cámara {_id} ha terminado de procesar todas las solicitudes.");
    }
}