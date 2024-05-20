using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public class Escenarios
    {
        static SemaphoreSlim semaphore = new SemaphoreSlim(1); // Semáforo para controlar acceso a recursos compartidos
        
        public async Task SeasonalScenario(string nombre, int duracion)
        {
            Console.WriteLine($"Iniciando escenario estacional: {nombre}");
            await SimulateWork(duracion); 
            Console.WriteLine($"Escenario estacional finalizado: {nombre}");
        }

        public async Task OneTimeScenario(string nombre, int duracion)
        {
            Console.WriteLine($"Iniciando escenario puntual: {nombre}");
            await SimulateWork(duracion);  
            Console.WriteLine($"Escenario puntual finalizado: {nombre}");
        }

        public async Task SimulateWork(int duracion)
        {
            await Task.Delay(duracion); // Simular trabajo mediante un retraso
            Console.WriteLine("Trabajo completado.");
        }
    }
}
