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

        public void AgregarSolicitud(SolicitudAcceso solicitud)         // Clase encargada de agregar una solicitud a la cola de prioridad
        {
            solicitud.TiempoDeLlegada = DateTime.Now;                   // Se le asigna la hora real de llegada la solicitud
            _semaphore.Wait();                                          // Esperar a que no haya otros hilos accediendo a la cola, usando un semáforo
            try
            {
                _solicitudes.Enqueue(solicitud, solicitud.Prioridad);   // Intenta agregar la solicitud a la cola, con la prioridad correspondiente
            }
            finally
            {
                _semaphore.Release();                                   // Libera el semáforo
            }
        }

        public async Task ProcesarSolicitudesAsync(HashSet<Person> personasAutorizadas, Dictionary<string, int> tratosEspeciales, Dictionary<string, TimeSpan> metricas)  // Clase encargada de procesar las solicitudes de acceso
        {
            while (_solicitudes.Count > 0)                                                                            
            {
                SolicitudAcceso solicitud = null;

                await _semaphore.WaitAsync();       // Esperar a que no haya otros hilos ingresando a la cola, usando un semáforo
                try
                {
                    if (_solicitudes.TryDequeue(out solicitud, out _))          // Si hay solicitudes en la cola, intenta sacar la solicitud con mayor prioridad
                    {
                        var autorizado = personasAutorizadas.Contains(solicitud.Persona);
                        var mensajeAutorizacion = autorizado ? "autorizada" : "denegada";

                        Console.WriteLine($"Cámara {_id} procesando solicitud de {solicitud.Persona.Nombre} en puerta {solicitud.Puerta} con prioridad {solicitud.Prioridad}. Acceso {mensajeAutorizacion}.");

                        // Aplicar tratos especiales si hay
                        if (tratosEspeciales.ContainsKey(solicitud.Persona.Tipo))           // Si la persona tiene un trato especial, se le resta el tiempo asignado
                        {
                            solicitud.TiempoDeAnalisis -= tratosEspeciales[solicitud.Persona.Tipo];    
                        }

                        solicitud.TiempoDeRetorno = solicitud.TiempoDeEspera + TimeSpan.FromMilliseconds(solicitud.TiempoDeAnalisis); // Tiempo de espera + tiempo de análisis
                        await Task.Delay(solicitud.TiempoDeAnalisis);       // Simular tiempo de análisis

                        Console.WriteLine($"Cámara {_id} completó solicitud de {solicitud.Persona.Nombre}. Tiempo de retorno: {solicitud.TiempoDeEspera.TotalMilliseconds} ms. Tiempo de respuesta: {solicitud.TiempoDeRetorno.TotalMilliseconds} ms. Acceso {mensajeAutorizacion}.");

                        // Registrar métricas
                        if (!metricas.ContainsKey(solicitud.Persona.Tipo))              // Si no hay solicitud para para un tipo de persona, se crea un nuevo registro
                        {
                            metricas[solicitud.Persona.Tipo] = TimeSpan.Zero;           // Se le asigna un tiempo de 0
                        }
                        metricas[solicitud.Persona.Tipo] += solicitud.TiempoDeRetorno;  // Se le suma el tiempo de retorno de la solicitud
                    }
                }
                finally
                {
                    _semaphore.Release();           // Libera el semáforo.
                }

                if (solicitud == null)
                {
                    await Task.Delay(500);          // Esperar un poco antes de volver a verificar si hay nuevas solicitudes
                }
            }
        }
    }
}

/*
Usando semaforos me aseguro que solo un proceso pueda acceder a la cola de solicitudes a la vez, evitando que se produzcan errores al intentar agregar o sacar solicitudes de la cola.
La importancia de tener identificados a las personas VIP o con tratos especiales es que se reducen el tiempo de análisis y permitiendo que se les de el acceso más rápido.
Tambien la ejecucion de los procesos por las prioridades de las solicitudes, permitiendo ver que solicitudes se atienden primero.

*/