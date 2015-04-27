using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Microsoft.VisualBasic;

namespace WebApplication1.Controllers
{
    public class LoanCalculatorController : Controller
    {
        // GET: LoanCalculator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Calculate(decimal loanAmt, decimal rate, int terms)
        {
            //Sanitze loanAmt, rate, terms here would be preferred to prevent any SQL injection/XSS attacks
            //But this program does not have access to sensitive data so not necessary here.
            decimal monthlyRate = rate / 12 / 100;
            decimal interestMonthlyPayment;
            decimal principleMonthlyPayment;
            decimal principlePaid = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table border=1><th>Payment #</th><th>Payment Amount</th><th>Interest</th><th>Principal</th><th>Balance</th>");

            for (int i = 0; i < terms; i++)
            {
                interestMonthlyPayment = Math.Round(Convert.ToDecimal(Financial.IPmt((double)monthlyRate, i + 1, (double)terms, (double)-loanAmt)), 2);
                principleMonthlyPayment = Math.Round(Convert.ToDecimal(Financial.PPmt((double)monthlyRate, i + 1, (double)terms, (double)-loanAmt)), 2);
                principlePaid = principlePaid + principleMonthlyPayment;
                sb.Append("<tr>");

                sb.AppendFormat("<td>{0}</td>", i + 1);
                sb.AppendFormat("<td>{0}</td>", interestMonthlyPayment + principleMonthlyPayment);
                sb.AppendFormat("<td>{0}</td>", interestMonthlyPayment);
                sb.AppendFormat("<td>{0}</td>", principleMonthlyPayment);
                if ((i + 1) == terms)
                {
                    sb.AppendFormat("<td>0</td>");
                }
                else
                {
                    sb.AppendFormat("<td>{0}</td>", Math.Round(loanAmt - principlePaid, 2));
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }

    }
}