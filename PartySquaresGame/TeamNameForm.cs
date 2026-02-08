using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeamName_NS
{
   public partial class TeamNameForm : Form
   {
      public TeamNameForm()
      {
         InitializeComponent();
         WindowClosed = false;
         TeamName = "";
         m_Label = null;
      }

      public TeamNameForm(string currTeamName)
      {
         InitializeComponent();
         WindowClosed = false;
         m_CurrentTeamName = currTeamName;
         m_Label = null;
      }

      public TeamNameForm(Label label)
      {
         InitializeComponent();
         WindowClosed = false;
         m_Label = label;
         m_CurrentTeamName = label.Text;
      }

      public string TeamName
      {
         get;
         private set;
      }

      public bool WindowClosed
      {
         get;
         private set;
      }

      private void enterButton_Click(object sender, EventArgs e)
      {
         string temp = teamNameTextBox.Text;
         if(temp != m_CurrentTeamName && temp != "")
         {
            TeamName = temp;
         }

         if(m_Label != null)
         {
            m_Label.Text = TeamName;
         }

         this.Hide();
      }

      private void closeButton_Click(object sender, EventArgs e)
      {
         WindowClosed = true;
         this.Hide();
      }

      private string m_CurrentTeamName;
      private Label m_Label;
   }
}
