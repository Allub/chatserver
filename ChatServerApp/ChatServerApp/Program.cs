using ChatServerApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*dentro del ConfiguratiionManager encontramos el AppSettings
             es equivalente a la etiqueta de AppSetting
             este atributo tiene como llaves las llaves que definí en AppConfig
             puerto representa el texto que puse en la llave
             da el valor 2000 pero en string, entonces se convierte en entero*/
            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);

            /*mensaje mostrado en consola
             {} entre llaves va el puerto*/
            Console.WriteLine("Iniciando Servidor en puerto {0}",puerto);

            /*se levanta un objeto de tipo ServerSocket para poder levantar servidor en 
             este puerto en particular*/
            ServerSocket servidor = new ServerSocket(puerto);

            /*si es capaz de tomar control del puerto
             se obtienen los clientes*/
            if (servidor.Iniciar())
            {
                //mientras espera el cliente
                while (true)
                {
                    //esperando Cliente
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando Clientes...");
                    //cuando obtenga un cliente
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion Establecida!");
                        //Protocolo de comunicacion
                        /*Se repite el ciclo mientras el mensaje no sea chao*/
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            //leo el mensaje del cliente
                            mensaje = servidor.Leer();
                            Console.WriteLine("C:{0}", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                //El cliente espera una respuesta
                                Console.WriteLine("Digame lo que quiere decir");
                                /*Se lee nuevamente un mensaje del cliente
                                 directamente a la variable mensaje*/
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("S:{0}", mensaje);
                                servidor.Escribir(mensaje);
                            }
                        }
                        servidor.CerrarConexion();
                    }
                }
                
            }
            /*si no es capaz de tomar el control del puerto*/
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es posible iniciar servidor");
                Console.ReadKey(); //esperando a que la persona presione una tecla
            }
        }
    }
}
