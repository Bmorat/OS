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


        
    }
}