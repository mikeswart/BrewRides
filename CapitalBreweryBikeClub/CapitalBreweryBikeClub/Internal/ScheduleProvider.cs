using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Hosting;

namespace CapitalBreweryBikeClub.Internal
{
    // public sealed class ScheduleProvider
    // {
    //     private readonly List<DailyRouteSchedule> schedules = new List<DailyRouteSchedule>();
    //     private readonly LocalJsonFile<ScheduleData> fileCache;

    //     public ScheduleProvider(IWebHostEnvironment webHostEnvironment, RouteProvider routeProvider)
    //     {
    //         fileCache = new LocalJsonFile<ScheduleData>(webHostEnvironment.ContentRootFileProvider, "schedule.json");
    //         if (!fileCache.CacheExists())
    //         {
    //             return;
    //         }

    //         schedules = fileCache.Load().Result?.Select(scheduleData => scheduleData.ToDailyRouteSchedule(routeProvider)).ToList() ?? new List<DailyRouteSchedule>();
    //     }

    //     public IEnumerable<DailyRouteSchedule> Get(DateTime beginTime, TimeSpan timeSpan)
    //     {
    //         var endTime = beginTime.Add(timeSpan);
    //         return schedules
    //          .OrderBy(schedule => schedule.DateTime)
    //          .Where(schedule => schedule.DateTime >= beginTime && schedule.DateTime < endTime);
    //     }

    //     public void AddOrReplace(DailyRouteSchedule schedule)
    //     {
    //         schedules.RemoveAll(routeSchedule => routeSchedule.DateTime.Date == schedule.DateTime.Date);
    //         schedules.Add(schedule);
    //         fileCache.Save(schedules.Select(ScheduleData.FromDailyRouteSchedule));
    //     }

    //     [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    //     private sealed class ScheduleData
    //     {
    //         public DateTime Date { get; set; }
    //         public string RouteName { get; set; }

    //         public DailyRouteSchedule ToDailyRouteSchedule(RouteProvider routeProvider)

    //             routeProvider.Routes.TryGetValue(RouteName, out var routeInfo);
    //             return new DailyRouteSchedule(Date, routeInfo);
    //         }

    //         public static ScheduleData FromDailyRouteSchedule(DailyRouteSchedule source)
    //         {
    //             return new ScheduleData { Date = source.DateTime, RouteName = source.Route.Name };
    //         }
    //     }
    // }
}
