using Aaks.FantasyFootball.Models;
using Aaks.Restclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using HtmlAgilityPack;

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
            //string url = "https://api.fantasydata.net/api/nfl/fantasy/json/PlayerSeasonStats/2018";
            //var client = new HttpRestClient();
            //Dictionary<string, string> headers = new Dictionary<string, string>();
            //headers.Add("Ocp-Apim-Subscription-Key", "5ff5e827ec174421aa914dcef10a2302");
            /*var response = client.Get<List<Player>>(url);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Body;
            }
            else
            {
                return null;
            }*/

            using (StreamReader r = new StreamReader(Server.MapPath(@"\Json\playerdata2018.json")))
            {
                string json = r.ReadToEnd();
                var players = Deserialize<List<Player>>(json);

                return Get2018DraftPrice(players);
            }
        }

        private List<Player> Get2018DraftPrice(List<Player> players)
        {
            var html = @"http://fantasy.espn.com/football/league/draftrecap?seasonId=2018&leagueId=113756";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.GetElementbyId("espn-analytics");
            var nodes = node.SelectNodes("div/div[3]/div/div[2]/div/div[2]/div");
            

            return players;
        }

        private T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
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