namespace Library
{
    public class SolicitudAcceso : IComparable<SolicitudAcceso>
    {
        public int Prioridad { get; set; }
        public Person Persona { get; set; }
        public DateTime TiempoDeLlegada { get; set; }
        public int TiempoDeAnalisis { get; set; }
        public TimeSpan TiempoDeEspera => DateTime.Now - TiempoDeLlegada;
        public TimeSpan TiempoDeRetorno { get; set; }
        public int CamaraId { get; set; }
        public string Puerta { get; set; }

        public int CompareTo(SolicitudAcceso other)
        {
            // Ordenar por prioridad (mayor prioridad primero)
            return other.Prioridad.CompareTo(this.Prioridad);
        }
    }
}