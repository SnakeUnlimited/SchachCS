namespace Schach
{
	partial class FormChess
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
			this.label1 = new System.Windows.Forms.Label();
			this.listMove = new System.Windows.Forms.ListBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.infoCheck = new System.Windows.Forms.Panel();
			this.infoCurrentPlayer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(601, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Spielzüge";
			// 
			// listMove
			// 
			this.listMove.FormattingEnabled = true;
			this.listMove.ItemHeight = 15;
			this.listMove.Location = new System.Drawing.Point(668, 26);
			this.listMove.Name = "listMove";
			this.listMove.Size = new System.Drawing.Size(120, 214);
			this.listMove.TabIndex = 3;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(620, 415);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 4;
			this.btnStart.Text = "Start!";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(514, 397);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "Time per Player:";
			// 
			// txtTime
			// 
			this.txtTime.Location = new System.Drawing.Point(514, 416);
			this.txtTime.Name = "txtTime";
			this.txtTime.Size = new System.Drawing.Size(100, 23);
			this.txtTime.TabIndex = 7;
			// 
			// infoCheck
			// 
			this.infoCheck.Location = new System.Drawing.Point(668, 246);
			this.infoCheck.Name = "infoCheck";
			this.infoCheck.Size = new System.Drawing.Size(28, 24);
			this.infoCheck.TabIndex = 8;
			// 
			// infoCurrentPlayer
			// 
			this.infoCurrentPlayer.Location = new System.Drawing.Point(702, 246);
			this.infoCurrentPlayer.Name = "infoCurrentPlayer";
			this.infoCurrentPlayer.Size = new System.Drawing.Size(28, 24);
			this.infoCurrentPlayer.TabIndex = 9;
			// 
			// FormChess
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.infoCurrentPlayer);
			this.Controls.Add(this.infoCheck);
			this.Controls.Add(this.txtTime);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.listMove);
			this.Controls.Add(this.label1);
			this.Name = "FormChess";
			this.Text = "FormChess";
			this.Load += new System.EventHandler(this.FormChess_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Label label1;
		private ListBox listMove;
		private Button btnStart;
		private Label label2;
		private TextBox txtTime;
		private Panel infoCheck;
		private Panel infoCurrentPlayer;
	}
}