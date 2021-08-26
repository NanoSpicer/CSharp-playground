using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryPractice1;
using System.IO;
using System.Collections;
 
namespace MainApplicationPractice1 {
    class Program {

        // Relative Data folder where csv files will be located.
        private static readonly string DATA_DIR = "Data\\";

        /* Used Storage datastructures */
        // ID    TEAM  NUMBER   POSITION   NAME
        List<PlayerCard> players = new List<PlayerCard>();
        // TEAM
        List<TeamCard> teams = new List<TeamCard>();
        // DAY   PLAYER_ID   SCORE 
        List<PlayerScore> scores = new List<PlayerScore>();

        static void Main(string[] args) {
            Program p = new Program();
            p.start();
            p.exit();
        }

        private void start() {
            Console.WriteLine("Reading csv...");
            dumpFileContent();
            Console.WriteLine("READY!");

            Console.WriteLine("Computing Best Player:");
            bestPlayer();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing Score in a Day:");
            bestScoreInDay();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
            
            Console.WriteLine("Computing Mean Score from players that've participated:");
            meanScorePlayersParticipated();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();


            Console.WriteLine("Computing Variance from players whomst've played more than  5 matches:");
            variancePlayersWithMore5Matches();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing who's the best team:");
            bestTeam();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing who's the best team in each day:");
            bestTeamEachDay();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing players who have got 10 points more times:");
            playersWith10PointsMoreTimes();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing the team which has 442 at the first day:");
            bestEleven442FirstDay();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();

            Console.WriteLine("Computing the team which has 442 of the whole season:");
            bestEleven442Season();
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
        }

        /// <summary>
        /// The best player is going to be the player which has scored most points in the season.
        /// </summary>
        private void bestPlayer() {

            // Select every player and it's total score.
            var players_total_scores =
                from player in players
                join score in scores on player.ID equals score.PlayerID into playersScores // Player's scores
                select new {
                    Name = player.Name,
                    Points = playersScores.Sum(score => score.Score) // Total points scored.
                };

            // From every player, select those whose score equals to the maximum score within "players_total_scores"
            var best_players =
                from best in players_total_scores
                where (best.Points == players_total_scores.Max(player => player.Points))
                select best;

            foreach (var item in best_players) {
                Console.WriteLine("\t -" + item.Name + " with a total of " + item.Points + " points");
            }
        }

        /// <summary>
        /// When and who is the best score in a day?
        /// </summary>
        private void bestScoreInDay() {

            var bestScoresInDay =
                // From all scores
                from score in scores
                // Of each player
                join player in players on score.PlayerID equals player.ID
                // Select those where the Score field equals the maximum of all the scores in a single day.
                where (score.Score == scores.Max(score => score.Score))
                select new {
                    player.Name,
                    score.DayNumber,
                    score.Score
                };

            foreach (var player in bestScoresInDay) {
                Console.WriteLine("\t" + player.Name + " with a score of " + player.Score + " at day " + player.DayNumber);
            }
        }

        /// <summary>
        /// Compute the mean score for the players that have participated
        /// </summary>
        private void meanScorePlayersParticipated() {
            var playersAndMeanScores =
                from player in players
                join score in scores on player.ID equals score.PlayerID into playersScores // Player's Scores...
                // If they have more than 0 scores, then they have participated.
                where (playersScores.Count() > 0)
                select new {
                    player.Name,
                    MeanScore = playersScores.Average(score => score.Score)
                };
            foreach (var player in playersAndMeanScores) {
                Console.WriteLine("\t" + player.Name + " has a mean score of " + player.MeanScore);
            }
        }

