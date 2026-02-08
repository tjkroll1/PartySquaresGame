using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Board_NS;
using FileIO;
using Score_NS;
using TeamName_NS;
using User_NS;

namespace Party_Squares_Game
{
   public enum QuarterEnum
   {
      FIRST  = 1,
      SECOND = 2,
      THIRD  = 3,
      FOURTH = 4,
      FINAL  = 5
   };

   public struct QuarterStruct
   {
      public QuarterStruct(int id, string name, QuarterEnum quarter)
      {
         quarterInt = id;
         quarterString = name;
         quarterEnum = quarter;
      }

      public override bool Equals(object rhs)
      {
         if(!(rhs is QuarterStruct))
         {
            return false;
         }

         return quarterEnum == ((QuarterStruct)rhs).quarterEnum &&
            quarterInt == ((QuarterStruct)rhs).quarterInt &&
            quarterString == ((QuarterStruct)rhs).quarterString;
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

      public static bool operator>(QuarterStruct lhs, QuarterStruct rhs)
      {
         return (int)lhs.quarterEnum > (int)rhs.quarterEnum;
      }

      public static bool operator<(QuarterStruct lhs, QuarterStruct rhs)
      {
         return !(lhs > rhs);
      }

      public QuarterEnum quarterEnum;
      public string quarterString;
      public int quarterInt;
   }

   /// <summary>
   /// Handles most operations done by the main GUI
   /// </summary>
   public class GameManager
   {
      /// <summary>
      /// Default constructor
      /// </summary>
      public GameManager()
      {
         InitializeVariables();
      }

      /// <summary>
      /// Constructor with SquaresBoardForm parameter.
      /// </summary>
      /// <param name="squaresBoard"></param>
      public GameManager(SquaresBoardForm squaresBoard)
      {
         MainForm = squaresBoard;

         InitializeVariables();
      }

      /// <summary>
      /// Initialize Game Manager Attributes
      /// </summary>
      private void InitializeVariables()
      {
         Board = new Board();

         SetupTimer();

         NumbersGenerated = false;
         m_UserList = new UserList();
         m_LabelDictionary = new Dictionary<string, Label>();
         CashPoolAmount = new Money();
         CurrentQuarter = new QuarterStruct();
         GameStarted = false;
         CurrentGameScore = new CurrentScore();
         WinnerLabel = new Label();
         PerQuarterPayout = new Money();
         MoneyPaidOut = new Money();
         LockPictureBox = null;
         QuarterWinnerMap = new Dictionary<QuarterStruct, string>();
         m_PictureBoxLibrary = new Dictionary<string, PictureBox>();
         m_DefaultCostPerSquare = new Money(1);
         Winner = new WinnerClass();
      }

      /// <summary>
      /// Sets up the game timer
      /// </summary>
      private void SetupTimer()
      {
         const int GAME_TIMER_INTERVAL = 500; // Once every 1/10 of a second
         GameTimer = new Timer();
         GameTimer.Interval = GAME_TIMER_INTERVAL;
         GameTimer.Tick += OnTimerTick;
         GameTimer.Start();
      }

      /// <summary>
      /// Called on tick event
      /// </summary>
      /// <param name="source"></param>
      /// <param name="timeEvent"></param>
      private void OnTimerTick(object source, EventArgs timeEvent)
      {
         SetCashPool();
      }

      /// <summary>
      /// Set Board Object
      /// </summary>
      /// <param name="board"></param>
      public void SetBoard(Board board)
      {
         Board = board;
      }

      /// <summary>
      /// Set the remaining squares label object in the dictionary.
      /// </summary>
      /// <param name="remainingSquaresLabel"></param>
      public void SetRemainingSquaresLabel(Label remainingSquaresLabel)
      {
         if (remainingSquaresLabel != null)
         {
            const string labelKey = "RemainingSquaresLabel";
            m_LabelDictionary.Add(labelKey, remainingSquaresLabel);
            UpdateRemainingSquaresLabel();
         }
      }

      /// <summary>
      /// Sets the paths to the images on the game board.
      /// </summary>
      public void SetImagePath()
      {
         string imagePath = Environment.CurrentDirectory + @"\Images\";
         string imagePath2 = Environment.CurrentDirectory + @"\..\..\..\Images\";
         string testFile = "the-duke-football.jpg";
         if (FileUtilities.FileExists(imagePath + testFile))
         {
            m_ImagesLocationPath = imagePath;
         }
         else if(FileUtilities.FileExists(imagePath2 + testFile))
         {
            m_ImagesLocationPath = imagePath2;
         }
         else
         {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            folderBrowserDialog.ShowDialog();
            m_ImagesLocationPath = folderBrowserDialog.SelectedPath + "\\";
         }
      }

