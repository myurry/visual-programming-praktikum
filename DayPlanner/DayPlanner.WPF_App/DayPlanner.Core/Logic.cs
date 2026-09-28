using System;
using System.Globalization;
using static DayPlanner.Core.Errors;

namespace DayPlanner.Core
{
    public static class Logic
    {
        public static bool TryValidateNote(string text, string timeText, out TimeSpan time, out string errorMessage)
        {
            time = default;
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(text))
            {
                errorMessage = Errors.TextIsEmptyMessage; 
                return false;
            }

            var trimmed = (timeText ?? string.Empty).Trim();
            if (!TimeSpan.TryParseExact(trimmed, @"h\:mm", CultureInfo.InvariantCulture, out var t) &&
                !TimeSpan.TryParseExact(trimmed, @"hh\:mm", CultureInfo.InvariantCulture, out t))
            {
                // Pull message from resx
                errorMessage = Errors.InvalidTimeErrorMessage;
                return false;
            }

            if (t < TimeSpan.Zero || t >= TimeSpan.FromHours(24))
            {
                errorMessage = Errors.TimeOutOfRangeMessage; // optional resource key
                return false;
            }

            time = t;
            return true;
        }
    }
}