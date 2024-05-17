using System.Diagnostics;

namespace Library
{
    public class ControlAcceso
    {
        public Thread CamaraEntrada { get; set; }
        public Thread CamaraCantina { get; set; }
        public Thread CamaraPasillo { get; set; }
        private bool stop = false;
        private int contador=0;

        public ControlAcceso()
        {
            CamaraEntrada = new Thread(() => IniciarReconocimiento("CamaraEntrada"));
            CamaraCantina = new Thread(() => IniciarReconocimiento("CamaraCantina"));
            CamaraPasillo = new Thread(() => IniciarReconocimiento("CamaraPasillo"));
        }

         public void IniciarReconocimiento(string camara)
        {
            while (!stop)
            {
                Person persona = Person.GetRandomPerson();
                if (persona.Estado.Equals("Autorizado"))
                {
                    Console.WriteLine($"{camara}: Persona reconocida: {persona.Nombre} {persona.Apellido}");
                }
                else
                {
                    Console.WriteLine($"{camara}: Persona no reconocida: {persona.Nombre} {persona.Apellido}, por favor vaya a registro de usuarios.");
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
    }
}