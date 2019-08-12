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
        private List<string> LastYearsKeepers = new List<string>
        {
            "David Johnson",
            "Alvin Kamara",
            "Jordan Howard",
            "Rob Gronkowski",
            "Larry Fitzgerald",
            "Kirk Cousins",
            "Odell Beckham",
            "Jimmy Graham",
            "Jimmy Garoppolo",
            "Todd Gurley",
            "Melvin Gordon",
            "Jerick McKinnon",
            "Tyreek Hill",
            "Greg Olsen",
            "Antonio Brown",
            "Devonta Freeman",
            "Marquise Goodwin",
            "Michael Thomas",
            "Zach Ertz",
            "Kenyan Drake",
            "Devante Adams",
            "John Gordon",
            "Brandin Cooks",
            "DeAndre Hopkins",
            "Dalvin Cook",
            "Leonard Fournette",
            "Le'Veon Bell",
            "Kareem Hunt",
            "Derrick Henery",
            "Dion Lewis",
            "Eric Decker",

        };

        public ActionResult Index()
        {
            var players = GetPlayers();
            ViewBag.Players = players;
            ViewBag.WideRecievers = players.Where(p => p.Position.ToLower() == "wr");
            ViewBag.RunningBacks = players.Where(p => p.Position.ToLower() == "rb");
            ViewBag.QuarterBacks = players.Where(p => p.Position.ToLower() == "qb");
            ViewBag.TightEnds = players.Where(p => p.Position.ToLower() == "te");
            return View();
        }

        private List<Player> GetPlayers()
        {
            using (StreamReader r = new StreamReader(Server.MapPath(@"\Json\playerdata2018.json")))
            {
                string json = r.ReadToEnd();
                var players = Deserialize<List<Player>>(json);

                var sorted = Get2018DraftPrice(players).OrderByDescending(p => p.FantasyPoints).ToList();

                sorted = SetIs2018Keeper(sorted);

                return sorted;
            }
        }

        private List<Player> SetIs2018Keeper(List<Player> players)
        {
            foreach(var p in players)
            {
                if(LastYearsKeepers.Contains(RemoveNonLetters(p.Name)))
                {
                    p.IsLastYearKeeper = true;
                }
            }

            return players;
        }

        private List<Player> Get2018DraftPrice(List<Player> players)
        {
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