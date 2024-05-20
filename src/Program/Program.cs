using System.Text.Json.Serialization;
using Library;
using System.Text.Json;


namespace Program;

class Program
{
    static void Main(string[] args)
    {
        ControlAcceso controlAcceso = new ControlAcceso();
        controlAcceso.IniciarSistema();       
        /*Person persona1=Person.GetRandomPerson();
        Console.WriteLine($"hola soy {persona1.Nombre} {persona1.Apellido} mi id es {persona1.Id} y estoy {persona1.Estado}");*/  

        Escenarios escenarios = new Escenarios();
        Console.WriteLine("Simulación de Gestión de Inscripciones");
        // Ejecutar diferentes escenarios en paralelo utilizando Task.Run
        Task.Run(() => controlAcceso.IniciarReconocimientoFacial());
        Task.Run(() => controlAcceso.IniciarVerificacionAcceso());
        Task.Run(() => escenarios.SeasonalScenario("Inicio del semestre", 5000));       // Duración de 5 segundos
        Task.Run(() => escenarios.SeasonalScenario("Período de inscripciones", 8000));  // Duración de 8 segundos
        Task.Run(() => escenarios.OneTimeScenario("Evento especial", 3000));            // Duración de 3 segundos
    
        Task.WaitAll(); // Esperar a que todas las tareas terminen
        Console.WriteLine("Simulación completada.");
    }
}