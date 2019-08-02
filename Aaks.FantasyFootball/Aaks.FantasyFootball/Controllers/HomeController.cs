using Aaks.FantasyFootball.Models;
using Aaks.Restclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaks.FantasyFootball.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Players = GetPlayers();
            return View();
        }

        private List<Player> GetPlayers()
        {
            string url = "https://api.fantasydata.net/api/nfl/fantasy/json/PlayerSeasonStats/2018";
            var client = new HttpRestClient();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", "5ff5e827ec174421aa914dcef10a2302");
            var response = client.Get<List<Player>>(url);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Body;
            }
            else
            {
                return null;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}