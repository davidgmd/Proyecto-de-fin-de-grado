using EASendMail;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    class Email
    {

        private SmtpServer servidor;
        private SmtpClient  clienteSmtp;
        private string[] configuracionemail;

        public string[] ConfiguracionEmail
        {
            get { return configuracionemail; }
            set { configuracionemail = value; }
        }


        public SmtpClient  ClienteSmtp
        {
            get { return clienteSmtp; }
            set { clienteSmtp = value; }
        }


        public SmtpServer Servidor
        {
            get { return servidor; }
            set { servidor = value; }
        }

        public Email()
        {
            //cambiar a .user al finalizar las pruebas, para que no se suba al repositorio
            //Se leen todos los datos linea por linea del fichero
            var path = RecursosAplicacion.DireccionBase + "emailcredentials.txt";
            this.ConfiguracionEmail = File.ReadAllLines(path, Encoding.UTF8);

            //configuramos los datos del correo
            // SMTP server address
            this.servidor = new SmtpServer(this.ConfiguracionEmail[0]);
            // User and password for ESMTP authentication
            this.servidor.User = this.ConfiguracionEmail[1];
            this.servidor.Password = this.ConfiguracionEmail[2];
            // If your SMTP server uses 587 port
            // oServer.Port = 587;

            // If your SMTP server requires SSL/TLS connection on 25/587/465 port
            // oServer.Port = 25; // 25 or 587 or 465
            // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            this.clienteSmtp = new SmtpClient();
            this.servidor.Port = 465;
        }

        public Boolean SendEmail(string correoDestino, string motivoMensaje, string mensaje)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Set sender email address, please change it to yours
                oMail.From = this.ConfiguracionEmail[1];
                // Set recipient email address, please change it to yours
                oMail.To = correoDestino;

                // Set email subject
                oMail.Subject = motivoMensaje;
                // Set email body
                oMail.TextBody = mensaje;

                // Most mordern SMTP servers require SSL/TLS connection now.
                // ConnectTryTLS means if server supports SSL/TLS, SSL/TLS will be used automatically.
                //this.servidor.ConnectType = SmtpConnectType.ConnectTryTLS;
                this.servidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

                //La funcion de testeo no funciona adecuadamente, esta parte es imposible testear sin servidor
                //this.clienteSmtp.TestRecipients(null, oMail);
                this.clienteSmtp.SendMail(this.servidor, oMail);
                return true;
            }
            catch (Exception ep)
            {
                //Error en el mensaje
                return false;
            }
        }
    }
}
