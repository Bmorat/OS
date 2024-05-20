using System.Text.Json;
using System.Text.Json.Serialization;

namespace Library;

public class Person
{
    public string Nombre{get; set;}
    public string Apellido{get; set;}
    public string Tipo{get; set;}
    public string Estado{get; set;}
    public bool Vip{get; set;}
    public int Id{get; set;}
    private static List <Person> Lista = new List<Person>();
    public Person(string name, string apellido, string type, string estado, bool vip,int id)
    {
        Nombre=name;
        Apellido=apellido;
        Tipo=type;
        Estado=estado;
        Vip=vip;
        Id=id;
    }
    
    [JsonConstructor]
    public Person() { }

    public static Person GetRandomPerson()
    {
        string filePath = @"C:\Users\alfon\OneDrive\Escritorio\OS\src\personas.json";
        string jsonStr = File.ReadAllText(filePath);
        List<Person> personas = JsonSerializer.Deserialize<List<Person>>(jsonStr);        
        Random random = new Random();
        int randomIndex = random.Next(personas.Count);
        Person randomPerson=personas[randomIndex];
        if (Lista.Contains(randomPerson))
        {
            randomIndex = random.Next(personas.Count);
            randomPerson = personas[randomIndex];
        }
        else 
        {
            Lista.Add(randomPerson);
        }
        return randomPerson;
    }
}
