using System.IO;
using System.Reflection.Emit;

internal class Program
{
    private static void Main(string[] args)
    {
        String[] setup = File.ReadAllLines("input/setup.csv");
        foreach (string league in setup)
        {
            //Console.WriteLine(league);
        }

        List<Club> clubs = new List<Club>();
        String[] teams = File.ReadAllLines("input/teams.csv");
        foreach (string team in teams)
        {
            String[] values = team.Split(",");
            Club club1 = new Club();
            club1.abbreviation = values[0];
            club1.fullName = values[1];
            club1.specialMarking = values[2];
            clubs.Add(club1);

            //Console.WriteLine(team);
        }

        for (int i = 1; i <= 32; i++) 
        {
            string roundFilePath = "input/round-" + i + ".csv";
            if (File.Exists(roundFilePath)) {
                String[] round = File.ReadAllLines(roundFilePath);
                foreach (string match in round)
                {
                    String[] values = match.Split(",");
                    String homeAbbreviation = values[0];
                    String awayAbbreviation = values[1];
                    String[] goals = values[2].Split("-");
                    int homeGoals = int.Parse(goals[0]);
                    int awayGoals = int.Parse(goals[1]);

                    foreach (Club club in clubs) {
                        if (club.abbreviation == homeAbbreviation) {
                            club.goalsFor += homeGoals;
                            club.goalsAgainst += awayGoals;

                            if (homeGoals > awayGoals) {
                                club.gamesWon += 1;
                                club.points += 3;
                            }
                            else if (homeGoals < awayGoals) {
                                club.gamesLost += 1;
                            }
                            else if (homeGoals == awayGoals) {
                                club.gamesDrawn += 1;
                                club.points += 1;
                            }
                        }

                        if (club.abbreviation == awayAbbreviation) {
                            club.goalsFor += awayGoals;
                            club.goalsAgainst += homeGoals;

                            if (awayGoals > homeGoals) {
                                club.gamesWon += 1;
                                club.points += 3;
                            }
                            else if (awayGoals < homeGoals) {
                                club.gamesLost += 1;
                            }
                            else if (awayGoals == homeGoals) {
                                club.gamesDrawn += 1;
                                club.points += 1;
                            }
                        }
                    }

                    //Console.WriteLine(match);
                }
            }
        }

        foreach (Club club in clubs)
        {
            club.goalDifference = Math.Abs(club.goalsFor - club.goalsAgainst);
        }

        foreach (Club club in clubs)
        {
            if (club.specialMarking != "") {
                Console.Write("(" + club.specialMarking + ") ");
            }

            Console.Write(
                club.fullName
                + " " +
                club.gamesPlayed
                + " " +
                club.gamesWon
                + " " +
                club.gamesDrawn
                + " " +
                club.gamesLost
                + " " +
                club.goalsFor
                + " " +
                club.goalsAgainst
                + " " +
                club.goalDifference
                + " " +
                club.points
            );

            Console.Write("\n");
        }
    }
}