using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_NS
{
   /// <summary>
   /// Class containing user information.
   /// </summary>
   public class User : Basic_NS.BasicObject
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public User()
      {
         ID = ++UserID_Counter;
         Name = "";
         MoneyAmount = new Money();
         Color = Color.Aquamarine;
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="id"></param>
      /// <param name="name"></param>
      public User(string name)
      {
         ID = ++UserID_Counter;
         Name = name;
         MoneyAmount = new Money();
         Color = Color.Aquamarine;
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="newName"></param>
      public void ChangeName(string newName)
      {
         Name = newName;
      }

      /// <summary>
      /// Set the Money Amount of the User.
      /// </summary>
      /// <param name="money"></param>
      public void SetMoneyAmount(Money money)
      {
         MoneyAmount = money;
      }

      /// <summary>
      /// Increment the money amount of the user.
      /// </summary>
      /// <param name="money"></param>
      public void IncrementMoney(Money money)
      {
         MoneyAmount += money;
      }

      /// <summary>
      /// Decrease the money amount of the user.
      /// </summary>
      /// <param name="money"></param>
      public void DecrementMoney(Money money)
      {
         MoneyAmount -= money;
      }

      /// <summary>
      /// Clear the contents of the user.
      /// </summary>
      public void ClearUser()
      {
         Name = "";
         MoneyAmount = new Money();
      }

      /// <summary>
      /// Resets the User ID Counter.
      /// </summary>
      public static void ResetUserIDCounter()
      {
         UserID_Counter = 0;
      }

      /// <summary>
      /// ID of the User.
      /// </summary>
      public int ID
      {
         get;
         private set;
      }

      /// <summary>
      /// Name of the user.
      /// </summary>
      public string Name
      {
         get;
         private set;
      }

      /// <summary>
      /// Money amount of the user.
      /// </summary>
      public Money MoneyAmount
      {
         get;
         set;
      }

      public Color Color
      {
         get;
         set;
      }

      public static int UserID_Counter
      {
         get;
         private set;
      }

      /// <summary>
      /// Returns whether the user is a valid user
      /// </summary>
      /// <returns>True if the ID is not 0 and name is not empty string.</returns>
      public bool IsValid()
      {
         return ID > 0 && Name != "";
      }

      /// <summary>
      /// String representation of the user class.
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         string output = Name + " (" + MoneyAmount.StringShortAmount + ")";
         return output;
      }
   }
}
