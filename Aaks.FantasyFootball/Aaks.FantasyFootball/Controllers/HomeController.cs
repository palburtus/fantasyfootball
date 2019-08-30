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
        private List<string> WrFriendlyTeams = new List<string>()
        {
            "ARI",
            "ATL",
            "KC",
            "CHI",
            "TB",
            "CIN"
        };

        private List<string> WrUnfriendlyTeams = new List<string>()
        {

        };

        private List<string> RbFriendlyTeams = new List<string>()
        {
            "CIN"
        };

        private List<string> RbUnFriendlyTeams = new List<string>()
        {
            "ATL",
            "DEN"
        };

        private List<string> RookieRankings = new List<string>()
        {
            "N'Keal Harry",
            "Miles Sanders"
        };

        private List<string> Sleepers = new List<string>()
        {
            "James Washington",
            "N'Keal Harry",
            "Miles Sanders",
            "Tarik Cohen",
            "Austin Ekeler", //Handcuff for Melvin Gordon 
        };

        private Dictionary<string, string> PlayersToTarget = new Dictionary<string, string>
        {
            { "Austin Ekeler", "Handcuff for Melvin Gordon" },
            { "A.J. Green", "" },
            { "Tyler Boyd", "Was hit or miss when AJ Green was healthy, was good when not, upside" },
            { "Stephon Diggs", "" },
            { "Christian Kirk", "Put him in this list twice!?" },
            { "Calvin Ridley", "" },
            { "Mike Williams", "" },
            { "Dak Prescott", "" },
            { "Michael Gallup", "" },
            { "Amari Cooper", "" },
            { "Allen Robison", "" },
            { "Cooper Kupp", "" },
            { "Robert Woods", "" },
            { "Will Fuller", "" },
            { "Jarvis Landry", "" },
            { "Chris Goodwin", "" },
            { "D.J. Moore", "" },
            { "Larry Fitzgerald", "" },
            { "Tyler Lockett", "Okay for the rigth price, TD regress to mean" },
            { "N'Keal Harry", "ROOKIE - lots of upside !!!" },
            { "Jordan Howard", "especially if you get rookie miles sanders" },
            { "Miles Sanders", "" },


        };

        private Dictionary<string, string> PlayersToAvoid = new Dictionary<string, string>
        {
            { "Phillip Lindsay", "" },
            { "Royce Freeman", ""},
            { "Devontae Booker", "" },
            { "Antonio Brown", ""},
            { "Keenan Allen", "" },
            { "Adam Thielen", "Price too high compared to diggs, more consistent on a weekly basis however" },
            { "Alshon Jeffery", "Has other players that are lest risky in same price range" },
            { "Sammy Watkins", "" },
            { "Kenny Golladay", "Too expensive" },
            { "Dante Pettis", "" },
            { "Corey Davis", "" },
            { "Derrius Guice", "too expensive" },
            { "Devonta Freeman", "" },
            { "Lamar Miller", "" },
            { "Latavius Murray", "" },
            { "Rashaad Penny", "" }

        };

        private List<string> ThisYearsKeepers = new List<string>()
        {
            "Alvin Kamara", //Collin
            "David Johnson",
            "Travis Kelce",
            "Todd Gurley", //Pat
            "Melvin Gordon",
            "Joe Mixon",
            "Davante Adams", //Shane
            "Christian Mccaffery",
            "Patrick Mahomes",
            "Tyreek Hill", //Jodee
            "Aaron Jones",
            "George Kittle",
            "Dalvin Cook", //Joe
            "DeAndre Hopkins",
            "Latavius Murry",
            "TY Hilton", //Wik
            "Devonta Freeman",
            "Dalvin Cook",
            "Brandin Cooks", //Gary
            "Damien Williams",
            "Michael Thomas", // Serge
            "Mark Ingram",
            "Julian Edelman",
            "Julio Jones", //Tom
            "Derrick Henry", //Meg
            "Evan Engram",
            "Adam Thielen",
            "James Conner", //Bob
            "Odel Beckham",
            "Nick Chubb"
        };

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
            ViewBag.WideReceivers = players.Where(p => p.Position.ToLower() == "wr");
            ViewBag.RunningBacks = players.Where(p => p.Position.ToLower() == "rb");
            ViewBag.QuarterBacks = players.Where(p => p.Position.ToLower() == "qb");
            ViewBag.TightEnds = players.Where(p => p.Position.ToLower() == "te");
            ViewBag.TargetedPlayers = players.Where(p => p.IsTarget == true);
            ViewBag.Rookies = GetRookies();
            return View();
        }

        private List<Player> GetPlayers()
        {
            using (StreamReader r = new StreamReader(Server.MapPath(@"\Json\playerdata2018.json")))
            {
                string json = r.ReadToEnd();
                var players = Deserialize<List<Player>>(json);

                var sorted = Get2018DraftPrice(players).OrderByDescending(p => p.FantasyPoints).ToList();

                sorted = SetDrafFlags(sorted);

                return sorted;
            }
        }

        private List<Player> GetRookies()
        {
            using (StreamReader r = new StreamReader(Server.MapPath(@"\Json\2019_rookies.txt")))
            {
                var players = new List<Player>();

                string line;
                
                while((line = r.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    Player player = new Player();
                    player.Name = values[0];
                    player.Team = values[1];
                    player.Position = values[2];
                    player.IsRookie = true;

                    players.Add(player);
                }
                

                

                var sorted = SetDrafFlags(players);

                return sorted;
            }
        }

        private List<Player> SetDrafFlags(List<Player> players)
        {
            foreach (var p in players)
            {
                if (ThisYearsKeepers.Contains(RemoveNonLetters(p.Name)))
                {
                    p.IsKeeper = true;
                }

                if (LastYearsKeepers.Contains(RemoveNonLetters(p.Name)))
                {
                    p.IsLastYearKeeper = true;
                }

                foreach (var key in PlayersToTarget.Keys)
                {
                    if (p.Name.Contains(key))
                    {
                        p.IsTarget = true;
                        p.Note = PlayersToTarget[key];
                    }
                }

                foreach (var key in PlayersToAvoid.Keys)
                {
                    if (p.Name.Contains(key))
                    {
                        p.IsAvoid = true;
                        p.Note = PlayersToAvoid[key];
                    }
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