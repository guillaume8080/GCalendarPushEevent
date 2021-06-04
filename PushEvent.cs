using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using RingCentral.Net.Events;


namespace EcrireEventGCalendar
{
    class PushEvent
    {
        List<Event> listToPush = new List<Event>();

        public static void makePush(List<EvenementFormat> listeEventsFormat)
        {
            //string dateSortieDataSet = "20/05/2021 14:00:00";
            UserCredential credentials;
            string calendarId = "vrp.cotefenetres@gmail.com";

            credentials = Authentification.makeAuthentication();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "GCalendarWrite",
            });

            for (int i = 0; i < listeEventsFormat.Count(); i++)
            {
                Event eventCalendar = new Event();

                EventDateTime eventDateTime = new EventDateTime();
                eventDateTime.Date = listeEventsFormat.ElementAt(i).Debut;
                eventCalendar.Start = eventDateTime;

                string dateTimeString = "2021/05/20 15:00:00";
                EventDateTime eventDateTimeFin = new EventDateTime();
                eventDateTimeFin.Date = dateTimeString;
                eventCalendar.End = eventDateTimeFin;

                eventCalendar.Summary = listeEventsFormat.ElementAt(i).intitule;
                eventCalendar.Description = listeEventsFormat.ElementAt(i).descritpion;
                eventCalendar.Location = listeEventsFormat.ElementAt(i).adresse;

                //Post sur l'API Google
                try
                {
                    //EUROPE
                    Event recurringEvent = service.Events.Insert(eventCalendar, calendarId).Execute();
                    

                    //INDIA
                    /*EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, eventCalendar, calendarId);
                    Event response = request.Execute();*/




                }
                catch (Exception ex)
                {
                    Console.WriteLine("erreur:" + ex);
                }


            }
            




        }

    }
}
