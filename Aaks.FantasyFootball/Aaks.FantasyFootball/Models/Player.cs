using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aaks.FantasyFootball.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public int SeasonType { get; set; }
        public int Season { get; set; }
        public string Team { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PositionCategory { get; set; }
        public int Played { get; set; }
        public int Started { get; set; }
        public double PassingAttempts { get; set; }
        public double PassingCompletions { get; set; }
        public double PassingYards { get; set; }
        public int PassingCompletionPercentage { get; set; }
        public int PassingYardsPerAttempt { get; set; }
        public int PassingYardsPerCompletion { get; set; }
        public double PassingTouchdowns { get; set; }
        public double PassingInterceptions { get; set; }
        public int PassingRating { get; set; }
        public double PassingLong { get; set; }
        public double PassingSacks { get; set; }
        public double PassingSackYards { get; set; }
        public double RushingAttempts { get; set; }
        public double RushingYards { get; set; }
        public int RushingYardsPerAttempt { get; set; }
        public double RushingTouchdowns { get; set; }
        public double RushingLong { get; set; }
        public double ReceivingTargets { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public int ReceivingYardsPerReception { get; set; }
        public double ReceivingTouchdowns { get; set; }
        public double ReceivingLong { get; set; }
        public double Fumbles { get; set; }
        public double FumblesLost { get; set; }
        public double PuntReturns { get; set; }
        public double PuntReturnYards { get; set; }
        public double PuntReturnTouchdowns { get; set; }
        public double KickReturns { get; set; }
        public double KickReturnYards { get; set; }
        public double KickReturnTouchdowns { get; set; }
        public double SoloTackles { get; set; }
        public double AssistedTackles { get; set; }
        public double TacklesForLoss { get; set; }
        public double Sacks { get; set; }
        public double SackYards { get; set; }
        public double QuarterbackHits { get; set; }
        public double PassesDefended { get; set; }
        public double FumblesForced { get; set; }
        public double FumblesRecovered { get; set; }
        public double FumbleReturnTouchdowns { get; set; }
        public double Interceptions { get; set; }
        public double InterceptionReturnTouchdowns { get; set; }
        public double FieldGoalsAttempted { get; set; }
        public double FieldGoalsMade { get; set; }
        public double ExtraPointsMade { get; set; }
        public double TwoPointConversionPasses { get; set; }
        public double TwoPointConversionRuns { get; set; }
        public double TwoPointConversionReceptions { get; set; }
        public double FantasyPoints { get; set; }
        public double FantasyPointsPPR { get; set; }
        public string FantasyPosition { get; set; }
        public int PlayerSeasonID { get; set; }
        public double ExtraPointsAttempted { get; set; }
        public object AuctionValue { get; set; }
        public object AuctionValuePPR { get; set; }
        public double FantasyPointsFanDuel { get; set; }
        public double FieldGoalsMade0to19 { get; set; }
        public double FieldGoalsMade20to29 { get; set; }
        public double FieldGoalsMade30to39 { get; set; }
        public double FieldGoalsMade40to49 { get; set; }
        public double FieldGoalsMade50Plus { get; set; }
        public double FantasyPointsDraftKings { get; set; }
        public object AverageDraftPosition { get; set; }
        public object AverageDraftPositionPPR { get; set; }
        public int TeamID { get; set; }
        public object AverageDraftPositionRookie { get; set; }
        public object AverageDraftPositionDynasty { get; set; }
        public object AverageDraftPosition2QB { get; set; }
    }
}