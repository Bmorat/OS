﻿using Library;

namespace Program
{
    public class Program
    {
        public static async Task Main()
        {
            // Creo las personas para la simuacion de acceso a la universidad.
            Person brian = new Person("Brian", "Morat", "Alumno", true, 12345678);
            Person alfonso = new Person("Alfonso", "Rodriguez", "Alumno", true, 23456789);
            Person agustin = new Person("Agustin", "Negreira", "Alumno", false, 34567890);
            Person julio = new Person("Julio", "Montañez", "Profesor", true, 45678901);
            Person ingrid = new Person("Ingrid", "Machado", "Profesor", true, 56789012);
            Person franyer = new Person("Franyer", "Malave", "Profesor", true, 67890123);
            Person carlos = new Person("Carlos", "Tevez", "Funcionario", true, 78901234);
            Person wanchope = new Person("Wanchope", "Avila", "Funcionario", false, 89012345);

            // Defino una lista de personas autorizadas para entrar a la universidad.
            var personasAutorizadas = new HashSet<Person> { brian, alfonso, julio, ingrid, franyer, carlos};

            /*
            Creo la lista de solicitudes para entrar a la universidad con sus caracteristicas (la persona, la prioridad de la solicitud definida de 
            forma arbitraria para la simulacion, el tiempo de analisis, la camara por la cual se estaria captando y la puerta por la que entra la persona).
            */
            var solicitudes = new List<SolicitudAcceso>
            {
                new SolicitudAcceso { Prioridad = 7, Persona = brian, TiempoDeAnalisis = 1000, CamaraId = 1, Puerta = "A" },
                new SolicitudAcceso { Prioridad = 7, Persona = alfonso, TiempoDeAnalisis = 1500, CamaraId = 2, Puerta = "B" },
                new SolicitudAcceso { Prioridad = 1, Persona = agustin, TiempoDeAnalisis = 500, CamaraId = 3, Puerta = "C" },
                new SolicitudAcceso { Prioridad = 3, Persona = franyer, TiempoDeAnalisis = 1000, CamaraId = 1, Puerta = "D" },
                new SolicitudAcceso { Prioridad = 7, Persona = ingrid, TiempoDeAnalisis = 1500, CamaraId = 2, Puerta = "E" },
                new SolicitudAcceso { Prioridad = 1, Persona = julio, TiempoDeAnalisis = 500, CamaraId = 3, Puerta = "A" },
                new SolicitudAcceso { Prioridad = 3, Persona = wanchope, TiempoDeAnalisis = 1500, CamaraId = 3, Puerta = "A" },
                new SolicitudAcceso { Prioridad = 2, Persona = carlos, TiempoDeAnalisis = 1000, CamaraId = 3, Puerta = "A" }
            };

            var camaras = new List<CamaraSeguridad>
            {
                /* 
                Se crea cada una de las camaras, que corre en un proceso, y a su vez, cada camara procesa diferentes procesos (puertas de acceso),
                y los administra mediante una cola de concurrencia.
                Una ConcurrentQueue<T> está diseñada para ser segura para el acceso concurrente desde múltiples hilos. Esto significa que puedes
                agregar y eliminar elementos desde diferentes hilos sin necesidad de implementar bloqueos adicionales (lock) para garantizar la
                coherencia de los datos.
                */
                new CamaraSeguridad(1),
                new CamaraSeguridad(2),
                new CamaraSeguridad(3)
            };

            // Tratos Especiales
            var tratosEspeciales = new Dictionary<string, int>
            {
                { "VIP", -500 },
                { "Capacidades Diferentes", -300 }
            };

            // Diccionario para métricas de desempeño
            var metricas = new Dictionary<string, TimeSpan>();

            // Asignar las solicitudes a las cámaras correspondientes
            foreach (var solicitud in solicitudes)
            {
                camaras.First(c => c.CamaraId == solicitud.CamaraId).AgregarSolicitud(solicitud);
            }

            // Procesar las solicitudes en todas las cámaras
            var tasks = camaras.Select(camara => camara.ProcesarSolicitudesAsync(personasAutorizadas, tratosEspeciales, metricas)).ToList();

            // Esperar que todas las cámaras terminen de procesar
            await Task.WhenAll(tasks);

            Console.WriteLine("\nTodas las cámaras han terminado de procesar sus solicitudes.");
            
            // Mostrar métricas de desempeño
            foreach (var tipo in metricas.Keys)
            {
                Console.WriteLine($"Tipo: {tipo}, Tiempo Total de Retorno: {metricas[tipo].TotalMilliseconds} ms.");
            }
            
            /*
            Al finalizar la ejecucion del programa podemos ver que el tiempo de retorno del procesamiento de la solicitud es considerablemente
            bajo, y definitivamente más bajo que si no estuvieramos utilizando un semaforo para asegurar que no existan conflictos con los
            recursos compartidos. Previamente sin usar semaforos los tiempos de retorno eran mucho mayores debido a que se deberia esperar a que
            terminen de administrar los procesos las demas camaras; mediante la implementacion de tareas asincronas podemos correr cada camara
            en un subproceso, cada una con colas de prioridad

            */
        }
    }
}