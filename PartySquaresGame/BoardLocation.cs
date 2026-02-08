using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_NS
{
   public class BoardLocation
   {
      public BoardLocation()
      {
         X = 0;
         Y = 0;
      }
      public BoardLocation(int x, int y)
      {
         X = x;
         Y = y;
      }
      public int X { set; get; }
      public int Y { set; get; }

      public static bool Equals(BoardLocation objA, BoardLocation objB)
      {
         return objA.X == objB.X && objA.Y == objB.Y;         
      }
   }
}
