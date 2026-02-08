using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_NS
{
   /// <summary>
   /// Money: Keeps track of the float and
   /// string CashAmount of the input float CashAmount;
   /// </summary>
   public class Money : Basic_NS.BasicObject
   {
      /// <summary>
      /// Default constructor
      /// </summary>
      public Money()
      {
         SetMoneyAmount(0.0F);
      }

      /// <summary>
      /// Constructor with money float amount
      /// </summary>
      /// <param name="amount"></param>
      public Money(float amount)
      {
         SetMoneyAmount(amount);
      }

      /// <summary>
      /// Clears the money amount
      /// </summary>
      public void Clear()
      {
         SetMoneyAmount(0.0f);
      }

      /// <summary>
      /// Sets the amount of the money
      /// </summary>
      /// <param name="fAmount">The float amount of money entered</param>
      private void SetMoneyAmount(float fAmount)
      {
         Value = fAmount;
      }

      /// <summary>
      /// Assignment operator
      /// </summary>
      /// <param name="fAmount"></param>
      public static implicit operator Money(float fAmount)
      {
         return new Money(fAmount);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="m1"></param>
      /// <param name="m2"></param>
      /// <returns></returns>
      public static Money operator+(Money m1, Money m2)
      {
         if (m1 != null && m2 != null)
         {
            float newAmount = m1.Value + m2.Value;
            return new Money(newAmount);
         }
         else
         {
            return new Money();
         }
      }

      /// <summary>
      /// Minus operator
      /// </summary>
      /// <param name="m1"></param>
      /// <param name="m2"></param>
      /// <returns></returns>
      public static Money operator -(Money m1, Money m2)
      {
         if (m1 != null && m2 != null)
         {
            float newAmount = m1.Value - m2.Value;
            if(newAmount < 0)
            {
               newAmount = 0;
            }
            return new Money(newAmount);
         }
         else
         {
            return new Money();
         }
      }

      /// <summary>
      /// Division operator
      /// </summary>
      /// <param name="m1"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public static Money operator /(Money m1, int divisor)
      {
         if (m1 != null && divisor != 0)
         {
            float newAmount = m1.Value / divisor;
            return new Money(newAmount);
         }
         else
         {
            return new Money();
         }
      }

      /// <summary>
      /// Returns a string of the money amount ($0.00)
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return string.Format("{0:C2}", Value);
      }

      /// <summary>
      /// Property that contains the
      /// money amount.
      /// </summary>
      public float Value
      {
         get;
         private set;
      }

      /// <summary>
      /// Returns the short string of the money value ($0).
      /// </summary>
      public string StringShortAmount
      {
         get { return string.Format("{0:C0}", Value); }
      }
   }
}
