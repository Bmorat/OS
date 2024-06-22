using System;

public class SolicitudAcceso : IComparable<SolicitudAcceso>
{
    public int Prioridad { get; set; }
    public string Nombre { get; set; }
    public DateTime TiempoDeLlegada { get; set; }
    public int TiempoDeAnalisis { get; set; }
    public TimeSpan TiempoDeEspera => DateTime.Now - TiempoDeLlegada;
    public TimeSpan TiempoDeRetorno { get; set; }
    public string Descripcion { get; set; }

    public int CompareTo(SolicitudAcceso other)
    {
        // Ordenar por prioridad (mayor prioridad primero)
        return other.Prioridad.CompareTo(this.Prioridad);
    }
}