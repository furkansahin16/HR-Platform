namespace Business.Helper.PermissionHelper
{
    public static class AnnualPermissionHelper
    {
        /// <summary>
        /// Çalışmaya başlama tarihine göre günümüze göre çalışılan yılları hesaplar.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static int GetWorkYears(DateTime startTime)
        {
            return DateTime.Now.Year - startTime.Year;
        }

        /// <summary>
        /// Çalışılan toplam yıla göre çalışanın sahip olduğu yıllık izin hakkını hesaplar.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static int GetAnnualPermissionDay(DateTime startTime)
        {
            int workYears = DateTime.Now.Year - startTime.Year;

            if (workYears < 5)
                return workYears * 14;
            else if (workYears < 15)
                return (5 * 14) + ((workYears - 5) * 24);
            else
                return (5 * 14) + (10 * 24) + ((workYears - 15) * 26);
        }

        /// <summary>
        /// Çalışan yeni bir yılı tamamladığında yeni eklenecek yıllık izin haklarını hesaplar.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="workYears"></param>
        /// <param name="currentPermission"></param>
        /// <returns></returns>
        public static (int, int) UpdateAnnualPermissionDay(DateTime startTime, int workYears, int currentPermission)
        {
            int newWorkTime = GetWorkYears(startTime);
            if (newWorkTime != workYears)
            {
                int addWorkYear = newWorkTime - workYears;
                workYears = newWorkTime;
                if (workYears < 4)
                    currentPermission += addWorkYear * 14;
                else if (workYears < 14)
                    currentPermission += addWorkYear * 24;
                else
                    currentPermission += addWorkYear * 26;
                return (workYears, currentPermission);
            }
            return (workYears, currentPermission);
        }
        
        /// <summary>
        /// Verilen iki tarih arasındaki iş günleri sayısını hesaplar.
        /// </summary>
        /// <param name="firstDay"></param>
        /// <param name="lastDay"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GetWorkingDays(DateTime firstDay, DateTime lastDay)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            return businessDays;
        }
    }
}
