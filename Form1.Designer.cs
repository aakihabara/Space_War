namespace Game
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.restartBttn = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Enabled = true;
            this.GameLoop.Interval = 10;
            this.GameLoop.Tick += new System.EventHandler(this.GameLoop_Tick);
            // 
            // restartBttn
            // 
            this.restartBttn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("restartBttn.BackgroundImage")));
            this.restartBttn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.restartBttn.Cursor = System.Windows.Forms.Cursors.Default;
            this.restartBttn.FlatAppearance.BorderSize = 0;
            this.restartBttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartBttn.Font = new System.Drawing.Font("Forte", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartBttn.ForeColor = System.Drawing.Color.Snow;
            this.restartBttn.Location = new System.Drawing.Point(416, 534);
            this.restartBttn.Name = "restartBttn";
            this.restartBttn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.restartBttn.Size = new System.Drawing.Size(195, 84);
            this.restartBttn.TabIndex = 0;
            this.restartBttn.Text = "RESTART";
            this.restartBttn.UseVisualStyleBackColor = true;
            this.restartBttn.Click += new System.EventHandler(this.restartBttn_Click);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Forte", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.Snow;
            this.lblScore.Location = new System.Drawing.Point(12, 715);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(138, 41);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "score: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(10)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.restartBttn);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.Button restartBttn;
        private System.Windows.Forms.Label lblScore;
    }
}

