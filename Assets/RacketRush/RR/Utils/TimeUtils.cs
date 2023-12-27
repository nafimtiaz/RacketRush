namespace RacketRush.RR.Utils
{
    public static class TimeUtils
    {
        public static string ToMinuteAndSecondsString(this int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // Use String.Format to format the string with leading zeros
            string formattedTime = $"{minutes:00}:{seconds:00}";

            return formattedTime;
        }
    }
}