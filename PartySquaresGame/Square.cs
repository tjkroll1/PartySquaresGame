using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using User_NS;

namespace Board_NS
{
   public class Square : Basic_NS.BasicObject
   {
      public enum SquareColor
      {
         DEFAULT_COLOR,
         LIGHT_COLOR,
         DARK_COLOR,
         DARKER_COLOR,
         SELECTED_COLOR,
         FILLED_COLOR
      };

      /// <summary>
      /// Default constructor
      /// </summary>
      public Square()
      {
         SetID();
         Location = new BoardLocation();
         CashAmount = new Money();
         IsHeaderSquare = false;
         SelectedSquare = false;
         SquareLabel = null;
      }

      /// <summary>
      /// Constructor with board location
      /// </summary>
      /// <param name="boardLocation"></param>
      public Square(BoardLocation boardLocation)
      {
         SetID();
         Location = boardLocation;
         CashAmount = new Money();
         IsHeaderSquare = false;
         SelectedSquare = false;
         SquareLabel = null;
      }

      /// <summary>
      /// Replace the user assigned to the square.
      /// </summary>
      /// <param name="user"></param>
      public void ReplaceUser(User user)
      {
         SquareUser = user;
      }

      /// <summary>
      /// Sets the square information from the user form class.
      /// </summary>
      /// <param name="user"></param>
      /// <param name="money"></param>
      /// <returns>True if the label is set.</returns>
      public bool FillSquare(User user, Money money)
      {
         bool success;

         ReplaceUser(user);

         SetMoney(money);

         success = SetLabelText(user.Name, money.StringShortAmount);

         SetLabelColor(user.Color);

         return success;
      }

      /// <summary>
      /// Updates/redraws the square label.
      /// </summary>
      public void Update()
      {
         if (SquareUser != null && SquareUser.IsValid())
         {
            SetLabelColor(SquareUser.Color);
         }
      }

      /// <summary>
      /// Sets the square as selected.
      /// </summary>
      /// <param name="boardLocation"></param>
      public void SetSelectedSquare(bool selectedSquare = true)
      {
         SelectedSquare = selectedSquare;
         int SYS_COLOR = 4; // Yellow
         SetLabelColor(SYS_COLOR);
      }

      /// <summary>
      /// Set the ID to the counter variable
      /// </summary>
      private void SetID()
      {
         ID = ID_Counter++;
      }

      /// <summary>
      /// Set the Money Amount of the Square.
      /// </summary>
      /// <param name="amount"></param>
      public void SetMoney(float amount)
      {
         SetMoney(new Money(amount));
      }

      /// <summary>
      /// Set Money Amount of the Square.
      /// </summary>
      /// <param name="money"></param>
      public void SetMoney(Money money)
      {
         if (money != null)
         {
            CashAmount = money;

            if (SquareUser == null)
            {
               SquareUser = new User();
            }
         }
      }

      /// <summary>
      /// Set the square label.
      /// </summary>
      /// <param name="label"></param>
      public void SetLabel(Label label)
      {
         SquareLabel = label;
      }

      public void SetUser(User user = null)
      {
         if(user == null)
         {
            user = new User();
         }

         SquareUser = user;
      }

      /// <summary>
      /// Set the text of the label.
      /// </summary>
      /// <param name="labelText"></param>
      /// <returns></returns>
      public bool SetLabelText(string name, string money)
      {
         bool labelIsSet = SquareLabel != null;

         string labelText = name + " " + money;

         if(SquareLabel != null)
         {
            SquareLabel.Text = labelText;

            int fontSize = (int)SquareLabel.Font.Size;
            if (name.Length > 5)
            {
               if (name.Length == 6)
               {
                  // Resets the font size from 18px to 16px so the name fits on one line
                  fontSize = 16;
               }
               else if (name.Length == 7)
               {
                  // Resets the font size from 18px to 14px so the name fits on one line
                  fontSize = 14;
               }
               else if(name.Length >= 8)
               {
                  // Resets the font size from 18px to 12px so the name fits on one line
                  fontSize = 12;
               }

               SquareLabel.Font = new Font(SquareLabel.Font.FontFamily, fontSize, SquareLabel.Font.Style);
            }
         }

         return labelIsSet;
      }

      /// <summary>
      /// Clear the contents of the square.
      /// </summary>
      public void Clear()
      {
         SquareLabel.Text = "";

         if (SquareUser != null)
         {
            SquareUser.DecrementMoney(CashAmount);
         }

         CashAmount = null;
         SquareUser = null;
         SetLabelColor(SquareColor.DEFAULT_COLOR);
         SelectedSquare = false;
      }

      /// <summary>
      /// Sets the color of the label that's set with this square.
      /// 0 = lightlight (Current Board Color), 1 = light, 2 = dark, 3 = darkdark,
      /// 4 = yellow, 5 = Aquamarine
      /// </summary>
      /// <param name="systemColorValue"></param>
      public void SetLabelColor(int systemColorValue)
      {
         switch(systemColorValue)
         {
            case 0:
               // Default square color
               SquareLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
               break;
            case 1:
               SquareLabel.BackColor = System.Drawing.SystemColors.ControlLight;
               break;
            case 2:
               SquareLabel.BackColor = System.Drawing.SystemColors.ControlDark;
               break;
            case 3:
               SquareLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
               break;
            case 4: //Winner square
               SquareLabel.BackColor = System.Drawing.Color.Yellow;
               break;
            case 5: //User selected square
               SquareLabel.BackColor = System.Drawing.Color.Aquamarine;
               break;
         }
      }

      /// <summary>
      /// Sets the label color using an enumeration
      /// </summary>
      /// <param name="colorEnum"></param>
      public void SetLabelColor(SquareColor colorEnum)
      {
         SetLabelColor((int)colorEnum);
      }

      /// <summary>
      /// Sets background color of the square label.
      /// </summary>
      /// <param name="userColor"></param>
      public void SetLabelColor(Color userColor)
      {
         SquareLabel.BackColor = userColor;
      }

      /// <summary>
      /// Reset the ID Counter.
      /// </summary>
      public static void ResetIDCounter()
      {
         ID_Counter = 0;
      }

      /// <summary>
      /// ID Counter of the squares.
      /// </summary>
      public static int ID_Counter
      {
         get;
         private set;
      }
      public int ID
      {
         get;
         private set;
      }

      public BoardLocation Location
      {
         get;
         set;
      }

      public User SquareUser
      {
         get;
         private set;
      }

      public Money CashAmount
      {
         get;
         set;
      }

      public Label SquareLabel
      {
         get;
         private set;
      }

      public bool IsHeaderSquare
      {
         get;
         protected set;
      }

      public bool SelectedSquare
      {
         get;
         private set;
      }

      public Color Color
      {
         get { return SquareLabel.BackColor; }
      }

      public bool IsFilled
      {
         get
         {
            if (SquareUser != null)
            {
               return SquareUser.IsValid();
            }
            else
            {
               return false;
            }
         }
      }
   }
}