      /// <summary>
      /// Start the game actions
      /// </summary>
      public void StartGame()
      {
         GameStarted = true;
         SetSquaresLock(true);
         Board.StartBoard();
         SetInitalScore();
      }

      /// <summary>
      /// Method to handle restarting the game.
      /// </summary>
      public void RestartGame()
      {
         GameStarted = false;
         SetSquaresLock(false);
         Board.Reset();
         m_UserList.Clear();
         SetQuarter(QuarterEnum.FIRST, fillLabel:false, setComboBox: true);

         WinnerLabel.Text = "";

         Winner.Reset();
         ResetCashPoolLabel();
      }

      /// <summary>
      /// Set the Winner Label object
      /// </summary>
      /// <param name="label"></param>
      public void SetWinnerLabel(Label label)
      {
         if(!Winner.Null())
         {
            Winner.SetWinnerLabel(label);
         }
      }

      public void SetDefaultCostPerSquare(int money)
      {
         m_DefaultCostPerSquare = new Money(money);
      }

      public void AddTeamPictureBoxes(PictureBox home, PictureBox away)
      {
         if(m_PictureBoxLibrary != null)
         {
            m_PictureBoxLibrary.Add(Team1.Name, home);
            m_PictureBoxLibrary.Add(Team2.Name, away);
         }
         SetTeamLogoLocations();
      }

      protected void SetTeamLogoLocations()
      {
         if (GameConfigurationFile != null)
         {
            string homePath = GameConfigurationFile.TeamImage1;
            string awayPath = GameConfigurationFile.TeamImage2;

            if (FileUtilities.PathsExist(new List<string>(){homePath, awayPath}))
            {
               m_PictureBoxLibrary[Team1.Name].ImageLocation = homePath;
               m_PictureBoxLibrary[Team2.Name].ImageLocation = awayPath;
            }
            else
            {
               homePath = m_ImagesLocationPath + "New_England_Patriots.jpg";
               awayPath = m_ImagesLocationPath + "Philadephia_Eagles_Logo.jpg";
               m_PictureBoxLibrary[Team1.Name].ImageLocation = homePath;
               m_PictureBoxLibrary[Team2.Name].ImageLocation = awayPath;
            }
         }
      }

      public void AddFootballImagePictureBox(PictureBox football)
      {
         string fileName = "the-duke-football.jpg";
         if (m_PictureBoxLibrary != null)
         {
            m_PictureBoxLibrary.Add("Football", football);
         }
         if(FileUtilities.FileExists(m_ImagesLocationPath + fileName))
         {
            m_PictureBoxLibrary["Football"].ImageLocation = m_ImagesLocationPath + fileName;
         }
      }

      /// <summary>
      /// Switch which team labels are used.
      /// </summary>
      /// <param name="teamLabel"></param>
      public void ChangeTeamLabel(Label teamLabel)
      {
         TeamName_NS.TeamNameForm teamNameForm = new TeamName_NS.TeamNameForm(teamLabel);
         teamNameForm.ShowDialog();
         m_LabelDictionary[teamLabel.Name] = teamLabel;
      }

      /// <summary>
      /// Sets the team objects and the current score object.
      /// </summary>
      public void SetTeams()
      {
         Team1 = new Team(GameConfigurationFile.TeamName1); // Home
         Team2 = new Team(GameConfigurationFile.TeamName2); // Away

         CurrentGameScore.SetTeams(Team1, Team2);
      }

      /// <summary>
      /// Set the initial score of the game (0 - 0).
      /// </summary>
      protected void SetInitalScore()
      {
         SetScore(0, 0);
      }

      /// <summary>
      /// Sets the Game Score
      /// </summary>
      /// <param name="homeScoreInt"></param>
      /// <param name="awayScoreInt"></param>
      /// <returns></returns>
      public bool SetScore(int homeScoreInt, int awayScoreInt)
      {
         if (GameStarted)
         {
            Score homeScore = new Score(homeScoreInt);
            Score awayScore = new Score(awayScoreInt);

            Team1.Score = homeScore;
            Team2.Score = awayScore;

            Square winnerSquare = Board.SetScore(homeScore, awayScore);
            SetWinnerInformation(winnerSquare);
         }

         return GameStarted;
      }

