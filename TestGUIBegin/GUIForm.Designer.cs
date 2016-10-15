namespace TestGUI
{
    partial class GUIForm
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
            this.SearchResult = new System.Windows.Forms.Label();
            this.CreateIndexAtPathButton = new System.Windows.Forms.Button();
			this.IndexPathTextBox = new System.Windows.Forms.TextBox();
			this.ViewNextPageButton = new System.Windows.Forms.Button();
			this.ViewLastPageButton = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // NextLabel
            // 
            this.SearchResult.AutoSize = true;
            this.SearchResult.Location = new System.Drawing.Point(65, 24);
            this.SearchResult.Name = "NextLabel";
            this.SearchResult.Size = new System.Drawing.Size(39, 13);
            this.SearchResult.TabIndex = 0;
            this.SearchResult.Text = "Label1";
            // 
            // NextButton
            // 
			this.CreateIndexAtPathButton.Location = new System.Drawing.Point(314, 19);
            this.CreateIndexAtPathButton.Name = "IndexAtPathBotton";
            this.CreateIndexAtPathButton.Size = new System.Drawing.Size(75, 23);
            this.CreateIndexAtPathButton.TabIndex = 1;
            this.CreateIndexAtPathButton.Text = "Create Index";
            this.CreateIndexAtPathButton.UseVisualStyleBackColor = true;
			this.CreateIndexAtPathButton.Click += new System.EventHandler(this.CreateIndexAtPathBottonHandler);
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 261);
            this.Controls.Add(this.CreateIndexAtPathButton);
            this.Controls.Add(this.SearchResult);
            this.Name = "GUIForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

			ViewLastPageButton.Enabled = false;
			ViewNextPageButton.Enabled = false;
        }

        #endregion

		private System.Windows.Forms.TextBox CollectionPathTextBox;
       	private System.Windows.Forms.TextBox IndexPathTextBox;
        private System.Windows.Forms.Button CreateIndexAtPathButton;

		private System.Windows.Forms.TextBox QueryTextBox;
		private System.Windows.Forms.Label FinalQueryLabel;
		private System.Windows.Forms.Label SearchResult;

		private System.Windows.Forms.Button ViewNextPageButton;
		private System.Windows.Forms.Button ViewLastPageButton;



		public void EnablePageChange()
		{
			ViewLastPageButton.Enabled = true;
			ViewNextPageButton.Enabled = true;
		}
    }


}

