using System;
using System.Globalization;

namespace DRY.Task1
{
    public class InterestCalculator
    {
        private const int Age = 60;
        private const int BonusElgibleAge = 13;
        private const double SeniorPercent = 5.5d;
        private const double DefaultInterestPercent = 4.5d;

        public decimal Calculate(AccountDetails account)
        {
            return IsAccountBonusElgible(account)
                    ? CalculateInterestAmountByAge(account)
                    : default;
        }

        private bool IsAccountBonusElgible(AccountDetails account)
        {
            var duration = GetDurationBetweenDatesInYears(account.DateOfBirth, account.StartDate);

            return duration > BonusElgibleAge;
        }

        private decimal CalculateInterestAmountByAge(AccountDetails account)
        {
            var balance = account.Balance;

            var percent = GetInterestPercentByAge(account.Age);

            var duration = GetDurationBetweenStartDateAndCurrentDateInYears(account.StartDate);

            return (decimal)ApplyInterestFormula(balance, percent, duration);
        }

        private static double ApplyInterestFormula(decimal balance, double percent, double duration)
        {
            return (double)balance * duration * percent / 100;
        }

        private double GetInterestPercentByAge(int age)
        {
            return Age <= age ? SeniorPercent : DefaultInterestPercent;
        }

        private double GetDurationBetweenStartDateAndCurrentDateInYears(DateTime startDate)
        {
            return GetDurationBetweenDatesInYears(startDate, GetDateTimeNow());
        }

        private int GetDurationBetweenDatesInYears(DateTime startDate, DateTime endDate)
        {
            Calendar calendar = new GregorianCalendar();

            int dateDifferenceInYear = calendar.GetYear(endDate) - calendar.GetYear(startDate);

            if (calendar.GetDayOfYear(endDate) < calendar.GetDayOfYear(startDate))
            {
                return dateDifferenceInYear - 1;
            }

            return dateDifferenceInYear;
        }
        private static DateTime GetDateTimeNow() => DateTime.Now;

    }
}
