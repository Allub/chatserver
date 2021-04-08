using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp.Comunicacion
{
    public class ServerSocket
    {
        /*Atributo del puerto*/
        private int puerto;

        /*Se define como atributo variable de tipo Socket que sera el servidor*/
        private Socket servidor;

        /*Se define como atributo un socket que represente la comunicacion activa con el cliente*/
        private Socket comCliente;

        /*instancias que necesito para cuando se reciba el cliente
         leer desde el cliente
         y escribir al cliente*/
        private StreamReader reader;
        private StreamWriter writer;

        /*Constructor que recibe el puerto*/
        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        /*Levanta la conexion*/
        public Boolean Iniciar()
        {
            /*se hace boolean porque pude explotar*/
            try
            {
                //1. Crear Socket
                /*se crea instancia
                 Que tipo de conexion de red soporta, de que tipo es el socket, protocolo*/
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //2 Tomar control del puerto
                /*recibe un EndPoint
                 recibe IPAddress que reprecenta las direcciones desde las cual se podran conectar a este socket
                 y recibe el puerto*/
                /*si esta linea no pasa
                 cae en excepsion como false y el inicio va a dar error*/
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));

                //3. Definir cuantos clientes atenderé
                /*cuando la aplicacion es concurrente tiene sentido poner 10*/
                this.servidor.Listen(10);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        /*Obtiene el cliente*/
        public bool ObtenerCliente()
        {
            try
            {
                /*recibo la comunicacion con cliente y lo guardo aca
                 se queda esperando hasta que llegue un cliente*/
                this.comCliente = this.servidor.Accept();

                /*stream se crea a partir del socket
                 La conexion entre cliente y servidor se llama comCliente de tipo socket
                 la carretera para enviar los datos de un extremo a otro es el Stream
                 en esa carretera necesito lo que manda la info de un extremo a otro
                 estos se llaman StreamReader y StreamWriter*/
                /*Stream(Socket)
                 SW(Stream)
                 SR(Stream)*/
                Stream stream = new NetworkStream(this.comCliente);
                /*stream se le pasa al Writer y Reader*/
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        /*Permite mandar el mensaje al cliente*/
        public bool Escribir(String mensaje)
        {

            try
            {
                /*Metodos para escribir mensajes al cliente*/
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (IOException ex)
            {

                return false;
            }

        }
        /*Permite recibir valor del cliente*/
        public String Leer()
        {
            try
            {
                /*metodo para leer mensajes del cliente*/
                return this.reader.ReadLine().Trim();
            }
            catch (IOException ex)
            {
                //return null porque es string
                return null;
            }

        }
        /*Permite cerrar conexion del cliente*/
        public void CerrarConexion()
        {
            this.comCliente.Close();

        }
    }
}
