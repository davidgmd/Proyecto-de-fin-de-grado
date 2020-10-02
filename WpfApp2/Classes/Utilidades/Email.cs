using ElEscribaDelDJ.Classes.Utilidades;
using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    class Email
    {
        private SmtpServer servidor = new SmtpServer();
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
            this.servidor.Direccion = this.ConfiguracionEmail[0];
            // User and password for ESMTP authentication
            this.servidor.Usuario = this.ConfiguracionEmail[1];
            this.servidor.Clave = this.ConfiguracionEmail[2];
            // If your SMTP server uses 587 port
            // oServer.Port = 587;

            // If your SMTP server requires SSL/TLS connection on 25/587/465 port
            // oServer.Port = 25; // 25 or 587 or 465
            // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            this.clienteSmtp = new SmtpClient();
            this.servidor.Puerto = 465;
        }

        public Boolean SendEmail(string correoDestino, string motivoMensaje, string mensaje)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("ElEscribaDelDJ", this.ConfiguracionEmail[1]));
            message.To.Add(new MailboxAddress("Usuario/a", correoDestino));
            message.Subject = motivoMensaje;
            message.Body = new TextPart("plain") { Text = mensaje };

            using (var client = new SmtpClient())
            {
                client.Connect(this.servidor.Direccion, this.servidor.Puerto);

                ////Note: only needed if the SMTP server requires authentication
                client.Authenticate(this.servidor.Usuario, this.servidor.Clave);
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }    
            }

            // Most mordern SMTP servers require SSL/TLS connection now.
            // ConnectTryTLS means if server supports SSL/TLS, SSL/TLS will be used automatically.
            //this.servidor.ConnectType = SmtpConnectType.ConnectTryTLS;
            //this.servidor.ConnectType = SmtpConnectType.ConnectSSLAuto;
        }
    }
}
