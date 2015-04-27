using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EthosLendingInterview.Controllers
{
    public class ParagraphManipulationController : Controller
    {
        // GET: ParagraphManipulation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string ButtonClick(string button, string paragraphInput)
        {
            //Sanitze button and paragraphInput here would be preferred to prevent any SQL injection/XSS attacks
            //But this program does not have access to sensitive data so not necessary here.
            switch (button)
            {
                case "btnReverseParagraph":
                    return WebApplication1.Models.ParagraphManipulation.reverseParagraph(paragraphInput);
                case "btnReverseParagraphWords":
                    return WebApplication1.Models.ParagraphManipulation.reverseParagraphWords(paragraphInput);
                case "btnSortParagraphWords":
                    return WebApplication1.Models.ParagraphManipulation.sortParagraphWords(paragraphInput);
                case "btnHashParagraph":
                    return WebApplication1.Models.ParagraphManipulation.hashParagraph(paragraphInput);
                default:
                    return "";
            }
        }
    }
}