        /// <summary>
        /// Variance of players who have more than 5 matches.
        /// <question> variance of a player over TOTAL SCORES or per single score????????</question>
        /// </summary>
        private void variancePlayersWithMore5Matches() {

            // Each player with its total score
            var playersAndTotalScores =
                from player in players
                join match in scores on player.ID equals match.PlayerID into playersMatches // Player's matches.
                // Whomst've played more than 5 matches
                // where (playersMatches.Count() > 5)
                select new {
                    player.Name,
                    MatchesPlayed = playersMatches.Count(),
                    TotalScore = playersMatches.Sum(score => score.Score)
                };
            
            // The average score for a player independently from the amount of matches that has played
            double mean = playersAndTotalScores.Average(player => player.TotalScore);
            // The amount of player that there are
            int N = playersAndTotalScores.Count();
            // Players whomst've played more than 5 matches
            var playersWithMoreThan5Matches =
                from player in playersAndTotalScores
                where (player.MatchesPlayed > 5)
                select player;

            // Calculate the variance for each player
            foreach (var player in playersWithMoreThan5Matches) {
                var variance = Math.Pow((player.TotalScore - mean),2) / N;
                Console.WriteLine("\t" + player.Name + " has a variance of " + variance);
            }

        }
        /// <summary>
        /// This will check the amount of points that a team has scored in total. 
        /// And then retrieve the ones who have a maximum amount of points
        /// </summary>
        private void bestTeam() {
            // This computes each player total score and records only the team that it belongs to
            var teamAndItsPlayersScores = 
                from team in teams
                join player in players on team.Name equals player.Team // team's players
                join score in scores on player.ID equals score.PlayerID into teamPlayersScores // team's player's scores
                select new {
                    team.Name,
                    IndividualScore = teamPlayersScores.Sum(score => score.Score)
                };
            
            var teamsAndItsFinalScores =
                from team in teams
                join teamName in teamAndItsPlayersScores on team.Name equals teamName.Name into eachTeam
                select new {
                    team.Name,
                    // Sum each team's player's individual scores.
                    TotalScore = eachTeam.Sum(t => t.IndividualScore)
                };
            
            var bestTeams =
                from team in teamsAndItsFinalScores
                where (team.TotalScore == teamsAndItsFinalScores.Max(score => score.TotalScore))
                select team;

            foreach (var team in bestTeams) {
                Console.WriteLine("\t*"+team.Name+" with a total score of "+team.TotalScore);
            }
        }
       
        private void bestTeamEachDay() {
            var days =
                (from score in scores
                 orderby score.DayNumber ascending
                 select score.DayNumber).Distinct();

            // foreach day compute which one was the best team.
            foreach (var day in days) {

                var PlayersScores =
                    from score in scores
                    where (score.DayNumber == day)
                    join p in players on score.PlayerID equals p.ID
                    select new {
                        Player = p,
                        IndividualScore = score.Score
                    };

                var teamsAndItsFinalScores =
                    from player in PlayersScores
                    group player by player.Player.Team into TeamGroups
                    select new {
                        Name = TeamGroups.Key,
                        TotalScore = TeamGroups.Sum(p => p.IndividualScore)
                    };
                
                var bestTeams =
                    from team in teamsAndItsFinalScores
                    where (team.TotalScore == teamsAndItsFinalScores.Max(score => score.TotalScore))
                    select team;
                foreach (var best_team in bestTeams) {
                    Console.WriteLine(" *" + best_team.Name + " - " + best_team.TotalScore + " at day " + day);
                }
            }


        }
        

        private void playersWith10PointsMoreTimes() {
           
            var result =
                from score in scores
                where (score.Score == 10)
                join player in players on score.PlayerID equals player.ID into PlayerScores
                group PlayerScores by score.PlayerID into Groups
                select new {
                    // Retrieve name
                    Name = (from pl in players where(pl.ID ==Groups.Key)select pl.Name).First(),
                    Times = Groups.Count()
                };

            var more_times =
                from reg in result
                where (reg.Times == result.Max(t => t.Times))
                select reg;

            Console.WriteLine("The players with 10 points more times are:");
            
            foreach (var val in more_times) {
                Console.WriteLine("\t" + val.Name+ " with 10 points scored " + val.Times + " times");
            }
        }

