using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Board_NS
{
   public class Generator
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public Generator()
      {
         InitializeList();
      }

      /// <summary>
      /// Initializes the Random Number List
      /// </summary>
      public void InitializeList()
      {
         m_RandomNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      }

      /// <summary>
      /// Generate the header above the board.
      /// </summary>
      /// <param name="labels"></param>
      public List<Square> GenerateHeaderSquares(List<Label> labels, bool isHeaderRow = true)
      {
         List<Square> squares = new List<Square>();

         PopulateSquareListWithLabels(ref squares, labels);

         GenerateHeader(ref squares);

         SetSquareLocations(ref squares, isHeaderRow);

         return squares;
      }

      /// <summary>
      /// Generates a list of squares for either
      /// the header row or header column.
      /// </summary>
      /// <param name="squares"></param>
      private void GenerateHeader(ref List<Square> squares)
      {
         InitializeList();
         Random rand = new Random(DateTime.Now.Millisecond);
         List<int> randNums = new List<int>();

         while (m_RandomNumbers.Count > 0)
         {
            int randIndex = rand.Next(0, 9);
            int newNumber = GetRandomNumberFromList(randIndex);
            if (newNumber >= 0)
            {
               randNums.Add(newNumber);
            }
         }

         int numIndex = 0;
         foreach(HeaderSquare square in squares)
         {
            square.SetSquareValue(randNums[numIndex++], false);
         }
      }

      private void SetSquareLocations(ref List<Square> squares, bool isHeaderRow)
      {
         int x = -1;
         int y = -1;

         foreach (Square square in squares)
         {
            if (isHeaderRow)
            {
               x++;
            }
            else
            {
               y++;
            }

            square.Location = new BoardLocation(x, y);
         }
      }

      /// <summary>
      /// Populates a list of squares with labels
      /// </summary>
      /// <param name="squares"></param>
      /// <param name="labels"></param>
      private void PopulateSquareListWithLabels(ref List<Square> squares, List<Label> labels)
      {
         Square.ResetIDCounter();
         foreach (Label label in labels)
         {
            HeaderSquare square = new HeaderSquare();
            square.SetLabel(label);
            squares.Add(square);
         }
      }

      /// <summary>
      /// Returns a number from the list of random numbers.
      /// </summary>
      /// <param name="randIndex"></param>
      /// <returns></returns>
      protected int GetRandomNumberFromList(int randIndex)
      {
         int selectedNum = -1;

         if(randIndex >= 0 && randIndex < m_RandomNumbers.Count)
         {
            selectedNum = m_RandomNumbers[randIndex];
            m_RandomNumbers.RemoveAt(randIndex);
         }

         return selectedNum;
      }

      /// <summary>
      /// Returns true if the number exists in the list.
      /// </summary>
      /// <param name="newNumber"></param>
      /// <param name="randNums"></param>
      /// <returns>Index of number in list.</returns>
      protected int NumberInList(List<int> randNums, int newNumber)
      {
         Predicate<int> predicate = (int n) => { return n == newNumber; };
         return randNums.Find(predicate);
      }

      private List<int> m_RandomNumbers;
   }
}
