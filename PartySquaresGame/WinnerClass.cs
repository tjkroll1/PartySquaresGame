using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Party_Squares_Game
{
   public class WinnerClass : Basic_NS.BasicObject
   {
      /// <summary>
      /// Default constructor
      /// </summary>
      public WinnerClass()
      {
         WinnerSquare = new Board_NS.Square();
         WinnerLabel = new Label();
      }

      /// <summary>
      /// Set the winning square.
      /// </summary>
      /// <param name="winnerSquare"></param>
      public void SetSquare(Board_NS.Square winnerSquare)
      {
         if (winnerSquare != null)
         {
            WinnerSquare = winnerSquare;
            SetWinnerLabelText();
         }
      }

      /// <summary>
      /// Set the winner label
      /// </summary>
      /// <param name="label"></param>
      public void SetWinnerLabel(Label label)
      {
         WinnerLabel = label;
      }

      /// <summary>
      /// Set the Winner Label Text
      /// </summary>
      private void SetWinnerLabelText()
      {
         if (WinnerLabel != null)
         {
            string labelText = "No winner";
            User_NS.User winningUser = WinnerSquare.SquareUser;

            if (winningUser != null)
            {
               if (winningUser.Name != "")
               {
                  labelText = $"Winner: {winningUser.Name}";
               }
            }

            WinnerLabel.Text = labelText;
         }
      }

      /// <summary>
      /// Reset the Winner Label
      /// </summary>
      public void Reset()
      {
         SetWinnerLabelText();
      }

      /// <summary>
      /// The current winner square.
      /// </summary>
      public Board_NS.Square WinnerSquare
      {
         get;
         private set;
      }

      /// <summary>
      /// The current winner label.
      /// </summary>
      public Label WinnerLabel
      {
         get;
         private set;
      }
   }
}
