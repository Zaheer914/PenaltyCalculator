using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenaltyCalculator.Controllers
{
    public class Countries
    {
        public string checkedDate;

        public string returnDate;

        public string publicDays;

        public string countryName;

        public int workingDays;

        public List<string> GetSpecialHoliday(string countryName)
        {
            string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                List<string> specialHolidays = new List<string>();

                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("getDetails", con);
                    command.Parameters.AddWithValue("@country", SqlDbType.Int).Value = countryName;
                    command.CommandType = CommandType.StoredProcedure;
                    //get data by using stored procedure
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (specialHolidays.Contains(dataReader["SpecialDate"].ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            specialHolidays.Add(dataReader["SpecialDate"].ToString());
                        }

                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                return specialHolidays;
            }
        }
        public List<string> GetHoliday(string countryName)
        {
            string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                List<string> holidays = new List<string>();

                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("getDetails", con);
                    command.Parameters.AddWithValue("@country", SqlDbType.Int).Value = countryName;
                    command.CommandType = CommandType.StoredProcedure;
                    //get data by using stored procedure
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (holidays.Contains(dataReader["specialholiday"].ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            holidays.Add(dataReader["specialholiday"].ToString());
                        }

                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                return holidays;
            }
        }

        public int WorkingDaysWithoutSpecialHoliday(string issueDate, string returnBackDate, string countryName)
        {
            List<string> weekholiday = GetHoliday(countryName);
            int daysWithoutSpecialHoliday = WorkingDays(weekholiday, issueDate, returnBackDate);

            return daysWithoutSpecialHoliday;
        }

        public int WorkingDays(List<string> weekholiday ,string issueDate ,string returnBackDate)
        {
            DateTime checkedDate = Convert.ToDateTime(issueDate);
            DateTime returnDate = Convert.ToDateTime(returnBackDate);
            for (var date = checkedDate; date <= returnDate; date = date.AddDays(1))
            {
                    if (date.DayOfWeek.ToString() != weekholiday[0] && date.DayOfWeek.ToString() != weekholiday[1])
                    {
                        workingDays++;
                    }
                

            }
            return workingDays;
        }

        public int TotalSpecialHoliday(string countryName)
        {
            List<string> weekholiday = GetHoliday(countryName);
            List<string> specialHoliday = GetSpecialHoliday(countryName);
            int totalSpecialHoliDays = SpecialHoliDaysTotal(weekholiday, specialHoliday);
            return totalSpecialHoliDays;
        }
        public int SpecialHoliDaysTotal(List<string> weekholiday,List<string> specialHoliday)
        {
            var totalSpecialHoliday = 0;
            for (int i = 0; i < specialHoliday.Count; i++)
            {
                DateTime specialDate = Convert.ToDateTime(specialHoliday[i]);
                
                if (specialDate.DayOfWeek.ToString() != weekholiday[0] && specialDate.DayOfWeek.ToString() != weekholiday[1])
                {
                    totalSpecialHoliday++;
                }
            }
            return totalSpecialHoliday;
                
        }

        public int SpecialOffBetweenDates(string countryName, string issueDate, string returnDate)
        {
            List<string> weekholiday = GetHoliday(countryName);
            List<string> specialHoliday = GetSpecialHoliday(countryName);

            return SpecialHoliDaysBetweenDates(weekholiday, specialHoliday, issueDate, returnDate);
        }
        public int SpecialHoliDaysBetweenDates(List<string> weekholiday, List<string> specialHoliday,string issueDate,string returnDate)
        {
            DateTime issuedDate = Convert.ToDateTime(issueDate);
            DateTime returnedDate = Convert.ToDateTime(returnDate);
            var specialHolidayBetweenDates = 0;
            for (int i = 0; i < specialHoliday.Count; i++)
            {
                DateTime specialDate = Convert.ToDateTime(specialHoliday[i]);
                if (specialDate >= issuedDate && specialDate <= returnedDate)
                {
                    if (specialDate.DayOfWeek.ToString() != weekholiday[0] && specialDate.DayOfWeek.ToString() != weekholiday[1])
                    {
                        specialHolidayBetweenDates++;
                    }
                }
                
            }
            return specialHolidayBetweenDates;

        }

        public int GetBussinessDays(string issueDate, string returnBackDate, string countryName)
        {
          
          int noOfWorkingDays = WorkingDaysWithoutSpecialHoliday(issueDate, returnBackDate, countryName);
          int totalSpecialHoliday = TotalSpecialHoliday(countryName);
          int specialDaysbtwDates = SpecialOffBetweenDates(countryName, issueDate, returnBackDate);

          DateTime issuedDate = Convert.ToDateTime(issueDate);
          DateTime returnedDate = Convert.ToDateTime(returnBackDate);
          int diffofYear = returnedDate.Year - issuedDate.Year;

          int bussinessDays = noOfWorkingDays - (specialDaysbtwDates + totalSpecialHoliday * diffofYear);
          return bussinessDays;
        }

    }
}
