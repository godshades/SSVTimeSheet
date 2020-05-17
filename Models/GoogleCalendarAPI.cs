using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace GoogleCalendarAPI.Models
{
    public class TimeSheetEvent
    {
        public string id { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        // Additional fields

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
    public class GoogleCalendar
    {
        static string CalendarId = "nghiepdocntt1k58utc@gmail.com";

        ServiceAccountCredential GetCredential()
        {
            try
            {
                var certificate = new X509Certificate2(
            "ssvtimesheet-277509-ea53330344b6.p12",
            "notasecret",
             X509KeyStorageFlags.Exportable);

                ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer("ssvtimesheet@ssvtimesheet-277509.iam.gserviceaccount.com")
                {
                    Scopes = new[] { CalendarService.Scope.Calendar }
                }.FromCertificate(certificate));

                return credential;
            }
            catch (Exception)
            {
                return null;
            }
        }
        CalendarService Get()
        {
            try
            {
                var credential = GetCredential();
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "SSVTimeSheet"
                });

                return service;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Creates a new event 
        /// for more information see https://developers.google.com/google-apps/calendar/v3/reference/events/insert
        /// </summary>
        /// <param name="gEvent">Google event object</param>
        /// <returns></returns>
        public string CreateEvent(TimeSheetEvent gEvent)
        {
            //try
            //{
            using (var service = Get())
            {
                var inserted = service.Events.Insert(new Event
                {
                    Id = gEvent.id,
                    Description = gEvent.description,
                    Summary = gEvent.text,
                    Created = gEvent.Created,
                    Updated = gEvent.Updated,
                    Start = new EventDateTime { DateTimeRaw = gEvent.start_date.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "+07:00" },
                    End = new EventDateTime { DateTimeRaw = gEvent.end_date.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "+07:00" }
                }, CalendarId).Execute();
                return inserted.Id;
            }
            //}
            //catch (Exception)
            //{
            //    return string.Empty;
            //}
        }
        /// <summary>
        /// Updates event in the calendar with the identifier CalendarId
        /// for more information see https://developers.google.com/google-apps/calendar/v3/reference/events/update
        /// </summary>
        /// <param name="gEvent">Google event object</param>
        /// <returns></returns>
        public string UpdateEvent(TimeSheetEvent gEvent)
        {
            try
            {
                using (var service = Get())
                {
                    var toUpdate = service.Events.Get(CalendarId, gEvent.id).Execute();
                    toUpdate.Description = gEvent.description;
                    toUpdate.Summary = gEvent.text;
                    toUpdate.Created = gEvent.Created;
                    toUpdate.Updated = gEvent.Updated;
                    toUpdate.Start = new EventDateTime { DateTimeRaw = gEvent.start_date.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "+07:00" };
                    toUpdate.End = new EventDateTime { DateTimeRaw = gEvent.end_date.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "+07:00" };
                    var result = service.Events.Update(toUpdate, CalendarId, toUpdate.Id).Execute();
                    return result.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Deletes event by eventId from the calendar with CalendarId
        /// for more information see https://developers.google.com/google-apps/calendar/v3/reference/events/delete
        /// </summary>
        /// <param name="eventId">Identifier of the event</param>
        public void DeleteEvent(string eventId)
        {
            try
            {
                using (var service = Get())
                {
                    service.Events.Delete(CalendarId, eventId).Execute();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}