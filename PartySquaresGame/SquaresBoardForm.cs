using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Board_NS;
using User_NS;

namespace Party_Squares_Game
{
   public partial class SquaresBoardForm : Form
   {
      GameManager gameManager;
      List<List<Label>> board;
      List<Label> headerRow;
      List<Label> headerColumn;

      public SquaresBoardForm()
      {
         InitializeComponent();
         InitializeGameManager();
      }

      public void InitializeLabels()
      {
         headerRow = new List<Label>() { labelHeaderRow0, labelHeaderRow1, labelHeaderRow2, labelHeaderRow3, labelHeaderRow4, labelHeaderRow5, labelHeaderRow6, labelHeaderRow7, labelHeaderRow8, labelHeaderRow9 };
         headerColumn = new List<Label>() { labelHeaderColumn0, labelHeaderColumn1, labelHeaderColumn2, labelHeaderColumn3, labelHeaderColumn4, labelHeaderColumn5, labelHeaderColumn6, labelHeaderColumn7, labelHeaderColumn8, labelHeaderColumn9 };
         board = new List<List<Label>>()
         {
            new List<Label>(){label00, label01, label02, label03, label04, label05, label06, label07, label08, label09},
            new List<Label>(){label10, label11, label12, label13, label14, label15, label16, label17, label18, label19},
            new List<Label>(){label20, label21, label22, label23, label24, label25, label26, label27, label28, label29},
            new List<Label>(){label30, label31, label32, label33, label34, label35, label36, label37, label38, label39},
            new List<Label>(){label40, label41, label42, label43, label44, label45, label46, label47, label48, label49},
            new List<Label>(){label50, label51, label52, label53, label54, label55, label56, label57, label58, label59},
            new List<Label>(){label60, label61, label62, label63, label64, label65, label66, label67, label68, label69},
            new List<Label>(){label70, label71, label72, label73, label74, label75, label76, label77, label78, label79},
            new List<Label>(){label80, label81, label82, label83, label84, label85, label86, label87, label88, label89},
            new List<Label>(){label90, label91, label92, label93, label94, label95, label96, label97, label98, label99}
         };

         if(gameManager.Board == null)
         {
            gameManager.SetBoard(new Board());
         }

         Board mainBoard = gameManager.Board;

         mainBoard.GenerateHeaderRow(headerRow);
         mainBoard.GenerateHeaderColumn(headerColumn);

         // Need the first square to be aligned with the 2nd square of the header
         // row and column.
         mainBoard.OrganizeRow(headerRow, labelBlank);
         mainBoard.OrganizeRow(headerColumn, labelBlank, false);
         mainBoard.OrganizeLabels(board, headerRow[1], headerColumn[1]);
         mainBoard.AddLabels(board);
         mainBoard.PopulateBoard();
      }

      private void SquaresBoardForm_Load(object sender, EventArgs e)
      {
         QuarterCB.SelectedIndex = 0;
      }

      /// <summary>
      /// Initialize the Game Manager attributes
      /// </summary>
      private void InitializeGameManager()
      {
         gameManager = new GameManager(this);
         ConfigureGameDataFile();
         InitializeLabels();
         gameManager.SetTeams();
         gameManager.SetImagePath();
         gameManager.AddListBox(UsersListBox);
         gameManager.AddCashPoolLabel(cashPoolLabel);
         gameManager.SetLockPictureBox(lockPictureBox);
         gameManager.SetQuarterComboBox(QuarterCB);
         gameManager.SetWinnerLabel(currentWinnerLabel);
         gameManager.SetRemainingSquaresLabel(remainingSquaresLabel);
         InitiateTeamLogos();
         SetQuarterLabels();

         lockPictureBox.DoubleClick += LockPictureBox_DoubleClicked;
      }

      private void InitiateTeamLogos()
      {
         gameManager.AddTeamPictureBoxes(homeTeamLogo, awayTeamLogo);
      }

      private void SetQuarterLabels()
      {
         gameManager.SetQuarterLabel(winnerLabel);
      }

      private void quitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Handles setting the user and amount in any given square.
      /// Disables once the numbers have been generated.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void labelHandler_DoubleClick(object sender, EventArgs e)
      {
         if (!gameManager.NumbersGenerated)
         {
            gameManager.AddUserToSquare((Label)sender);
         }
      }
      
      /// <summary>
      /// Shows a tooltip balloon when the user hovers over a square.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void labelHandler_MouseHover(object sender, EventArgs e)
      {
         gameManager.SetToolTip((Label)sender);
      }

      //*************************//
      /// <summary>
      /// Populate Numbers Event Handler: Populates the numbers in the header row & column
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void populateNumbersEventHander(object sender, EventArgs e)
      {
         gameManager.StartGame();
         startGameButton.Visible = false;
      }

