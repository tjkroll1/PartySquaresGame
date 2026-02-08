using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using User_NS;

namespace Board_NS
{
   public class BoardRow
   {
      public BoardRow(int maxCols)
      {
         RowID = RowID_Counter++;
         Squares = new List<Square>();
         m_ColumnTracker = 0;
         MaximumColumns = maxCols;
      }

      public BoardRow(int maxCols, List<Square> listSquares)
      {
         RowID = RowID_Counter++;
         Squares = new List<Square>();
         m_ColumnTracker = 0;
         MaximumColumns = maxCols;

         AddSquares(listSquares);
      }

      public BoardRow(int maxCols, List<Square> listSquares, bool isHeaderRow = true, bool isHeaderColumn = false)
      {
         if (isHeaderRow)
         {
            RowID = 0;
            RowID_Counter++;
         }
         else if(isHeaderColumn)
         {
            RowID = 0;
         }
         else
         {
            RowID = RowID_Counter++;
         }

         Squares = new List<Square>();

         m_ColumnTracker = 0;

         MaximumColumns = maxCols;

         AddSquares(listSquares);
      }

      public void AddSquare(Square newSquare)
      {
         Squares.Add(newSquare);
         IncrementColTracker();
      }

      public void AddSquares(List<Square> newSquares)
      {
         foreach (Square square in newSquares)
         {
            Squares.Add(square);
            IncrementColTracker();
         }
      }

      /// <summary>
      /// Show the numbers of the header square.
      /// </summary>
      /// <param name="showNumbers"></param>
      public void ShowHeaderNumbers(bool showNumbers = true)
      {
         foreach(HeaderSquare headerSquare in Squares)
         {
            headerSquare.ShowLabelText(showNumbers);
         }
      }

      /// <summary>
      /// Reset the Row ID Counter
      /// </summary>
      public static void ResetRowIDCounter()
      {
         RowID_Counter = 0;
      }

      public Square Find(Square findingSquare)
      {
         Predicate<Square> predicate = GetPredicateByID(findingSquare);
         return Squares.Find(predicate);
      }

      public Square FindLast(Square findingSquare)
      {
         Predicate<Square> predicate = GetPredicateByID(findingSquare);
         return Squares.FindLast(predicate);
      }

      public int FindIndex(Square fSquare)
      {
         Predicate<Square> predicate = GetPredicateByID(fSquare);
         return Squares.FindIndex(predicate);
      }

      public List<Square> FindAll(Square fSquare)
      {
         Predicate<Square> predicate = GetPredicateByID(fSquare);
         return Squares.FindAll(predicate);
      }

      public Square FindSquareByLocation(BoardLocation location)
      {
         Predicate<Square> predicate = GetPredicateByLocation(location);
         return Squares.Find(predicate);
      }

      public Square FindSquareByUser(User user)
      {
         Predicate<Square> predicate = GetPredicateByUser(user);
         return Squares.Find(predicate);
      }

      public List<Square> FindSquaresByUser(User user)
      {
         Predicate<Square> predicate = GetPredicateByUser(user);
         return Squares.FindAll(predicate);
      }

      public Square FindSquareByLabel(Label label)
      {
         Predicate<Square> predicate = GetPredicateByLabelName(label);
         return Squares.Find(predicate);
      }

      public void RemoveSquare(Square rSquare)
      {
         Predicate<Square> predicate = GetPredicateByID(rSquare);
         int removedamount = Squares.RemoveAll(predicate);
         if (removedamount < m_ColumnTracker)
         {
            m_ColumnTracker -= removedamount;
         }
      }

      public Square At(int index)
      {
         return Squares[index];
      }

      public void SetValue(int key, Square value)
      {
         Squares.Insert(key, value);
      }

      /// <summary>
      /// Clears out the board row.
      /// </summary>
      public void ResetRow()
      {
         foreach(Square square in Squares)
         {
            square.Clear();
         }
      }

      public List<Square> Squares
      {
         get;
         private set;
      }

      public int RowID
      {
         get;
         private set;
      }

      protected static int RowID_Counter
      {
         get;
         set;
      }

      protected int MaximumColumns
      {
         get;
         set;
      }

      public int Count
      {
         get { return Squares.Count; }
      }

      public Square this[int key]
      {
         get
         {
            return At(key);
         }
         set
         {
            SetValue(key, value);
         }
      }

      public bool MaxColsReached
      {
         get { return m_ColumnTracker >= MaximumColumns; }
      }

      private bool IncrementColTracker()
      {
         bool maxReached = true;
         if (m_ColumnTracker + 1 <= MaximumColumns)
         {
            m_ColumnTracker++;
            maxReached = false;
         }
         return maxReached;
      }

      private Predicate<Square> GetPredicateByID(Square square)
      {
         Predicate<Square> squarePredicate = (Square s) => { return s.ID == square.ID; };
         return squarePredicate;
      }

      private Predicate<Square> GetPredicateByLocation(BoardLocation location)
      {
         Predicate<Square> squarePredicate = (Square s) => { return s.Location.X == location.X && s.Location.Y == location.Y; };
         return squarePredicate;
      }

      private Predicate<Square> GetPredicateByUser(User user)
      {
         Predicate<Square> squarePredicate = (Square square) => 
         {
            if(square.SquareUser == null)
            {
               square.SetUser();
            }

            return square.SquareUser.Name == user.Name;
         };

         return squarePredicate;
      }

      private Predicate<Square> GetPredicateByLabelName(Label label)
      {
         Predicate<Square> squarePredicate = (Square s) => { return s.SquareLabel.Name == label.Name; };
         return squarePredicate;
      }

      private int m_ColumnTracker;
   }
}
