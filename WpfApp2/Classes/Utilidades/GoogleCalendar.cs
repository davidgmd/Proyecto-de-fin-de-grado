using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ElEscribaDelDJ.Classes.Utilidades
{
    class GoogleCalendar
    {
        string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        string ApplicationName = "ElEscribaDelDJ";
        private CalendarService service;

        public CalendarService Service
        {
            get { return service; }
            set { service = value; }
        }


        public GoogleCalendar()
        {
            UserCredential credential;

            using (var stream =
                new FileStream(RecursosAplicacion.DireccionBase + "googlecalendarcredentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            this.service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });      
        }

        public Events GetEvents(DateTime fecha)
        {
            // Define parameters of request.
            var calendarList = service.CalendarList.List().Execute();
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = fecha;
            //request.TimeMin = DateTime.Now;
            //request.ShowDeleted = false;
            //request.SingleEvents = true;
            //request.MaxResults = 10;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            return events;
        }
    }
}
