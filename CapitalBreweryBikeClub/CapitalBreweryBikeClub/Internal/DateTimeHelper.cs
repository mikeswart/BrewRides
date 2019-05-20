using System;

namespace CapitalBreweryBikeClub.Internal
{
    internal sealed class DateTimeHelper
    {
        public static DateTime GetBeginningOfWeek(DateTime dateTime)
        {
            var currentTime = dateTime;
            while (currentTime.DayOfWeek != DayOfWeek.Sunday)
            {
                currentTime = currentTime.AddDays(-1);
            }

            return currentTime;
        }
    }
}