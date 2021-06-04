using System;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Collections.Generic;
//using CsvHelper;
using Google.Apis.Calendar.v3.Data;


namespace EcrireEventGCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EvenementFormat> listeEventsFormat = new List<EvenementFormat>();
            listeEventsFormat = DAO.exportEvent();
            PushEvent.makePush(listeEventsFormat);

        }
    }
}
