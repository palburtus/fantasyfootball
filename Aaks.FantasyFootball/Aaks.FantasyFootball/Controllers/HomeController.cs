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
using System.Text.RegularExpressions;

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
            //var html = @"http://fantasy.espn.com/football/league/draftrecap?seasonId=2018&leagueId=113756";

            //HtmlWeb web = new HtmlWeb();



            //var body = web.Load(html).DocumentNode.SelectNodes("html/body//*[@id='__next-wrapper']//*[@id='__next']//*[@id='espn-analytics']"); 

            //GetElementbyId("espn-analytics");

            using (StreamReader r = new StreamReader(Server.MapPath(@"\Json\2018DraftResults.html")))
            {
                string html = r.ReadToEnd();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var nodes = htmlDoc.DocumentNode.ChildNodes.FirstOrDefault().ChildNodes;

                foreach(var child in nodes)
                {
                    var previosDraftValue = ParseTeamDraftTable(child.SelectNodes("div/section/table/tbody/tr/td/div/div/div[2]/table/tbody/tr/td/div/table/tbody/tr"));

                    foreach (var pdv in previosDraftValue)
                    {
                        foreach (var p in players)
                        {
                            if(pdv.PlayerName != null)
                            {
                                if (RemoveNonLetters(p.Name).Contains(pdv.PlayerName) || pdv.PlayerName.Contains(RemoveNonLetters(p.Name)))
                                {
                                    p.LastYearDraftValue = pdv.Price;
                                }
                            }
                            
                        }
                       
                        
                    }
                }

                return players;
            }
        }

        private List<PreviousDraftValue> ParseTeamDraftTable(HtmlNodeCollection nodes)
        {
            List<PreviousDraftValue> values = new List<PreviousDraftValue>();

            foreach(var node in nodes)
            {
                var value = new PreviousDraftValue();

                string[] nameValueSplit = node.InnerText.Split(',');

                string[] nameSplit = nameValueSplit[0].Split(' ');
                string[] valueSplit = nameValueSplit[1].Split('$');
                nameSplit[0] = RemoveNonLetters(nameSplit[0]);

                string unformattedName = nameSplit[0] + " " + nameSplit[1];

                value.PlayerName = unformattedName;
                value.Price = Convert.ToInt32(valueSplit[1]);

                values.Add(value);
            }

            //NO.PlayerBID AMOUNT12David Johnson Ari, RB$1623Alvin Kamara NO, RB$631Jordan Howard Chi, RB$1637A.J. Green Cin, WR$4242Travis Kelce KC, TE$2444Keenan Allen LAC, WR$4792Corey Davis Ten, WR$14104Kerryon Johnson Det, RB$22115Philip Rivers LAC, QB$4127Anthony Miller Chi, WR$1162Christian Kirk Ari, WR$1179Tyler Lockett Sea, WR$1190Eli Manning NYG, QB$1194Dede Westbrook Jax, WR$1198Terrance Williams Dal, WR$1200Matt Prater Det, K$1202Bears D/ST Chi, D/ST$1

            //

            return values;
        }

        private static string RemoveNonLetters(string word)
        {
            Regex reg = new Regex("[^a-zA-Z' ]");
            word = reg.Replace(word, string.Empty);
            return word;
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