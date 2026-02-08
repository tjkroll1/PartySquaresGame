using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Score_NS;

namespace Board_NS
{
   /// <Board Class>
   ///   Class for storing all of the components of the board
   /// </Board Class>

   public class Board : Basic_NS.BasicObject
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public Board()
      {
         InitializeBoard();
      }

      /// <summary>
      /// Initializes the playing board.
      /// </summary>
      public void InitializeBoard()
      {
         m_Rows = 0;
         m_MaxRows = 10;
         m_MaxCols = 10;
         MasterBoard = new List<BoardRow>(m_MaxRows);
         m_BoardLabels = new List<List<Label>>();
         BoardGenerator = new Generator();

         BoardRow.ResetRowIDCounter();
         Square.ResetIDCounter();
      }

      /// <summary>
      /// Resets the board to its original state.
      /// </summary>
      public void Reset()
      {
         foreach(BoardRow boardRow in MasterBoard)
         {
            boardRow.ResetRow();
         }
      }

      /// <summary>
      /// Displays the header numbers
      /// </summary>
      public void StartBoard()
      {
         HeaderRow.ShowHeaderNumbers();
         HeaderColumn.ShowHeaderNumbers();
      }

      /// <summary>
      /// Generate the header row
      /// </summary>
      public void GenerateHeaderRow(List<Label> headerLabels)
      {
         HeaderRow = new BoardRow(m_MaxCols, BoardGenerator.GenerateHeaderSquares(headerLabels, true), true);
         MasterBoard.Add(HeaderRow);
      }

      /// <summary>
      /// Generate the header column
      /// </summary>
      public void GenerateHeaderColumn(List<Label> headerLabels)
      {
         HeaderColumn = new BoardRow(m_MaxCols, BoardGenerator.GenerateHeaderSquares(headerLabels, false), false, true);
      }

      /// <summary>
      /// Populate the board with rows
      /// </summary>
      public void PopulateBoard()
      {
         Square.ResetIDCounter();

         for (int row = 0; row < m_MaxRows && m_BoardLabels.Count > 0; ++row)
         {
            // Set the ID of the row 1 more than the header row.
            BoardRow boardRow = new BoardRow(m_MaxCols);
            List<Label> labelList = m_BoardLabels[row];

            for(int col = 0; col < m_MaxCols; ++col)
            {
               //Square square = new Square(new BoardLocation(row, col));
               Square square = new Square(new BoardLocation(col, row));

               if (col < labelList.Count)
               {
                  square.SetLabel(labelList[col]);
               }

               boardRow.AddSquare(square);
            }

            MasterBoard.Add(boardRow);
         }
      }

