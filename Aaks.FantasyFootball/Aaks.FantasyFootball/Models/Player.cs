﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aaks.FantasyFootball.Models
{
    public class Player
    {
        public int? DraftPrice { get; set; }
        public int? PlayerID { get; set; }
        public int? SeasonType { get; set; }
        public int? Season { get; set; }
        public string Team { get; set; }
        public int? Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PositionCategory { get; set; }
        public int? Activated { get; set; }
        public int? Played { get; set; }
        public int? Started { get; set; }
        public double? PassingAttempts { get; set; }
        public double? PassingCompletions { get; set; }
        public double? PassingYards { get; set; }
        public double? PassingCompletionPercentage { get; set; }
        public double? PassingYardsPerAttempt { get; set; }
        public double? PassingYardsPerCompletion { get; set; }
        public double? PassingTouchdowns { get; set; }
        public double? PassingInterceptions { get; set; }
        public double? PassingRating { get; set; }
        public double? PassingLong { get; set; }
        public double? PassingSacks { get; set; }
        public double? PassingSackYards { get; set; }
        public double? RushingAttempts { get; set; }
        public double? RushingYards { get; set; }
        public double? RushingYardsPerAttempt { get; set; }
        public double? RushingTouchdowns { get; set; }
        public double? RushingLong { get; set; }
        public double? ReceivingTargets { get; set; }
        public double? Receptions { get; set; }
        public double? ReceivingYards { get; set; }
        public double? ReceivingYardsPerReception { get; set; }
        public double? ReceivingTouchdowns { get; set; }
        public double? ReceivingLong { get; set; }
        public double? Fumbles { get; set; }
        public double? FumblesLost { get; set; }
        public double? PuntReturns { get; set; }
        public double? PuntReturnYards { get; set; }
        public double? PuntReturnYardsPerAttempt { get; set; }
        public double? PuntReturnTouchdowns { get; set; }
        public double? PuntReturnLong { get; set; }
        public double? KickReturns { get; set; }
        public double? KickReturnYards { get; set; }
        public double? KickReturnYardsPerAttempt { get; set; }
        public double? KickReturnTouchdowns { get; set; }
        public double? KickReturnLong { get; set; }
        public double? SoloTackles { get; set; }
        public double? AssistedTackles { get; set; }
        public double? TacklesForLoss { get; set; }
        public double? Sacks { get; set; }
        public double? SackYards { get; set; }
        public double? QuarterbackHits { get; set; }
        public double? PassesDefended { get; set; }
        public double? Interceptions { get; set; }
        public double? InterceptionReturnYards { get; set; }
        public double? InterceptionReturnTouchdowns { get; set; }
        public double? BlockedKicks { get; set; }
        public double? SpecialTeamsSoloTackles { get; set; }
        public double? SpecialTeamsAssistedTackles { get; set; }
        public double? MiscSoloTackles { get; set; }
        public double? MiscAssistedTackles { get; set; }
        public double? Punts { get; set; }
        public double? PuntYards { get; set; }
        public double? PuntAverage { get; set; }
        public double? FieldGoalsAttempted { get; set; }
        public double? FieldGoalsMade { get; set; }
        public double? FieldGoalsLongestMade { get; set; }
        public double? ExtraPointsMade { get; set; }
        public double? TwoPointConversionPasses { get; set; }
        public double? TwoPointConversionRuns { get; set; }
        public double? TwoPointConversionReceptions { get; set; }
        public double? FantasyPoints { get; set; }
        public double? FantasyPointsPPR { get; set; }
        public double? ReceptionPercentage { get; set; }
        public double? ReceivingYardsPerTarget { get; set; }
        public double? Tackles { get; set; }
        public double? OffensiveTouchdowns { get; set; }
        public double? DefensiveTouchdowns { get; set; }
        public double? SpecialTeamsTouchdowns { get; set; }
        public double? Touchdowns { get; set; }
        public string FantasyPosition { get; set; }
        public double? FieldGoalPercentage { get; set; }
        public int? PlayerSeasonID { get; set; }
        public double? FumblesOwnRecoveries { get; set; }
        public double? FumblesOutOfBounds { get; set; }
        public double? KickReturnFairCatches { get; set; }
        public double? PuntReturnFairCatches { get; set; }
        public double? PuntTouchbacks { get; set; }
        public double? PuntInside20 { get; set; }
        public double? PuntNetAverage { get; set; }
        public double? ExtraPointsAttempted { get; set; }
        public double? BlockedKickReturnTouchdowns { get; set; }
        public double? FieldGoalReturnTouchdowns { get; set; }
        public double? Safeties { get; set; }
        public double? FieldGoalsHadBlocked { get; set; }
        public double? PuntsHadBlocked { get; set; }
        public double? ExtraPointsHadBlocked { get; set; }
        public double? PuntLong { get; set; }
        public double? BlockedKickReturnYards { get; set; }
        public double? FieldGoalReturnYards { get; set; }
        public double? PuntNetYards { get; set; }
        public double? SpecialTeamsFumblesForced { get; set; }
        public double? SpecialTeamsFumblesRecovered { get; set; }
        public double? MiscFumblesForced { get; set; }
        public double? MiscFumblesRecovered { get; set; }
        public string ShortName { get; set; }
        public double? SafetiesAllowed { get; set; }
        public object Temperature { get; set; }
        public object Humidity { get; set; }
        public object WindSpeed { get; set; }
        public object OffensiveSnapsPlayed { get; set; }
        public object DefensiveSnapsPlayed { get; set; }
        public object SpecialTeamsSnapsPlayed { get; set; }
        public object OffensiveTeamSnaps { get; set; }
        public object DefensiveTeamSnaps { get; set; }
        public object SpecialTeamsTeamSnaps { get; set; }
        public double? AuctionValue { get; set; }
        public double? AuctionValuePPR { get; set; }
        public double? TwoPointConversionReturns { get; set; }
        public double? FantasyPointsFanDuel { get; set; }
        public double? FieldGoalsMade0to19 { get; set; }
        public double? FieldGoalsMade20to29 { get; set; }
        public double? FieldGoalsMade30to39 { get; set; }
        public double? FieldGoalsMade40to49 { get; set; }
        public double? FieldGoalsMade50Plus { get; set; }
        public double? FantasyPointsDraftKings { get; set; }
        public double? FantasyPointsYahoo { get; set; }
        public double? AverageDraftPosition { get; set; }
        public double? AverageDraftPositionPPR { get; set; }
        public int? TeamID { get; set; }
        public int? GlobalTeamID { get; set; }
        public double? FantasyPointsFantasyDraft { get; set; }
        public object AverageDraftPositionRookie { get; set; }
        public double? AverageDraftPositionDynasty { get; set; }
        public double? AverageDraftPosition2QB { get; set; }
        public List<object> ScoringDetails { get; set; }
    }
}