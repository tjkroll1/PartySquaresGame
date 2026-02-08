using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basic_NS
{
   /// <summary>
   /// Contains utility functions
   /// for all derived class objects.
   /// </summary>
   public class BasicObject
   {
      public BasicObject()
      {
         // Intentionally empty
      }

      /// <summary>
      /// Returns true if the derived object class is null.
      /// </summary>
      /// <returns>True if object is null.</returns>
      public bool Null()
      {
         return this == null;
      }
   }
}
