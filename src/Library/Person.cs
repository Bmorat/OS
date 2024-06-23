namespace Library;

public class Person
{
    public string Nombre{get; set;}
    public string Apellido{get; set;}
    public string Tipo{get; set;}
    public bool Autorizado{get; set;}
    public int Id{get; set;}
    private static List <Person> Lista = new List<Person>();
    public Person(string name, string apellido, string type, bool autorizado,int id)
    {
        Nombre = name;
        Apellido = apellido;
        Tipo = type;
        Autorizado = autorizado;
        Id = id;
    }
}
