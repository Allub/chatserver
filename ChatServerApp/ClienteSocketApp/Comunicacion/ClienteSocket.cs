using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp.Comunicacion
{
    public class ClienteSocket
    {
        private string ip;
        private int puerto;
        private Socket comServidor;
        private StreamReader reader;
        private StreamWriter writer;
        /*Constructor que recibe el ip y puerto*/
        public ClienteSocket(string ip, int puerto)
        {
            this.puerto = puerto;
            this.ip = ip;
        }

        public Boolean Conectar()
        {
            
            try
            {
                this.comServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                /*se convierte ip en formato String a objeto IpAdress*/
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), puerto);
                /*se genera la conexion */
                this.comServidor.Connect(endpoint);
                /*Con esto se pueden crear el Reader Y Writer*/
                Stream stream = new NetworkStream(this.comServidor);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);
                return true;
            }
            catch (IOException ex)
            {

                return false;
            }
        }
        /*Permite mandar mensajes al servidor*/
        public Boolean Escribir(String mensaje)
        {
            try
            {
                /*Metodos para escribir mensajes al servidor*/
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (IOException ex)
            {

                return false;
            }
        }
        /*Permite recibir valor desde el servidor*/
        public String Leer()
        {
            try
            {
                /*Metodo para leer mensaje del servidor*/
                return this.reader.ReadLine().Trim();
            }
            catch (IOException ex)
            {
                //return null porque es String
                return null;
            }
        }
        /*Permite cerrar la conexion con el servidor*/
        public void Desconectar()
        {
            this.comServidor.Close();
        }
    }
}
