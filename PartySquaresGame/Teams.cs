using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamName_NS
{
   public class Team
   {
      public Team()
      {
         Score = new Score_NS.Score();
         Name = "";
      }

      public Team(string name)
      {
         Name = name;
         Score = new Score_NS.Score();
      }

      public Team(string name, Score_NS.Score score)
      {
         Name = name;
         Score = score;
      }

      public Score_NS.Score Score
      {
         get;
         set;
      }

      public string Name
      {
         get;
         set;
      }
   }
}
