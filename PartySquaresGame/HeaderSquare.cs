using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_NS
{
   public class HeaderSquare : Square
   {
      public HeaderSquare()
      {
         Value = -1;
         IsHeaderSquare = true;
      }

      /// <summary>
      /// Shows or hides the text in the header label
      /// </summary>
      /// <param name="showLabelText"></param>
      public void ShowLabelText(bool showLabelText = true)
      {
         if(SquareLabel != null)
         {
            if (showLabelText)
            {
               SquareLabel.Text = Value.ToString();
            }
            else
            {
               SquareLabel.Text = "";
            }
         }
      }

      /// <summary>
      /// Sets the value of the square.
      /// </summary>
      /// <param name="value"></param>
      /// <param name="showLabelText"></param>
      public void SetSquareValue(int value, bool showLabelText = false)
      {
         Value = value;

         ShowLabelText(showLabelText);
      }

      /// <summary>
      /// The value of the header square.
      /// </summary>
      public int Value
      {
         get;
         private set;
      }
   }
}
