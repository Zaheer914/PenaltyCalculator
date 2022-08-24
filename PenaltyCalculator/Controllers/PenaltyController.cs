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
  public class PenaltyController : ApiController
  {
    public double tax = 0.08;
    [Route("Publicday")]
    public double GetPenalty(string issueDate, string returnBackDate, string countryName)
    {
      Countries country = new Countries();
      int bussinessDay = country.GetBussinessDays(issueDate, returnBackDate, countryName);

      double amount;
      double totalamount;
      if (bussinessDay > 10)
      {
        int penaltyDays = bussinessDay - 10;
        if (countryName == "Pakistan")
        {
          totalamount = penaltyDays * 50;

        }
        if (countryName == "Uae")
        {
          amount = penaltyDays * 50;
          totalamount = amount * 0.08 + amount;
        }

      }
      return totalamount;
    }
  }

    

}