      /// <summary>
      /// Sets the clear label.
      /// </summary>
      /// <param name="label"></param>
      public void SetSelectedLabel(Label label)
      {
         const string labelKey = "SelectedLabel";
         m_LabelDictionary[labelKey] = label;
      }

      /// <summary>
      /// Returns the selected label object and removes
      /// it from the dictionary.
      /// </summary>
      /// <returns></returns>
      public Label GetAndRemoveSelectedLabel()
      {
         const string labelKey = "SelectedLabel";
         Label selectedLabel = null;
         if(m_LabelDictionary.ContainsKey(labelKey))
         {
            selectedLabel = m_LabelDictionary[labelKey];
            m_LabelDictionary.Remove(labelKey);
         }
         return selectedLabel;
      }

      /// <summary>
      /// Update how many squares remain to be filled in.
      /// </summary>
      public void UpdateRemainingSquaresLabel()
      {
         const string labelKey = "RemainingSquaresLabel";
         if (m_LabelDictionary.ContainsKey(labelKey))
         {
            Label remainingSquaresLabel = m_LabelDictionary[labelKey];
            if (remainingSquaresLabel != null)
            {
               int numberOfRemainingSquares = Board.GetNumberOfSquaresAvailable();
               remainingSquaresLabel.Text = $"Available:\n{numberOfRemainingSquares} Squares";
            }
         }
      }

      /// <summary>
      /// The action when the user double clicks on any board square.
      /// </summary>
      /// <param name="clickedLabel"></param>
      public void AddUserToSquare(Label clickedLabel)
      {
         if (!SquaresLocked)
         {
            Square clickedSquare = Board.FindSquareByLabel(clickedLabel);
            if (clickedSquare != null)
            {
               UserForm userForm = new UserForm(clickedSquare, m_UserList, m_DefaultCostPerSquare);
               userForm.SetRemainingSquares(Board.GetNumberOfSquaresAvailable());
               userForm.ShowDialog();
               if (!userForm.WindowClosed)
               {
                  User user = userForm.NewUser;

                  if (userForm.AdditionalSquares > 0)
                  {
                     Board.AddUserToRandomSquares(user, userForm.MoneyAmount, userForm.AdditionalSquares);
                  }

                  if (Board.CalculateUserAmount(ref user) > 0)
                  {
                     userForm.UserList.SortList();
                  }
                  else
                  {
                     RemoveUserFromUserList(user.Name);
                  }

                  Board.Update();

                  UpdateRemainingSquaresLabel();
               }
            }
         }
      }

      /// <summary>
      /// Add cash pool label to the label dictionary
      /// </summary>
      /// <param name="cashPoolLabel"></param>
      public void AddCashPoolLabel(Label cashPoolLabel)
      {
         m_LabelDictionary[cashPoolLabel.Name] = cashPoolLabel;
      }

      /// <summary>
      /// Set the cash pool label.
      /// </summary>
      public void SetCashPool()
      {
         Money cashPoolAmount = Board.GetCashTotal();
         cashPoolAmount -= MoneyPaidOut;

         const int MAX_QUARTERS = 5;
         PerQuarterPayout = cashPoolAmount / (MAX_QUARTERS - CurrentQuarter.quarterInt);

         UpdateCashPoolLabel(cashPoolAmount, PerQuarterPayout);
      }

      /// <summary>
      /// Update the Cash Pool Label
      /// </summary>
      /// <param name="payout"></param>
      public void UpdateCashPoolLabel(Money totalPayout, Money perQuarter)
      {
         m_LabelDictionary["cashPoolLabel"].Text = $"Current Cash Pool: {totalPayout.StringShortAmount}\n\n";
         m_LabelDictionary["cashPoolLabel"].Text += $"Per Quarter Payout: {perQuarter.StringShortAmount}";
      }

      /// <summary>
      /// Reset the cash pool label
      /// </summary>
      public void ResetCashPoolLabel()
      {
         Money zero = new Money();
         UpdateCashPoolLabel(zero, zero);
      }

      /// <summary>
      /// Set the user list box control
      /// </summary>
      /// <param name="listbox"></param>
      public void AddListBox(ListView listbox)
      {
         m_UserList.SetListBox(listbox);
      }

      /// <summary>
      /// Set the tool tip lable text
      /// </summary>
      /// <param name="label"></param>
      public void SetToolTip(Label label)
      {
         ToolTip toolTip = new ToolTip();
         toolTip.SetToolTip(label, label.Text);
      }

