using EASendMail;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes
{
    class Email
    {

        private SmtpServer servidor;
        private SmtpClient  clienteSmtp;

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
            // SMTP server address
            this.servidor = new SmtpServer("smtp.gmail.com");
            // User and password for ESMTP authentication
            this.servidor.User = "davidpinedosolano@gmail.com";
            this.servidor.Password = "xkulzzobqldewgdj";
            // If your SMTP server uses 587 port
            // oServer.Port = 587;

            // If your SMTP server requires SSL/TLS connection on 25/587/465 port
            // oServer.Port = 25; // 25 or 587 or 465
            // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            this.clienteSmtp = new SmtpClient();
            this.servidor.Port = 465;
        }

        public Boolean sendEmail(string correoDestino, string motivoMensaje, string mensaje)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Set sender email address, please change it to yours
                oMail.From = "davidpinedosolano@gmail.com";
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
