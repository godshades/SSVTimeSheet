using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleCalendarAPI.Models
{
    public class GoogleCalendar
    {
        static string jsonFile = "credentials.json";
        static string calendarId = @"quangquyen1410@gmail.com";

        static string[] Scopes = { CalendarService.Scope.Calendar };
        public ServiceAccountCredential credential;
        public GoogleCalendar()
        {
            using (var stream = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
            }
        }

        public CalendarService GetService()
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SSVTimeSheet",
            });
            return service;
        }
        public void CreateEvent()
        {
            CalendarService service = GetService();
            var myevent = DB.Find(x => x.Id == "eventid" + 1);

            var InsertRequest = service.Events.Insert(myevent, calendarId);

            //try
            //{
            InsertRequest.Execute();
            //}
            //catch (Exception)
            //{
            //    try
            //    {
            //        service.Events.Update(myevent, calendarId, myevent.Id).Execute();

            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
        }

        public static List<Event> DB =
             new List<Event>() {
                new Event(){
                    Id = "eventid" + 1,
                    Summary = "Google I/O 2015",
                    Location = "800 Howard St., San Francisco, CA 94103",
                    Description = "A chance to hear more about Google's developer products.",
                    Start = new EventDateTime()
                    {
                        DateTime = new DateTime(2020, 04, 15, 15, 30, 0),
                        TimeZone = "America/Los_Angeles",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = new DateTime(2020, 04, 16, 15, 30, 0),
                        TimeZone = "America/Los_Angeles",
                    },
                    Attendees = new List<EventAttendee>
                    {
                        new EventAttendee() { Email = "quangquyen1410@gmail.com"},
                        new EventAttendee() { Email = "quangquyen1410@gmail.com"}
                    }
                }
             };

    }
}