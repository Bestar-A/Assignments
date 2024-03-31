namespace StudentRegister
{
    partial class MainForm
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
            this.AddBtn = new System.Windows.Forms.Button();
            this.ShowBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(70, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Data Management";
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(180, 175);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Padding = new System.Windows.Forms.Padding(8);
            this.AddBtn.Size = new System.Drawing.Size(140, 40);
            this.AddBtn.TabIndex = 1;
            this.AddBtn.Text = "Add Student";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // ShowBtn
            // 
            this.ShowBtn.Location = new System.Drawing.Point(180, 285);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Padding = new System.Windows.Forms.Padding(8);
            this.ShowBtn.Size = new System.Drawing.Size(140, 40);
            this.ShowBtn.TabIndex = 2;
            this.ShowBtn.Text = "Show Student";
            this.ShowBtn.UseVisualStyleBackColor = true;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(180, 460);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Padding = new System.Windows.Forms.Padding(8);
            this.ExitBtn.Size = new System.Drawing.Size(140, 40);
            this.ExitBtn.TabIndex = 3;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StudentRegister.Properties.Resources.Logo;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ShowBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(500, 600);
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "MainForm";
            this.Text = "Eduvos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button ShowBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}

