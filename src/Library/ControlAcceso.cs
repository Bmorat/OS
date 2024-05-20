using System.Diagnostics;

namespace Library
{
    public class ControlAcceso
    {
        public Thread CamaraEntrada { get; set; }
        public Thread CamaraCantina { get; set; }
        public Thread CamaraPasillo { get; set; }
        public Thread ReconocimientoFacial { get; set; }
        public Thread VerificacionAcceso { get; set; }
        private bool stop = false;
        private int contador = 0;

        public ControlAcceso()
        {
            CamaraEntrada = new Thread(() => IniciarReconocimiento("CamaraEntrada"));
            CamaraCantina = new Thread(() => IniciarReconocimiento("CamaraCantina"));
            CamaraPasillo = new Thread(() => IniciarReconocimiento("CamaraPasillo"));
            ReconocimientoFacial = new Thread(() => IniciarReconocimiento("ReconocimientoFacial"));
            VerificacionAcceso = new Thread(() => IniciarReconocimiento("VerificacionAcceso"));
        }

        public void IniciarReconocimiento(string camara)
        {
            while (!stop)
            {
                Person persona = Person.GetRandomPerson();
                IniciarReconocimientoFacial();
                if (persona.Estado.Equals("Autorizado"))
                {
                    Console.WriteLine($"{camara}: Persona reconocida: {persona.Nombre} {persona.Apellido}");
                }
                else
                {
                    Console.WriteLine($"{camara}: Persona no reconocida: {persona.Nombre} {persona.Apellido}, por favor vaya a registro de usuarios.");
                    IniciarVerificacionAcceso();
                    RegistrarUsuario();
                }

                if (persona.Tipo.Equals("Discapacitado"))
                {
                    Console.WriteLine($"{camara}: Alerta: Persona discapacitada detectada: {persona.Nombre} {persona.Apellido}");
                }
                else if (persona.Tipo.Equals("Embarazada"))
                {
                    Console.WriteLine($"{camara}: Alerta: Mujer embarazada detectada: {persona.Nombre} {persona.Apellido}");
                }

                if (persona.Vip)
                {
                    Console.WriteLine($"{camara}: Alerta: Persona VIP detectada: {persona.Nombre} {persona.Apellido}");
                }

                contador++;
                if (contador == 30)
                {
                    StopSystem();
                }

                // delay para simular
                Thread.Sleep(3000);
            }
        }
        public void IniciarSistema()
        {
            CamaraEntrada.Start();
            CamaraCantina.Start();
            CamaraPasillo.Start();
        }
        public void StopSystem()
        {
            this.stop=true;
        }
        public void IniciarReconocimientoFacial()
        {
            IniciarVerificacionAcceso();
            Console.WriteLine("Por Camara, \nIniciando Reconocimiento Facial...");
            Thread.Sleep(5000);
            Console.WriteLine("Completado");
            
        }
        public void IniciarVerificacionAcceso()
        {
            Console.WriteLine("Iniciando la verificación...");
            Thread.Sleep(5000);
        }
        public void RegistrarUsuario()
        {
            Thread.Sleep(4000);
            Console.WriteLine("Usuario Registrado");
        }
    }
}