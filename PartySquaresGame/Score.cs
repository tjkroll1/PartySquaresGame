using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TeamName_NS;

namespace Score_NS
{
   public class CurrentScore
   {
      public CurrentScore()
      {
         TeamScore1 = new Team();
         TeamScore2 = new Team();
      }

      public void SetTeams(Team team1, Team team2)
      {
         TeamScore1 = team1 ?? new Team();
         TeamScore2 = team2 ?? new Team();
      }

      public void SetScore(Score score1, Score score2)
      {
         if(TeamScore1 == null || TeamScore2 == null)
         {
            TeamScore1 = new Team();
            TeamScore2 = new Team();
         }

         TeamScore1.Score = score1;
         TeamScore2.Score = score2;
      }

      public Team TeamScore1
      {
         get;
         set;
      }

      public Team TeamScore2
      {
         get;
         set;
      }
   }

   public class Score : Basic_NS.BasicObject
   {
      public Score()
      {
         ScoreNumber = 0;
         ScoreString = "";
      }

      public Score(string scoreStr)
      {
         ScoreString = scoreStr;
         SetScoreInt(scoreStr);
      }

      public Score(int scoreInt)
      {
         ScoreNumber = scoreInt;
         SetScoreString(scoreInt);
      }

      public void SetScoreString(int scoreInt)
      {
         string scoreStr = scoreInt.ToString();
         if (scoreStr.Length == 1)
         {
            scoreStr = '0' + scoreStr;
         }
         ScoreString = scoreStr;
      }

      public void SetScoreInt(string scoreStr)
      {
         int result = 0;
         if(int.TryParse(scoreStr, out result))
         {
            ScoreNumber = result;
         }
         else
         {
            ScoreNumber = 0;
         }
      }

      public static bool operator <=(Score s1, Score s2)
      {
         if (s1 != null && s2 != null)
         {
            return s1.ScoreNumber <= s2.ScoreNumber;
         }
         else
         {
            return false;
         }
      }

      public static bool operator >=(Score s1, Score s2)
      {
         if (s1 != null && s2 != null)
         {
            return s1.ScoreNumber >= s2.ScoreNumber;
         }
         else
         {
            return false;
         }
      }

      public static bool operator <(Score s1, Score s2)
      {
         if (s1 != null && s2 != null)
         {
            return s1.ScoreNumber < s2.ScoreNumber;
         }
         else
         {
            return false;
         }
      }

      public static bool operator >(Score s1, Score s2)
      {
         if (s1 != null && s2 != null)
         {
            return s1.ScoreNumber > s2.ScoreNumber;
         }
         else
         {
            return false;
         }
      }

      public string ScoreString
      {
         get;
         private set;
      }

      public int ScoreNumber
      {
         get;
         private set;
      }
   }
}
