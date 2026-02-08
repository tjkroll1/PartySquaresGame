namespace TeamName_NS
{
   partial class TeamNameForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.closeButton = new System.Windows.Forms.Button();
         this.enterButton = new System.Windows.Forms.Button();
         this.teamNameTextBox = new System.Windows.Forms.TextBox();
         this.labelUserName = new System.Windows.Forms.Label();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.closeButton);
         this.groupBox1.Controls.Add(this.enterButton);
         this.groupBox1.Controls.Add(this.teamNameTextBox);
         this.groupBox1.Controls.Add(this.labelUserName);
         this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.groupBox1.Location = new System.Drawing.Point(23, 22);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(441, 274);
         this.groupBox1.TabIndex = 4;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Team Name";
         // 
         // closeButton
         // 
         this.closeButton.Location = new System.Drawing.Point(266, 196);
         this.closeButton.Name = "closeButton";
         this.closeButton.Size = new System.Drawing.Size(126, 36);
         this.closeButton.TabIndex = 5;
         this.closeButton.Text = "Close";
         this.closeButton.UseVisualStyleBackColor = true;
         this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
         // 
         // enterButton
         // 
         this.enterButton.Location = new System.Drawing.Point(49, 196);
         this.enterButton.Name = "enterButton";
         this.enterButton.Size = new System.Drawing.Size(126, 36);
         this.enterButton.TabIndex = 4;
         this.enterButton.Text = "Enter";
         this.enterButton.UseVisualStyleBackColor = true;
         this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
         // 
         // teamNameTextBox
         // 
         this.teamNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.teamNameTextBox.Location = new System.Drawing.Point(253, 66);
         this.teamNameTextBox.Name = "teamNameTextBox";
         this.teamNameTextBox.Size = new System.Drawing.Size(160, 31);
         this.teamNameTextBox.TabIndex = 2;
         // 
         // labelUserName
         // 
         this.labelUserName.AutoSize = true;
         this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelUserName.Location = new System.Drawing.Point(23, 72);
         this.labelUserName.Name = "labelUserName";
         this.labelUserName.Size = new System.Drawing.Size(191, 25);
         this.labelUserName.TabIndex = 0;
         this.labelUserName.Text = "Enter Team Name:";
         // 
         // TeamNameForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(507, 308);
         this.Controls.Add(this.groupBox1);
         this.Name = "TeamNameForm";
         this.Text = "TeamNameForm";
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button closeButton;
      private System.Windows.Forms.Button enterButton;
      private System.Windows.Forms.TextBox teamNameTextBox;
      private System.Windows.Forms.Label labelUserName;
   }
}