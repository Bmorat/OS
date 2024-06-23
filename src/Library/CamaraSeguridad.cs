namespace Library
{
        public class CamaraSeguridad
        {
            private readonly int _id;
            private readonly PriorityQueue<SolicitudAcceso, int> _solicitudes = new PriorityQueue<SolicitudAcceso, int>();
            private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

            public int CamaraId => _id;

            public CamaraSeguridad(int id)
            {
                _id = id;
            }

            public void AgregarSolicitud(SolicitudAcceso solicitud)
            {
                solicitud.TiempoDeLlegada = DateTime.Now;
                _semaphore.Wait();
                try
                {
                    _solicitudes.Enqueue(solicitud, solicitud.Prioridad);
                }
                finally
                {
                    _semaphore.Release();
                }
            }

            public async Task ProcesarSolicitudesAsync(HashSet<Person> personasAutorizadas, Dictionary<string, int> tratosEspeciales, Dictionary<string, TimeSpan> metricas)
            {
                while (_solicitudes.Count > 0)
                {
                    SolicitudAcceso solicitud = null;

                    await _semaphore.WaitAsync();
                    try
                    {
                        if (_solicitudes.TryDequeue(out solicitud, out _))
                        {
                            var autorizado = personasAutorizadas.Contains(solicitud.Persona);
                            var mensajeAutorizacion = autorizado ? "autorizada" : "denegada";

                            Console.WriteLine($"Cámara {_id} procesando solicitud de {solicitud.Persona.Nombre} en puerta {solicitud.Puerta} con prioridad {solicitud.Prioridad}. Acceso {mensajeAutorizacion}.");

                            // Aplicar tratos especiales si hay
                            if (tratosEspeciales.ContainsKey(solicitud.Persona.Tipo))
                            {
                                solicitud.TiempoDeAnalisis -= tratosEspeciales[solicitud.Persona.Tipo];
                            }

                            solicitud.TiempoDeRetorno = solicitud.TiempoDeEspera + TimeSpan.FromMilliseconds(solicitud.TiempoDeAnalisis);
                            await Task.Delay(solicitud.TiempoDeAnalisis);

                            Console.WriteLine($"Cámara {_id} completó solicitud de {solicitud.Persona.Nombre}. Tiempo de espera: {solicitud.TiempoDeEspera.TotalMilliseconds} ms. Tiempo de retorno: {solicitud.TiempoDeRetorno.TotalMilliseconds} ms. Acceso {mensajeAutorizacion}.");

                            // Registrar métricas
                            if (!metricas.ContainsKey(solicitud.Persona.Tipo))
                            {
                                metricas[solicitud.Persona.Tipo] = TimeSpan.Zero;
                            }
                            metricas[solicitud.Persona.Tipo] += solicitud.TiempoDeRetorno;
                        }
                    }
                    finally
                    {
                        _semaphore.Release();
                    }

                    if (solicitud == null)
                    {
                        await Task.Delay(500); // Esperar un poco antes de volver a verificar si hay nuevas solicitudes
                    }
                }
            }
        }
}