      /// <summary>
      /// Clears the specified square.
      /// </summary>
      public void ClearSquare(Label selectedSquare)
      {
         const string labelKey = "SelectedLabel";
         if (selectedSquare != null)
         {
            Square clearSquare = Board.FindSquareByLabel(selectedSquare);
            User userRemoved = clearSquare.SquareUser;
            CashPoolAmount -= clearSquare.CashAmount;
            SetCashPool();
            clearSquare.Clear();
            CheckRemoveUser(userRemoved);
            m_UserList.RefreshListBox();
            UpdateRemainingSquaresLabel();

            m_LabelDictionary.Remove(labelKey);
         }
      }

      /// <summary>
      /// Clears the squares of a specified user
      /// </summary>
      /// <param name="user"></param>
      public void ClearSquares(User user)
      {
         Money clearMoney = new Money(Board.ClearSquares(user));
         CashPoolAmount -= clearMoney;
         m_UserList.RefreshListBox();
         UpdateRemainingSquaresLabel();
      }

      /// <summary>
      /// Checks to see if the user exists on the board.
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public bool CheckRemoveUser(User user)
      {
         bool userRemoved = true;
         float calculatedAmount = Board.CalculateUserAmount(ref user);
         if(calculatedAmount <= 0)
         {
            userRemoved = RemoveUserFromGame(user);

            UpdateRemainingSquaresLabel();
         }

         return userRemoved;
      }

      /// <summary>
      /// Go to the next quarter
      /// </summary>
      public void NextQuarter()
      {
         QuarterComboBox.SelectedIndex = QuarterComboBox.SelectedIndex + 1;

         SetQuarter(QuarterComboBox);
      }

      /// <summary>
      /// Set the quarter combo box object
      /// </summary>
      /// <param name="comboBox"></param>
      public void SetQuarterComboBox(ComboBox comboBox)
      {
         QuarterComboBox = comboBox;
      }

      /// <summary>
      /// Set the current quarter
      /// </summary>
      /// <param name="quarterEnum"></param>
      /// <param name="fillLabel"></param>
      public void SetQuarter(QuarterEnum quarterEnum, bool fillLabel = true, bool setComboBox = false)
      {
         QuarterStruct previousQuarter = CurrentQuarter;
         CurrentQuarter = GetQuarter((int)quarterEnum);
         if (previousQuarter.quarterInt > 0 && fillLabel)
         {
            FillQuarterLabel(previousQuarter);
         }

         if(setComboBox)
         {
            QuarterComboBox.SelectedIndex = ((int)quarterEnum) - 1;
         }
      }

      /// <summary>
      /// Set the current quarter
      /// </summary>
      /// <param name="cb"></param>
      public void SetQuarter(ComboBox cb)
      {
         QuarterStruct currentQuarter;
         QuarterStruct previousQuarter = CurrentQuarter;
         currentQuarter = GetQuarter(cb.SelectedIndex);
         if (previousQuarter.quarterInt > 0 && previousQuarter.quarterInt < currentQuarter.quarterInt && GameStarted)
         {
            FillQuarterLabel(previousQuarter);
         }
         else if(previousQuarter.quarterInt >= currentQuarter.quarterInt && GameStarted)
         {
            ClearQuarterLabel(currentQuarter);
         }

         if(CheckQuarterChangeForFinal(cb))
         {
            FinalGameOperations(cb);
         }
         else
         {
            CurrentQuarter = currentQuarter;
         }
      }

      /// <summary>
      /// Get the Quarter Struct based on the index.
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      private QuarterStruct GetQuarter(int index)
      {
         QuarterStruct quarter = new QuarterStruct();
         switch(index)
         {
            case 0:
               quarter.quarterEnum = QuarterEnum.FIRST;
               quarter.quarterString = "1st Quarter";
               quarter.quarterInt = 1;
               break;
            case 1:
               quarter.quarterEnum = QuarterEnum.SECOND;
               quarter.quarterString = "2nd Quarter";
               quarter.quarterInt = 2;
               break;
            case 2:
               quarter.quarterEnum = QuarterEnum.THIRD;
               quarter.quarterString = "3rd Quarter";
               quarter.quarterInt = 3;
               break;
            case 3:
               quarter.quarterEnum = QuarterEnum.FOURTH;
               quarter.quarterString = "4th Quarter";
               quarter.quarterInt = 4;
               break;
            case 4:
               quarter.quarterEnum = QuarterEnum.FINAL;
               quarter.quarterString = "Final";
               quarter.quarterInt = 5;
               break;
            default:
               quarter.quarterEnum = QuarterEnum.FIRST;
               quarter.quarterString = "1st Quarter";
               quarter.quarterInt = 1;
               break;
         }
         return quarter;
      }

