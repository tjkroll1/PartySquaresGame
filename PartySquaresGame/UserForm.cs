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

namespace User_NS
{
   public partial class UserForm : Form
   {
      /// <summary>
      /// Constructor with three parameters
      /// </summary>
      /// <param name="assignedLabel"></param>
      /// <param name="userList"></param>
      /// <param name="defaultAmount"></param>
      public UserForm(Square clickedSquare, UserList userList, Money defaultAmount)
      {
         InitializeComponent();
         UserSquare = clickedSquare;
         MoneyAmount = defaultAmount;
         WindowClosed = false;
         UserList = userList;
         // We should lock the money lock by default.
         MoneyCheckBox.Checked = true;
         AdditionalSquares = 0;
         MoneyChanged = false;
         EnableMoneyTextBoxes();
         FillInputBoxes();
      }

      /// <summary>
      /// Sets the remaining squares label
      /// </summary>
      /// <param name="squares"></param>
      public void SetRemainingSquares(int squares)
      {
         labelRemainingSquares.Text = "Open Squares: " + squares.ToString();

         if (squares > 0)
         {
            squaresSpinner.Maximum = squares;
         }
      }

      /// <summary>
      /// Auto populate the text boxes if user is selected.
      /// </summary>
      private void FillInputBoxes()
      {
         if(UserSquare != null)
         {
            if (UserSquare.SquareUser != null)
            {
               textBoxName.Text = UserSquare.SquareUser.Name;
               moneyUpDown.Value = UserSquare.CashAmount != null ?
                  (decimal)UserSquare.CashAmount.Value : 0;
               UpdateSelectedColor(UserSquare.Color);
            }
            else
            {
               textBoxName.Text = "";
               moneyUpDown.Value = 0;
               MoneyChanged = true;
               UpdateSelectedColor(Color.Aquamarine);
            }
         }
      }
     
      /// <summary>
      /// Called when the form loads.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void UserForm_Load(object sender, EventArgs e)
      {
         PopulateListBox();
         moneyUpDown.Value = (decimal)MoneyAmount.Value;
         moneyUpDown.Minimum = (decimal)MoneyAmount.Value;
         SetAutoCompleteSource();
      }

      /// <summary>
      /// Sets the collection of auto complete strings.
      /// </summary>
      private void SetAutoCompleteSource()
      {
         AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
         foreach(string userName in UserList.ListUsers.Keys)
         {
            collection.Add(userName);
         }
         textBoxName.AutoCompleteCustomSource = collection;
      }

      /// <summary>
      /// Populate the listbox control.
      /// </summary>
      protected void PopulateListBox()
      {
         if (UserList != null)
         {
            foreach (KeyValuePair<string, User> keyvalue in UserList.ListUsers)
            {
               usersListBox.Items.Add(keyvalue.Value.ToString());
            }
            if(usersListBox.Items.Count == 0)
            {
               usersListBox.Items.Add("None");
               usersListBox.Enabled = false;
            }
         }
      }

      /// <summary>
      /// Returns true if the name text box is not empty.
      /// </summary>
      /// <returns></returns>
      private bool CheckNameTextBox()
      {
         return textBoxName.Text != "";
      }