        private void bestEleven442FirstDay() {

            var firstDayNumber =
                (from score in scores select score.DayNumber).Min();

            var scores_firstDay = from score in scores where (score.DayNumber == firstDayNumber) select score;

            var playerAndItsTotalScores =
                (from player in players
                join score in scores_firstDay on player.ID equals score.PlayerID into PlayersAndScores
                select new {
                    // Player Name
                    Player = player,
                    TotalScore = PlayersAndScores.Sum(w => w.Score)
                }).Distinct().OrderByDescending(t => t.TotalScore);


            Console.WriteLine("The best eleven 442 of day 1 is:");

            var forwards = (from p in playerAndItsTotalScores where (p.Player.Position == "Forward") select p).Take(2);
            var middles = (from p in playerAndItsTotalScores where (p.Player.Position == "Midfield") select p).Take(4);
            var defenders = (from p in playerAndItsTotalScores where (p.Player.Position == "Defender") select p).Take(4);
            var goalkeeper = (from p in playerAndItsTotalScores where (p.Player.Position == "Goalkeeper") select p).Take(1);

            Console.WriteLine("Best Forwards");
            foreach (var fw in forwards) {
                Console.WriteLine("\t"+fw.Player+" SCORED => "+fw.TotalScore);
            }

            Console.WriteLine("Best Middles");
            foreach (var md in middles) {
                Console.WriteLine("\t" + md.Player + " SCORED => " + md.TotalScore);
            }

            Console.WriteLine("Best Defenders");
            foreach (var df in defenders) {
                Console.WriteLine("\t" + df.Player + " SCORED => " + df.TotalScore);
            }

            Console.WriteLine("Best Goalkeeper");
            foreach (var gp in goalkeeper) {
                Console.WriteLine("\t" + gp.Player + " SCORED => " + gp.TotalScore);
            }

        }

        
        private void bestEleven442Season() {


            var playerAndItsTotalScores =
                (from player in players
                 join score in scores on player.ID equals score.PlayerID into PlayersAndScores
                 select new {
                     // Player Name
                     Player = player,
                     TotalScore = PlayersAndScores.Sum(w => w.Score)
                 }).Distinct().OrderByDescending(t => t.TotalScore);


            Console.WriteLine("The best eleven 442 of day 1 is:");

            var forwards = (from p in playerAndItsTotalScores where (p.Player.Position == "Forward") select p).Take(2);
            var middles = (from p in playerAndItsTotalScores where (p.Player.Position == "Midfield") select p).Take(4);
            var defenders = (from p in playerAndItsTotalScores where (p.Player.Position == "Defender") select p).Take(4);
            var goalkeeper = (from p in playerAndItsTotalScores where (p.Player.Position == "Goalkeeper") select p).Take(1);

            Console.WriteLine("Best Forwards");
            foreach (var fw in forwards) {
                Console.WriteLine("\t" + fw.Player + " SCORED => " + fw.TotalScore);
            }

            Console.WriteLine("Best Middles");
            foreach (var md in middles) {
                Console.WriteLine("\t" + md.Player + " SCORED => " + md.TotalScore);
            }

            Console.WriteLine("Best Defenders");
            foreach (var df in defenders) {
                Console.WriteLine("\t" + df.Player + " SCORED => " + df.TotalScore);
            }

            Console.WriteLine("Best Goalkeeper");
            foreach (var gp in goalkeeper) {
                Console.WriteLine("\t" + gp.Player + " SCORED => " + gp.TotalScore);
            }
        }

        private void exit() {
            Console.Write("Press any key to exit the program...");
            Console.ReadKey();
        }

        private void dumpFileContent() {
            /** Dump teams **/
            StreamReader SRC_TEAMS = new StreamReader(DATA_DIR + "teams.csv");
            // Remove the first line which is the header of the csv file.
            SRC_TEAMS.ReadLine();
            while (!SRC_TEAMS.EndOfStream) {
                teams.Add(new TeamCard(SRC_TEAMS.ReadLine()));
            }

            StreamReader SRC_PLAYERS = new StreamReader(DATA_DIR + "players.csv");
            SRC_PLAYERS.ReadLine();
            string aux;
            while (!SRC_PLAYERS.EndOfStream) {
                aux = SRC_PLAYERS.ReadLine();
                // these csv separator is ";"
                string[] tokens = aux.Split(';');
                // the order of the parameters is as follows
                // [0]   [1]   [2]      [3]        [4]
                // ID    TEAM  NUMBER   POSITION   NAME
                players.Add(new PlayerCard(Int32.Parse(tokens[0]), Int32.Parse(tokens[2]), tokens[4], tokens[1], tokens[3]));
            }

            StreamReader SRC_SCORES = new StreamReader(DATA_DIR + "scores.csv");
            SRC_SCORES.ReadLine();
            while (!SRC_SCORES.EndOfStream) {
                aux = SRC_SCORES.ReadLine();
                string[] tokens = aux.Split(';');
                // the order of the parameters is as follows
                // [0]   [1]         [2]  
                // DAY   PLAYER_ID   SCORE 
                scores.Add(new PlayerScore(Int32.Parse(tokens[0]), Int32.Parse(tokens[1]), Int32.Parse(tokens[2])));
            }
        }
    }
}