      /// <summary>
      /// Return true if the quarter combobox has
      /// been set to "Final".
      /// </summary>
      /// <param name="quarterComboBox"></param>
      /// <returns></returns>
      private bool CheckQuarterChangeForFinal(ComboBox quarterComboBox)
      {
         return quarterComboBox.Text == "Final";
      }

      /// <summary>
      /// Occurs when final has been set in the quarter dropdown.
      /// Verifies with user if this is the case and locks all controls.
      /// </summary>
      private void FinalGameOperations(ComboBox quarterComboBox)
      {
         DialogResult gameOverResult = MessageBox.Show("Final has been detected!\n\nContinue?", "Game's Over", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

         if (gameOverResult == DialogResult.Yes)
         {
            MainForm.LockAllControls();
         }
         else
         {
            quarterComboBox.SelectedIndex = quarterComboBox.SelectedIndex - 1;
         }
      }

      /// <summary>
      /// Set the Lock Picture box object
      /// </summary>
      /// <param name="pictureBox"></param>
      public void SetLockPictureBox(PictureBox pictureBox)
      {
         LockPictureBox = pictureBox;

         SetSquaresLock(false);
      }

      /// <summary>
      /// Set the game configuration file
      /// </summary>
      /// <param name="gameConfiguration"></param>
      public void SetGameConfigurationFile(GameConfiguration gameConfiguration)
      {
         GameConfigurationFile = gameConfiguration;
      }

      /// <summary>
      /// Add to the Quarter Label Library
      /// </summary>
      /// <param name="id"></param>
      /// <param name="quarterLabel"></param>
      public void SetQuarterLabel(Label quarterLabel)
      {
         WinnerLabel = quarterLabel;
      }

      /// <summary>
      /// Fill Quarter Labels
      /// </summary>
      /// <param name="previousQuarter"></param>
      public void FillQuarterLabel(QuarterStruct previousQuarter)
      {
         string labelText = "";
         string scoreText;
         string cashPayout = PerQuarterPayout.ToString();
         Team homeTeam = CurrentGameScore.TeamScore1;
         Team awayTeam = CurrentGameScore.TeamScore2;

         if (homeTeam.Score > awayTeam.Score)
         {
            scoreText = $"{homeTeam.Name} {homeTeam.Score.ScoreString} - {awayTeam.Score.ScoreString}";
         }
         else if (homeTeam.Score < awayTeam.Score)
         {
            scoreText = $"{awayTeam.Name} {awayTeam.Score.ScoreString} - {homeTeam.Score.ScoreString}";
         }
         else
         {
            scoreText = $"Tied {awayTeam.Score.ScoreString} - {homeTeam.Score.ScoreString}";
         }

         Square winnerSquare = Board.WinningSquare;
         if (winnerSquare != null)
         {
            string winnerName = winnerSquare.SquareUser != null ?
               winnerSquare.SquareUser.Name : "";

            if(winnerName == "")
            {
               labelText += previousQuarter.quarterString + "\n";
               labelText += scoreText + "\n";
               labelText += $"Winner: None\n";
               QuarterWinnerMap[previousQuarter] = labelText;
            }
            else
            {
               labelText += previousQuarter.quarterString + "\n";
               labelText += scoreText + "\n";
               labelText += $"Winner: {winnerName}\n";
               labelText += $"Payout: {cashPayout}\n";
               QuarterWinnerMap[previousQuarter] = labelText;
               MoneyPaidOut += PerQuarterPayout;
            }
         }
         else
         {
            labelText += previousQuarter.quarterString + "\n";
            labelText += scoreText + "\n";
            labelText += "No one won that quarter\n";
            QuarterWinnerMap[previousQuarter] = labelText;
         }

         SetWinnerLabelText(QuarterWinnerMap);
      }

      /// <summary>
      /// Sets the winner label text string.
      /// </summary>
      /// <param name="quarterWinnerMap"></param>
      private void SetWinnerLabelText(Dictionary<QuarterStruct, string> quarterWinnerMap)
      {
         WinnerLabel.Text = "";

         foreach(KeyValuePair<QuarterStruct, string> quarterWinner in quarterWinnerMap)
         {
            WinnerLabel.Text += quarterWinner.Value + "\n";
         }
      }

      /// <summary>
      /// Clears a quarter label
      /// </summary>
      /// <param name="quarterToClear"></param>
      public void ClearQuarterLabel(QuarterStruct quarterToClear)
      {
         QuarterWinnerMap[quarterToClear] = quarterToClear.quarterString;
      }

      /// <summary>
      /// Removes the user from the game if they aren't on the board anymore.
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public bool RemoveUserFromGame(User user)
      {
         return m_UserList.RemoveUser(user.Name);
      }

      /// <summary>
      /// Remove user from user listbox
      /// </summary>
      /// <param name="name"></param>
      public void RemoveUserFromUserList(string name)
      {
         StripInputString(ref name);
         DialogResult removeResult = MessageBox.Show($"Are you sure you want to remove user {name}?", "Remove User?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
         if (removeResult == DialogResult.Yes)
         {
            User removedUser = m_UserList.GetUser(name);
            bool removedSuccess = m_UserList.RemoveUser(name);
            ClearSquares(removedUser);
            m_UserList.RemoveUser(name);
            if (!removedSuccess)
            {
               MessageBox.Show($"{name} was not removed successfully", "Failed to Remove User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
      }

      /// <summary>
      /// Strip the input string
      /// </summary>
      /// <param name="name"></param>
      private void StripInputString(ref string name)
      {
         int indexCounter = 0;
         foreach(char c in name)
         {
            if(c == '(')
            {
               indexCounter--;
               break;
            }
            indexCounter++;
         }
         name = name.Substring(0, indexCounter);
      }

      /// <summary>
      /// Set the winner information label
      /// </summary>
      /// <param name="square"></param>
      public void SetWinnerInformation(Square square)
      {
         Winner.SetSquare(square);
      }

      /// <summary>
      /// Set by the squaresboard class
      /// </summary>
      /// <param name="locked"></param>
      public void SetSquaresLock(bool locked)
      {
         SquaresLocked = locked && GameStarted;

         if (LockPictureBox != null && m_ImagesLocationPath != "")
         {
            if (!SquaresLocked)
            {
               LockPictureBox.ImageLocation = m_ImagesLocationPath + "Unlock.jpg";
            }
            else
            {
               LockPictureBox.ImageLocation = m_ImagesLocationPath + "Lock.jpg";
            }
         }
      }

      // *** Defined Game Manager Properties ***

      public bool NumbersGenerated
      {
         get;
         private set;
      }

      public bool GameStarted
      {
         get;
         private set;
      }

      public Board Board
      {
         get;
         private set;
      }

      public PictureBox LockPictureBox
      {
         get;
         private set;
      }

      public Label WinnerLabel
      {
         get;
         private set;
      }

      private Dictionary<QuarterStruct, string> QuarterWinnerMap
      {
         get;
         set;
      }

      public GameConfiguration GameConfigurationFile
      {
         get;
         private set;
      }

      /// <summary>
      /// Home Team
      /// </summary>
      public Team Team1
      {
         get;
         set;
      }

      /// <summary>
      /// Away Team
      /// </summary>
      public Team Team2
      {
         get;
         set;
      }

      public CurrentScore CurrentGameScore
      {
         get;
         private set;
      }

      public WinnerClass Winner
      {
         get;
         private set;
      }

      public Money PerQuarterPayout
      {
         get;
         private set;
      }

      public Money MoneyPaidOut
      {
         get;
         private set;
      }

      private ComboBox QuarterComboBox
      {
         get;
         set;
      }

      private SquaresBoardForm MainForm
      {
         get;
         set;
      }

      /// <summary>
      /// Contains whether the squares are locked or not
      /// </summary>
      public bool SquaresLocked
      {
         get;
         private set;
      }

      /// <summary>
      /// Contains the cash pool money amount
      /// </summary>
      private Money CashPoolAmount
      {
         get;
         set;
      }
      private QuarterStruct CurrentQuarter
      {
         get; set;
      }

      private Timer GameTimer
      {
         get;
         set;
      }

      //Stores all labels except board squares
      private Dictionary<string, Label> m_LabelDictionary;
      private UserList m_UserList;
      private string m_ImagesLocationPath;
      private Dictionary<string, PictureBox> m_PictureBoxLibrary;
      private Money m_DefaultCostPerSquare;
   }
}