      /// <summary>
      /// Sets the winning square
      /// </summary>
      /// <param name="team1"></param>
      /// <param name="team2"></param>
      public Square SetScore(Score team1, Score team2)
      {
         GetNewScore(ref team1, ref team2);
         int rowNum = -1;
         int colNum = -1;
         try
         {
            if (team1 != null && team2 != null)
            {
               //hometeam
               foreach (Square squareHeader in HeaderRow.Squares)
               {
                  if (squareHeader.SquareLabel.Text == "")
                  {
                     break;
                  }

                  if (int.Parse(squareHeader.SquareLabel.Text) == team1.ScoreNumber)
                  {
                     rowNum = squareHeader.ID; //HomeTeamSquare
                  }
               }

               //awayteam
               foreach (Square squareColumn in HeaderColumn.Squares)
               {
                  if (squareColumn.SquareLabel.Text == "")
                  {
                     break;
                  }

                  if (int.Parse(squareColumn.SquareLabel.Text) == team2.ScoreNumber)
                  {
                     colNum = squareColumn.ID; //AwayTeamSquare
                  }
               }

               BoardLocation location = new BoardLocation(rowNum, colNum);

               Square selectedSquare = null;

               if (location != null && HeaderRow != null && HeaderColumn != null)
               {
                  selectedSquare = FindSquareByLocation(location);

                  if (selectedSquare != null)
                  {
                     if(BoardLocation.Equals(location, selectedSquare.Location))
                     {
                        if (WinningSquare != null)
                        {
                           WinningSquare.SetSelectedSquare(false);
                        }
                        selectedSquare.SetSelectedSquare(true);
                     }
                  }
               }

               if (WinningSquare == null)
               {
                  WinningSquare = selectedSquare;
               }
               else
               {
                  if (WinningSquare.IsFilled)
                  {
                     // Update to the color the user selected for that square.
                     WinningSquare.Update();
                  }
                  else
                  {
                     WinningSquare.SetLabelColor(Square.SquareColor.DEFAULT_COLOR); //White
                  }

                  WinningSquare = selectedSquare;
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }

         return WinningSquare;
      }

      /// <summary>
      /// Get the last digit of the home and away scores.
      /// </summary>
      /// <param name="home"></param>
      /// <param name="away"></param>
      private void GetNewScore(ref Score home, ref Score away)
      {
         Func<string, string> LastCharScoreFunc = delegate (string scoreStr)
         {
            char lastChar = scoreStr.Length == 2 ? scoreStr[1] : ' ';

            return String.Concat(lastChar);
         };

         if(home != null && away != null)
         {
            int x = int.Parse(LastCharScoreFunc(home.ScoreString));
            int y = int.Parse(LastCharScoreFunc(away.ScoreString));

            home = new Score(x);
            away = new Score(y);
         }
      }

      /// <summary>
      /// Adds a user to a bunch of squares
      /// </summary>
      /// <param name="boardLocation"></param>
      /// <param name="user"></param>
      /// <param name="count"></param>
      public User_NS.Money AddUserToRandomSquares(User_NS.User user, User_NS.Money squareAmount, int count = 1)
      {
         User_NS.Money moneyToAddPool = 0;
         List<Square> availableSquares = GetAvailableSquares();
         if(availableSquares != null && availableSquares.Count > 0)
         {
            Random rand = new Random(DateTime.Now.Millisecond);
            for(int index = 0; index < count; index++)
            {
               if (rand != null)
               {
                  int randIndex = rand.Next(0, availableSquares.Count - 1);
                  Square nextSquare = availableSquares[randIndex];
                  if (nextSquare != null && !nextSquare.IsFilled)
                  {
                     nextSquare.FillSquare(user, squareAmount);
                     availableSquares.RemoveAt(randIndex);
                     moneyToAddPool += squareAmount;
                  }
               }
            }
         }
         return moneyToAddPool;
      }

      /// <summary>
      /// Add row to the board.
      /// </summary>
      /// <param name="boardRow"></param>
      public void AddRow(BoardRow boardRow)
      {
         MasterBoard.Add(boardRow);
      }

      /// <summary>
      /// Calculates the total amount for the user
      /// by calculating each square with that user.
      /// </summary>
      /// <param name="user"></param>
      /// <returns>The amount calculated.</returns>
      public float CalculateUserAmount(ref User_NS.User user)
      {
         float totalAmount = 0.0F;

         foreach(BoardRow row in MasterBoard)
         {
            List<Square> squares = row.FindSquaresByUser(user);
            foreach(Square square in squares)
            {
               totalAmount += square.CashAmount.Value;
            }
         }

         user.SetMoneyAmount(new User_NS.Money(totalAmount));

         return totalAmount;
      }

      /// <summary>
      /// Returns the cash total based on the number
      /// of squares filled in.
      /// </summary>
      /// <returns></returns>
      public User_NS.Money GetCashTotal()
      {
         User_NS.Money totalAmount = 0;
         foreach (BoardRow row in MasterBoard)
         {
            foreach(Square square in row.Squares)
            {
               if(square.IsFilled)
               {
                  totalAmount += square.CashAmount;
               }
            }
         }

         return totalAmount;
      }

      /// <summary>
      /// Update each square on the board.
      /// </summary>
      public void Update()
      {
         foreach (BoardRow row in MasterBoard)
         {
            foreach (Square square in row.Squares)
            {
               square.Update();
            }
         }
      }

      /// <summary>
      /// Returns a square at a location on the board.
      /// </summary>
      /// <param name="location"></param>
      /// <returns></returns>
      public Square FindSquareByLocation(BoardLocation location)
      {
         Square square = null;
         foreach(BoardRow row in MasterBoard)
         {
            square = row.FindSquareByLocation(location);
            if(square != null)
            {
               break;
            }
         }
         return square;
      }

      /// <summary>
      /// Returns a square with a given user.
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public Square FindSquareByUser(User_NS.User user)
      {
         Square square = null;
         foreach (BoardRow row in MasterBoard)
         {
            square = row.FindSquareByUser(user);
            if (square != null)
            {
               break;
            }
         }
         return square;
      }

      /// <summary>
      /// Returns a square with a given label.
      /// </summary>
      /// <param name="label"></param>
      /// <returns></returns>
      public Square FindSquareByLabel(Label label)
      {
         Square square = null;
         foreach (BoardRow row in MasterBoard)
         {
            square = row.FindSquareByLabel(label);
            if (square != null)
            {
               break;
            }
         }
         return square;
      }

      /// <summary>
      /// Removes squares of a specified user.
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public float ClearSquares(User_NS.User user)
      {
         float moneyReturn = 0;
         foreach (BoardRow row in MasterBoard)
         {
            foreach (Square squareFound in row.FindSquaresByUser(user))
            {
               moneyReturn += squareFound.CashAmount.Value;
               squareFound.Clear();
            }
         }

         return moneyReturn;
      }

      public BoardRow GetRow(int rowNumber)
      {
         return MasterBoard[rowNumber];
      }

      public void AddLabels(List<List<Label>> labels)
      {
         m_BoardLabels = labels;
      }

      /// <summary>
      /// Organizes the labels/squares so that they're 
      /// right next to each other.
      /// </summary>
      /// <param name="labels"></param>
      public void OrganizeLabels(List<List<Label>> labels, Label headerRowSquare, Label headerColSquare)
      {
         if (labels.Count > 0)
         {
            Label firstSquare = labels[0][0];
            Point firstPoint = new Point();
            firstPoint.Y = headerRowSquare.Location.Y + headerRowSquare.Size.Height;
            firstPoint.X = headerColSquare.Location.X + headerColSquare.Size.Width;
            firstSquare.Location = firstPoint;

            for (int rowIndex = 0; rowIndex < labels.Count; rowIndex++)
            {
               Label firstLabel = labels[rowIndex][0];

               if (rowIndex > 0)
               {
                  Label previousFirstRowLabel = labels[rowIndex - 1][0];
                  Point previousPoint = previousFirstRowLabel.Location;
                  previousPoint.X = previousFirstRowLabel.Location.X;
                  previousPoint.Y = previousFirstRowLabel.Location.Y + previousFirstRowLabel.Size.Height;
                  firstLabel.Location = previousPoint;
               }

               OrganizeRow(labels[rowIndex], firstLabel, true);
            }
         }
      }

      /// <summary>
      ///  Organizes either row or column depending on the 3rd argument
      /// </summary>
      /// <param name="labels">List of labels</param>
      /// <param name="firstLabel">The first label to go off of</param>
      /// <param name="isRow">Flag for seeing if a row or column needs to be organized.</param>
      public void OrganizeRow(List<Label> labels, Label firstLabel = null, bool isRow = true)
      {
         if (labels.Count > 0)
         {
            if (firstLabel == null)
            {
               firstLabel = labels[0];
            }

            Label previousLabel = null;
            for (int colIndex = 0; colIndex < labels.Count; colIndex++)
            {
               Label label = labels[colIndex];
               if (label.Name != firstLabel.Name)
               {
                  if (previousLabel == null)
                  {
                     previousLabel = firstLabel;
                  }

                  Point newPoint = label.Location;
                  if (isRow)
                  {
                     newPoint.X = previousLabel.Location.X + previousLabel.Size.Width;
                     newPoint.Y = previousLabel.Location.Y;
                  }
                  else
                  {
                     newPoint.Y = previousLabel.Location.Y + previousLabel.Size.Height;
                     newPoint.X = previousLabel.Location.X;
                  }
                  label.Location = newPoint;
               }

               previousLabel = label;
            }
         }
      }

      /// <summary>
      /// Returns a list of available squares
      /// </summary>
      /// <returns></returns>
      public List<Square> GetAvailableSquares()
      {
         List<Square> availableSquares = new List<Square>();
         foreach(BoardRow boardRow in MasterBoard)
         {
            foreach(Square square in boardRow.Squares)
            {
               if(!square.IsFilled && !square.IsHeaderSquare)
               {
                  availableSquares.Add(square);
               }
            }
         }

         return availableSquares;
      }

      /// <summary>
      /// Return the number of remaining squares
      /// </summary>
      /// <returns></returns>
      public int GetNumberOfSquaresAvailable()
      {
         return GetAvailableSquares().Count;
      }

      public string TeamOne
      {
         get { return m_TeamOneName; }
         set { m_TeamOneName = value; }
      }

      public string TeamTwo
      {
         get { return m_TeamTwoName; }
         set { m_TeamTwoName = value; }
      }

      public List<BoardRow> MasterBoard
      {
         get;
         private set;
      }

      public BoardRow HeaderRow
      {
         get;
         set;
      }

      public BoardRow HeaderColumn
      {
         get;
         set;
      }

      public Square WinningSquare
      {
         get;
         private set;
      }

      public Generator BoardGenerator
      {
         get;
         private set;
      }

      public int RowSize
      {
         get
         {
            if (MasterBoard != null)
            {
               return MasterBoard.Count;
            }
            else
            {
               return -1;
            }
         }
      }

      public int ColSize
      {
         get
         {
            if(MasterBoard[0] != null)
            {
               return MasterBoard[0].Count;
            }
            else
            {
               return -1;
            }
         }
      }

      private bool IncrementRows()
      {
         bool maxReached = true;
         if(m_Rows < m_MaxRows)
         {
            m_Rows++;
            maxReached = false;
         }
         m_MaxRowsReached = maxReached;
         return maxReached;
      }

      private Square GetFirstColumnSquare(int rowID)
      {
         return HeaderColumn[rowID];
      }

      private string m_TeamOneName;
      private string m_TeamTwoName;
      private int m_Rows;
      private int m_MaxRows;
      private int m_MaxCols;
      private bool m_MaxRowsReached;

      private List<List<Label>> m_BoardLabels;
   }
}
