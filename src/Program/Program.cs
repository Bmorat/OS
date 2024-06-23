using Library;

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
            Person sergio = new Person("Sergio", "Rochet", "Alumno", true, 30123456);
            Person facundo = new Person("Facundo", "Pellistri", "Alumno", false, 41234567);
            Person nicolas = new Person("Nicolás", "Lopez", "Alumno", true, 52345678);
            Person gustavo = new Person("Gustavo", "Viera", "Alumno", false, 63456789);
            Person jonathan = new Person("Jonathan", "Urretaviscaya", "Alumno", true, 74567890);
            Person jorge = new Person("Jorge", "Fucile", "Profesor", true, 85678901);
            Person oliveros = new Person("Agustín", "Oliveros", "Profesor", false, 96789012);
            Person sebastian = new Person("Sebastián", "Sosa", "Profesor", true, 10890123);
            Person christian = new Person("Christian", "Rodriguez", "Funcionario", true, 11901234);
            Person martin = new Person("Martín", "Cáceres", "Funcionario", false, 13012345);
            Person maximiliano = new Person("Maximiliano", "Falcón", "Alumno", true, 14123456);
            Person torres = new Person("Facundo", "Torres", "Alumno", false, 15234567);
            Person pablo = new Person("Pablo", "Garcia", "Alumno", true, 16345678);
            Person diego = new Person("Diego", "Polenta", "Alumno", false, 17456789);
            Person emiliano = new Person("Emiliano", "Martínez", "Alumno", true, 18567890);
            Person leandro = new Person("Leandro", "Fernández", "Profesor", true, 19678901);
            Person damian = new Person("Damián", "Macaluso", "Profesor", false, 20789012);
            Person ignacio = new Person("Ignacio", "Lores", "Profesor", true, 21890123);
            Person rodrigo = new Person("Rodrigo", "Pérez", "Funcionario", true, 22901234);
            Person federico = new Person("Federico", "Rodriguez", "Funcionario", false, 24012345);
            
            // Defino una lista de personas autorizadas para entrar a la universidad.
            var personasAutorizadas = new HashSet<Person> { brian, alfonso, julio, ingrid, franyer, carlos, wanchope, sergio, facundo, nicolas, gustavo, jonathan, jorge, oliveros, sebastian, christian, martin, maximiliano, torres, pablo, diego, emiliano, leandro, damian, ignacio, rodrigo, federico};
            Random rand = new Random();         // Creo un objeto de tipo Random para generar numeros aleatorios.   
           
            /*
            Creo la lista de solicitudes para entrar a la universidad con sus caracteristicas (la persona, la prioridad de la solicitud definida de 
            forma arbitraria para la simulacion, el tiempo de analisis, la camara por la cual se estaria captando y la puerta por la que entra la persona).
            */

            var solicitudes = new List<SolicitudAcceso>
            {
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = alfonso, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = agustin, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = franyer, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = ingrid, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = julio, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = wanchope, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = carlos, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = sergio, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = facundo, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = nicolas, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = gustavo, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = jonathan, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = jorge, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = agustin, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = sebastian, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = christian, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = martin, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = maximiliano, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = torres, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = pablo, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = diego, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = emiliano, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = leandro, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = damian, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = ignacio, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = rodrigo, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = federico, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = brian, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = franyer, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = rodrigo, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = federico, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() },
                new SolicitudAcceso { Prioridad = rand.Next(1, 8), Persona = wanchope, TiempoDeAnalisis = rand.Next(200, 1601), CamaraId = rand.Next(1, 4), Puerta = ((char)rand.Next('A', 'D')).ToString() }
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