namespace Library
{
    public class ControlAcceso
    {
        public Thread CamaraEntrada { get; set; }
        public Thread CamaraCantina { get; set; }
        public Thread CamaraPasillo { get; set; }

        public void IniciarReconocimiento()
        {
            
        }
    }
}