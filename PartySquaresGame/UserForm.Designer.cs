namespace User_NS
{
   partial class UserForm
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
         this.labelUserName = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.groupBox = new System.Windows.Forms.GroupBox();
         this.colorButton = new System.Windows.Forms.Button();
         this.colorLabel = new System.Windows.Forms.Label();
         this.squaresSpinner = new System.Windows.Forms.NumericUpDown();
         this.squareNumLabel = new System.Windows.Forms.Label();
         this.moneyUpDown = new System.Windows.Forms.NumericUpDown();
         this.usersListBox = new System.Windows.Forms.ListView();
         this.MoneyCheckBox = new System.Windows.Forms.CheckBox();
         this.label2 = new System.Windows.Forms.Label();
         this.closeButton = new System.Windows.Forms.Button();
         this.enterButton = new System.Windows.Forms.Button();
         this.labelRemainingSquares = new System.Windows.Forms.Label();
         this.userColorDialog = new System.Windows.Forms.ColorDialog();
         this.groupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.squaresSpinner)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.moneyUpDown)).BeginInit();
         this.SuspendLayout();
         // 
         // labelUserName
         // 
         this.labelUserName.AutoSize = true;
         this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelUserName.Location = new System.Drawing.Point(12, 72);
         this.labelUserName.Name = "labelUserName";
         this.labelUserName.Size = new System.Drawing.Size(74, 25);
         this.labelUserName.TabIndex = 0;
         this.labelUserName.Text = "Name:";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(12, 125);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(162, 25);
         this.label1.TabIndex = 1;
         this.label1.Text = "Money Amount:";
         // 
         // textBoxName
         // 
         this.textBoxName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
         this.textBoxName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
         this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxName.Location = new System.Drawing.Point(197, 66);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(123, 31);
         this.textBoxName.TabIndex = 2;
         this.textBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMoney_KeyDown);
         // 
         // groupBox
         // 
         this.groupBox.Controls.Add(this.colorButton);
         this.groupBox.Controls.Add(this.colorLabel);
         this.groupBox.Controls.Add(this.squaresSpinner);
         this.groupBox.Controls.Add(this.squareNumLabel);
         this.groupBox.Controls.Add(this.moneyUpDown);
         this.groupBox.Controls.Add(this.usersListBox);
         this.groupBox.Controls.Add(this.MoneyCheckBox);
         this.groupBox.Controls.Add(this.label2);
         this.groupBox.Controls.Add(this.closeButton);
         this.groupBox.Controls.Add(this.enterButton);
         this.groupBox.Controls.Add(this.textBoxName);
         this.groupBox.Controls.Add(this.labelUserName);
         this.groupBox.Controls.Add(this.label1);
         this.groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.groupBox.Location = new System.Drawing.Point(12, 40);
         this.groupBox.Name = "groupBox";
         this.groupBox.Size = new System.Drawing.Size(572, 383);
         this.groupBox.TabIndex = 3;
         this.groupBox.TabStop = false;
         this.groupBox.Text = "User Information";
         // 
         // colorButton
         // 
         this.colorButton.Location = new System.Drawing.Point(193, 235);
         this.colorButton.Name = "colorButton";
         this.colorButton.Size = new System.Drawing.Size(100, 30);
         this.colorButton.TabIndex = 14;
         this.colorButton.UseVisualStyleBackColor = true;
         this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
         // 
         // colorLabel
         // 
         this.colorLabel.AutoSize = true;
         this.colorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.colorLabel.Location = new System.Drawing.Point(12, 235);
         this.colorLabel.Name = "colorLabel";
         this.colorLabel.Size = new System.Drawing.Size(120, 25);
         this.colorLabel.TabIndex = 13;
         this.colorLabel.Text = "User Color:";
         // 
         // squaresSpinner
         // 
         this.squaresSpinner.Location = new System.Drawing.Point(197, 182);
         this.squaresSpinner.Name = "squaresSpinner";
         this.squaresSpinner.Size = new System.Drawing.Size(77, 31);
         this.squaresSpinner.TabIndex = 12;
         this.squaresSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.squaresSpinner.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         // 
         // squareNumLabel
         // 
         this.squareNumLabel.AutoSize = true;
         this.squareNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.squareNumLabel.Location = new System.Drawing.Point(12, 184);
         this.squareNumLabel.Name = "squareNumLabel";
         this.squareNumLabel.Size = new System.Drawing.Size(179, 25);
         this.squareNumLabel.TabIndex = 11;
         this.squareNumLabel.Text = "Number Squares:";
         // 
         // moneyUpDown
         // 
         this.moneyUpDown.DecimalPlaces = 2;
         this.moneyUpDown.Location = new System.Drawing.Point(197, 123);
         this.moneyUpDown.Name = "moneyUpDown";
         this.moneyUpDown.Size = new System.Drawing.Size(123, 31);
         this.moneyUpDown.TabIndex = 10;
         this.moneyUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         // 
         // usersListBox
         // 
         this.usersListBox.AllowColumnReorder = true;
         this.usersListBox.HideSelection = false;
         this.usersListBox.LabelWrap = false;
         this.usersListBox.Location = new System.Drawing.Point(391, 66);
         this.usersListBox.Name = "usersListBox";
         this.usersListBox.ShowGroups = false;
         this.usersListBox.Size = new System.Drawing.Size(145, 154);
         this.usersListBox.TabIndex = 9;
         this.usersListBox.UseCompatibleStateImageBehavior = false;
         this.usersListBox.View = System.Windows.Forms.View.List;
         this.usersListBox.SelectedIndexChanged += new System.EventHandler(this.usersListBox_SelectedIndexChanged);
         this.usersListBox.DoubleClick += new System.EventHandler(this.usersListBox_DoubleClick);
         this.usersListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usersListBox_KeyDown);
         // 
         // MoneyCheckBox
         // 
         this.MoneyCheckBox.AutoSize = true;
         this.MoneyCheckBox.Location = new System.Drawing.Point(106, 279);
         this.MoneyCheckBox.Name = "MoneyCheckBox";
         this.MoneyCheckBox.Size = new System.Drawing.Size(172, 29);
         this.MoneyCheckBox.TabIndex = 8;
         this.MoneyCheckBox.Text = "Money Locked";
         this.MoneyCheckBox.UseVisualStyleBackColor = true;
         this.MoneyCheckBox.CheckedChanged += new System.EventHandler(this.MoneyCheckBox_CheckedChanged);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label2.Location = new System.Drawing.Point(386, 38);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(156, 25);
         this.label2.TabIndex = 7;
         this.label2.Text = "Existing Users:";
         // 
         // closeButton
         // 
         this.closeButton.Location = new System.Drawing.Point(324, 325);
         this.closeButton.Name = "closeButton";
         this.closeButton.Size = new System.Drawing.Size(126, 36);
         this.closeButton.TabIndex = 5;
         this.closeButton.Text = "Close";
         this.closeButton.UseVisualStyleBackColor = true;
         this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
         // 
         // enterButton
         // 
         this.enterButton.Location = new System.Drawing.Point(141, 325);
         this.enterButton.Name = "enterButton";
         this.enterButton.Size = new System.Drawing.Size(126, 36);
         this.enterButton.TabIndex = 4;
         this.enterButton.Text = "Enter";
         this.enterButton.UseVisualStyleBackColor = true;
         this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
         // 
         // labelRemainingSquares
         // 
         this.labelRemainingSquares.AutoSize = true;
         this.labelRemainingSquares.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelRemainingSquares.Location = new System.Drawing.Point(331, 12);
         this.labelRemainingSquares.Name = "labelRemainingSquares";
         this.labelRemainingSquares.Size = new System.Drawing.Size(0, 25);
         this.labelRemainingSquares.TabIndex = 13;
         // 
         // UserForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(596, 446);
         this.Controls.Add(this.labelRemainingSquares);
         this.Controls.Add(this.groupBox);
         this.Name = "UserForm";
         this.Text = "UserForm";
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
         this.Load += new System.EventHandler(this.UserForm_Load);
         this.groupBox.ResumeLayout(false);
         this.groupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.squaresSpinner)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.moneyUpDown)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label labelUserName;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.GroupBox groupBox;
      private System.Windows.Forms.Button closeButton;
      private System.Windows.Forms.Button enterButton;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.CheckBox MoneyCheckBox;
      private System.Windows.Forms.ListView usersListBox;
      private System.Windows.Forms.NumericUpDown moneyUpDown;
        private System.Windows.Forms.NumericUpDown squaresSpinner;
        private System.Windows.Forms.Label squareNumLabel;
        private System.Windows.Forms.Label labelRemainingSquares;
      private System.Windows.Forms.Label colorLabel;
      private System.Windows.Forms.ColorDialog userColorDialog;
      private System.Windows.Forms.Button colorButton;
   }
}