using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Score_NS;

namespace Board_NS
{
   /// <summary>
   /// Selects the tile based on the current score.
   /// </summary>
   public class ScoreTile : Square
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public ScoreTile(Square square)
      {
         PreviousSquare = null;
      }

      public void SetPreviousSquare(Square square)
      {
         if(square != null)
         {
            if(square.)
         }

         PreviousSquare = square;
      }

      public Square PreviousSquare
      {
         get;
         private set;
      }

   }
}
