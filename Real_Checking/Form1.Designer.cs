namespace Real_Checking
{
	partial class Form1
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
			if(disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.inputFileButton = new System.Windows.Forms.Button();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.outputFileButton = new System.Windows.Forms.Button();
			this.inputFileTextBox = new System.Windows.Forms.TextBox();
			this.outputFileTextBox = new System.Windows.Forms.TextBox();
			this.nameLogLabel = new System.Windows.Forms.Label();
			this.inputFileLabel = new System.Windows.Forms.Label();
			this.outputFileLabel = new System.Windows.Forms.Label();
			this.runButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.answersCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.openInputFileButton = new System.Windows.Forms.Button();
			this.statusConsole = new Real_Checking.SyncTextBox();
			this.loginConsole = new Real_Checking.SyncTextBox();
			this.SuspendLayout();
			// 
			// inputFileButton
			// 
			resources.ApplyResources(this.inputFileButton, "inputFileButton");
			this.inputFileButton.Name = "inputFileButton";
			this.inputFileButton.UseVisualStyleBackColor = true;
			this.inputFileButton.Click += new System.EventHandler(this.input_button_Click);
			// 
			// openFile
			// 
			resources.ApplyResources(this.openFile, "openFile");
			// 
			// outputFileButton
			// 
			resources.ApplyResources(this.outputFileButton, "outputFileButton");
			this.outputFileButton.Name = "outputFileButton";
			this.outputFileButton.UseVisualStyleBackColor = true;
			this.outputFileButton.Click += new System.EventHandler(this.output_button_Click);
			// 
			// inputFileTextBox
			// 
			resources.ApplyResources(this.inputFileTextBox, "inputFileTextBox");
			this.inputFileTextBox.Name = "inputFileTextBox";
			this.inputFileTextBox.ReadOnly = true;
			this.inputFileTextBox.TextChanged += new System.EventHandler(this.input_file_text_TextChanged);
			// 
			// outputFileTextBox
			// 
			resources.ApplyResources(this.outputFileTextBox, "outputFileTextBox");
			this.outputFileTextBox.Name = "outputFileTextBox";
			this.outputFileTextBox.ReadOnly = true;
			this.outputFileTextBox.TextChanged += new System.EventHandler(this.output_file_text_TextChanged);
			// 
			// nameLogLabel
			// 
			resources.ApplyResources(this.nameLogLabel, "nameLogLabel");
			this.nameLogLabel.Cursor = System.Windows.Forms.Cursors.Default;
			this.nameLogLabel.Name = "nameLogLabel";
			// 
			// inputFileLabel
			// 
			resources.ApplyResources(this.inputFileLabel, "inputFileLabel");
			this.inputFileLabel.Name = "inputFileLabel";
			// 
			// outputFileLabel
			// 
			resources.ApplyResources(this.outputFileLabel, "outputFileLabel");
			this.outputFileLabel.Name = "outputFileLabel";
			// 
			// runButton
			// 
			this.runButton.BackColor = System.Drawing.Color.Green;
			resources.ApplyResources(this.runButton, "runButton");
			this.runButton.Name = "runButton";
			this.runButton.UseVisualStyleBackColor = false;
			this.runButton.Click += new System.EventHandler(this.run_button_Click);
			// 
			// stopButton
			// 
			this.stopButton.BackColor = System.Drawing.Color.Orange;
			resources.ApplyResources(this.stopButton, "stopButton");
			this.stopButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.stopButton.Name = "stopButton";
			this.stopButton.UseVisualStyleBackColor = false;
			this.stopButton.Click += new System.EventHandler(this.stop_button_Click);
			// 
			// answersCheckBox
			// 
			resources.ApplyResources(this.answersCheckBox, "answersCheckBox");
			this.answersCheckBox.Checked = true;
			this.answersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.answersCheckBox.Name = "answersCheckBox";
			this.answersCheckBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.BackColor = System.Drawing.Color.Red;
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.BackColor = System.Drawing.Color.Red;
			this.label2.Name = "label2";
			// 
			// openInputFileButton
			// 
			this.openInputFileButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
			resources.ApplyResources(this.openInputFileButton, "openInputFileButton");
			this.openInputFileButton.Name = "openInputFileButton";
			this.openInputFileButton.UseVisualStyleBackColor = false;
			this.openInputFileButton.Click += new System.EventHandler(this.openInputFileButton_Click);
			// 
			// statusConsole
			// 
			this.statusConsole.Buddy = this.loginConsole;
			resources.ApplyResources(this.statusConsole, "statusConsole");
			this.statusConsole.Name = "statusConsole";
			this.statusConsole.ReadOnly = true;
			this.statusConsole.TextChanged += new System.EventHandler(this.status_console_TextChanged);
			// 
			// loginConsole
			// 
			this.loginConsole.Buddy = this.statusConsole;
			resources.ApplyResources(this.loginConsole, "loginConsole");
			this.loginConsole.Name = "loginConsole";
			this.loginConsole.ReadOnly = true;
			this.loginConsole.TextChanged += new System.EventHandler(this.out_console_TextChanged);
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.openInputFileButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.answersCheckBox);
			this.Controls.Add(this.statusConsole);
			this.Controls.Add(this.loginConsole);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.runButton);
			this.Controls.Add(this.outputFileLabel);
			this.Controls.Add(this.inputFileLabel);
			this.Controls.Add(this.nameLogLabel);
			this.Controls.Add(this.outputFileTextBox);
			this.Controls.Add(this.inputFileTextBox);
			this.Controls.Add(this.outputFileButton);
			this.Controls.Add(this.inputFileButton);
			this.Name = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button inputFileButton;
		private System.Windows.Forms.Button outputFileButton;
		private System.Windows.Forms.TextBox inputFileTextBox;
		private System.Windows.Forms.TextBox outputFileTextBox;
		private System.Windows.Forms.Label nameLogLabel;
		private System.Windows.Forms.Label inputFileLabel;
		private System.Windows.Forms.Label outputFileLabel;
		private System.Windows.Forms.Button runButton;
		private System.Windows.Forms.Button stopButton;
		private SyncTextBox loginConsole;
		private SyncTextBox statusConsole;
		private System.Windows.Forms.CheckBox answersCheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button openInputFileButton;
		private System.Windows.Forms.OpenFileDialog openFile;
	}
}