      /// <summary>
      /// Mouse down event handler for each label.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void labelHandler_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            Label clickedLabel = (Label)sender;
            gameManager.SetSelectedLabel(clickedLabel);
         }
      }

      private void homeTeamLabel_DoubleClick(object sender, EventArgs e)
      {
         gameManager.ChangeTeamLabel((Label)sender);
      }

      private void awayTeamLabel_DoubleClick(object sender, EventArgs e)
      {
         gameManager.ChangeTeamLabel((Label)sender);
      }

      private void QuarterCB_SelectedIndexChanged(object sender, EventArgs e)
      {
         gameManager.SetQuarter(QuarterCB);
      }

      /// <summary>
      /// Activated by the right click context menu.
      /// Sender is the rightclickmenu and not the label.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void rightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
      {
         ContextMenuStrip clickedStrip = (ContextMenuStrip)sender;

         if (e.ClickedItem.Name == removeUserMenuItem.Name)
         {
            Label selectedLabel = gameManager.GetAndRemoveSelectedLabel();
            if (selectedLabel != null && selectedLabel.Text != "")
            {
               gameManager.ClearSquare(selectedLabel);
            }
         }
         else if(e.ClickedItem.Name == addUserMenuItem.Name)
         {
            Label selectedLabel = gameManager.GetAndRemoveSelectedLabel();
            if (selectedLabel != null)
            {
               gameManager.AddUserToSquare(selectedLabel);
            }
         }
      }

      private void SquaresBoardForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         DialogResult closingResult = MessageBox.Show("Are you sure you want to exit the game?", "Exiting...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
         if(closingResult == DialogResult.No)
         {
            e.Cancel = true;
         }
         else
         {
            e.Cancel = false;
         }
      }

      private void UsersListBox_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         if(e.Clicks == 3 && UsersListBox.SelectedItems[0] != null && UsersListBox.Items.Count > 0)
         {
            gameManager.RemoveUserFromUserList(UsersListBox.SelectedItems[0].Text);
         }
      }

      private void removeNameMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
      {
         if (e.ClickedItem.Name == removeNameItem.Name && UsersListBox.SelectedItems[0] != null)
         {
            string removeName = UsersListBox.SelectedItems[0].Text;
            gameManager.RemoveUserFromUserList(removeName);
         }
      }

      private void defaultCostPerSquare_ValueChanged(object sender, EventArgs e)
      {
         int defaultCost = (int)defaultCostPerSquare.Value;
         gameManager.SetDefaultCostPerSquare(defaultCost);
      }

      /// <summary>
      /// Hit whenever either the home or away team's score
      /// changes on the form.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void score_ValueChanged(object sender, EventArgs e)
      {
         if(!gameManager.SetScore((int)homeTeamNumUp.Value, (int)awayTeamNumUp.Value))
         {
            homeTeamNumUp.Value = 0;
            awayTeamNumUp.Value = 0;

            MessageBox.Show("Game hasn't started yet!\nYou can't change score until game has started.",
               "Game Hasn't Started!",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error);
         }
      }

      private void pointLabel_Click(object sender, EventArgs e)
      {
         Button selectedButton = (Button)sender;
         int pointsAdded = 0;
         bool homeSelected = false;

         switch(selectedButton.Name)
         {
            case "threePtAway":
               pointsAdded += 3;
               homeSelected = false;
               break;
            case "threePtHome":
               pointsAdded += 3;
               homeSelected = true;
               break;
            case "sevenPtAway":
               pointsAdded += 7;
               homeSelected = false;
               break;
            case "sevenPtHome":
               pointsAdded += 7;
               homeSelected = true;
               break;
         }

         int newHomeScore = homeSelected == true ?
            gameManager.Team1.Score.ScoreNumber + pointsAdded : gameManager.Team1.Score.ScoreNumber;

         homeTeamNumUp.Value = newHomeScore;

         int newAwayScore = homeSelected != true ?
            gameManager.Team2.Score.ScoreNumber + pointsAdded : gameManager.Team2.Score.ScoreNumber;

         awayTeamNumUp.Value = newAwayScore;
      }

      private void ResetGameButton_Click(object sender, EventArgs e)
      {
         DialogResult closingResult = MessageBox.Show("Are you sure you want to restart the game?", "Restarting Game",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

         if (closingResult == DialogResult.Yes)
         {
            // Reset Score
            homeTeamNumUp.Value = 0;
            awayTeamNumUp.Value = 0;

            gameManager.RestartGame();
         }
      }

      public void ConfigureGameDataFile()
      {
         GameConfiguration gameConfiguration = new GameConfiguration();
         bool readFileSuccess = gameConfiguration.ReadFile();

         if(!readFileSuccess)
         {
            DialogResult dialogResult = MessageBox.Show("Unable to find Game Data File!", "Game Data File Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if(dialogResult == DialogResult.Cancel)
            {
               Environment.Exit(0);
            }
            else
            {
               gameManager.SetGameConfigurationFile(new GameConfiguration());
            }
         }
         else
         {
            gameManager.SetGameConfigurationFile(gameConfiguration);
         }
      }

      /// <summary>
      /// Called after "final" has been selected in quarter
      /// combo box.
      /// </summary>
      /// <param name="includingResetButton"></param>
      public void LockAllControls(bool includingResetButton = false, bool lockControls = true)
      {
         startGameButton.Enabled = !lockControls;
         threePtAway.Enabled = !lockControls;
         threePtHome.Enabled = !lockControls;
         sevenPtAway.Enabled = !lockControls;
         sevenPtHome.Enabled = !lockControls;
         QuarterCB.Enabled = !lockControls;
         defaultCostPerSquare.Enabled = !lockControls;
         awayTeamNumUp.Enabled = !lockControls;
         homeTeamNumUp.Enabled = !lockControls;
      }

      /// <summary>
      /// Handles double click events from the lock
      /// picture box.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="clickedEvent"></param>
      private void LockPictureBox_DoubleClicked(object sender, EventArgs clickedEvent)
      {
         if(gameManager.GameStarted)
         {
            gameManager.SetSquaresLock(!gameManager.SquaresLocked);
         }
      }

      /// <summary>
      /// Increments the quarter by one.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void nextQuarterButton_Click(object sender, EventArgs e)
      {
         if(gameManager.GameStarted)
         {
            gameManager.NextQuarter();
         }
      }
   }
}