      /// <summary>
      /// Performs the action of the enter button.
      /// </summary>
      private void EnterButtonAction()
      {
         if (CheckNameTextBox())
         {
            string name = textBoxName.Text;

            if (UserList != null)
            {
               NewUser = UserList.GetUser(name);
               MoneyAmount = new Money((float)moneyUpDown.Value);

               // Additional squares other than the one selected
               AdditionalSquares = (int)squaresSpinner.Value - 1;

               if (NewUser == null)
               {
                  NewUser = new User(textBoxName.Text);
                  NewUser.IncrementMoney(MoneyAmount);
                  UserList.InsertUser(NewUser);
               }
               else
               {
                  UserList.UpdateUser(NewUser);
               }
            }

            if (NewUser.Color != colorButton.BackColor)
            {
               NewUser.Color = colorButton.BackColor;
            }

            UserSquare.FillSquare(NewUser, MoneyAmount);
            UserSquare.SetLabelColor(NewUser.Color);

            Hide(); // Hide user form.
         }
         else
         {
            MessageBox.Show("No name is in the text box. Please enter or select a name!", "No Name Listed", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      /// <summary>
      /// Activated when enter button is clicked.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void enterButton_Click(object sender, EventArgs e)
      {
         EnterButtonAction();
      }

      /// <summary>
      /// Activated when close button is clicked. 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void closeButton_Click(object sender, EventArgs e)
      {
         WindowClosed = true;
         this.Hide();
      }

      public User NewUser
      {
         get;
         private set;
      }

      public Money MoneyAmount
      {
         get;
         private set;
      }

      public bool MoneyChanged
      {
         get;
         private set;
      }

      public Board_NS.Square UserSquare
      {
         get;
         private set;
      }

      public int AdditionalSquares
      {
         get;
         private set;
      }

      public UserList UserList
      {
         get;
         private set;
      }

      public bool WindowClosed
      {
         get;
         private set;
      }

      private void textBoxMoney_KeyDown(object sender, KeyEventArgs e)
      {
         if(e.KeyCode == Keys.Enter)
         {
            if (textBoxName.Text != "" && moneyUpDown.Value != 0)
            {
               EnterButtonAction();
            }
            else if(textBoxName.Text == "" && moneyUpDown.Value != 0)
            {
               MessageBox.Show("Please enter value into Name field!", "Enter User's Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
               textBoxName.Focus();
            }
            else if (textBoxName.Text != "" && moneyUpDown.Value == 0)
            {
               MessageBox.Show("Please enter value into Money field!", "Enter User's Money Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
               textBoxName.Focus();
            }
            else
            {
               MessageBox.Show("Please enter value into both Name and Money field!", "Enter Value for Both Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
               textBoxName.Focus();
            }
         }
      }

      private void existingUsersCB_SelectedIndexChanged(object sender, EventArgs e)
      {
         textBoxName.ReadOnly = true;
      }

      private void MoneyCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         EnableMoneyTextBoxes();
      }

      /// <summary>
      /// Enables the Money Text Boxes
      /// </summary>
      private void EnableMoneyTextBoxes()
      {
         if (MoneyCheckBox.Checked)
         {
            moneyUpDown.Enabled = false;
            moneyUpDown.ReadOnly = true;
         }
         else
         {
            moneyUpDown.Enabled = true;
            moneyUpDown.ReadOnly = false;
         }
      }

      private void usersListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         ListView listBox = (ListView)sender;
         if (listBox.SelectedItems.Count > 0)
         {
            string userName = listBox.SelectedItems[0].Text;
            int indexOfSpaceBeforeParenthesis = userName.IndexOf('(') - 1;
            userName = userName.Substring(0, indexOfSpaceBeforeParenthesis);
            User selectedUser = UserList.GetUser(userName);
            textBoxName.Text = userName;
            UpdateSelectedColor(selectedUser.Color);
         }
      }

      private void usersListBox_KeyDown(object sender, KeyEventArgs e)
      {
         if(e.KeyCode == Keys.Enter)
         {
            EnterButtonAction();
         }
      }

      private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         if(e.CloseReason == CloseReason.UserClosing)
         {
            WindowClosed = true;
         }
      }

      private void usersListBox_DoubleClick(object sender, EventArgs e)
      {
         ListView listBox = (ListView)sender;
         if(listBox.SelectedItems.Count > 0)
         {
            EnterButtonAction();
         }
      }

      private void colorButton_Click(object sender, EventArgs e)
      {
         userColorDialog.Color = NewUser != null ? NewUser.Color : Color.Aquamarine;
         userColorDialog.AnyColor = false;
         userColorDialog.ShowDialog();

         // Yellow is used for the selected color.
         if(userColorDialog.Color == Color.Yellow)
         {
            userColorDialog.Color = Color.LightYellow;
         }
         colorButton.BackColor = userColorDialog.Color;
      }

      /// <summary>
      /// Updates the color of the button and user color dialog.
      /// </summary>
      /// <param name="color"></param>
      private void UpdateSelectedColor(Color color)
      {
         colorButton.BackColor = color;
         userColorDialog.Color = color;
      }
   }
}
