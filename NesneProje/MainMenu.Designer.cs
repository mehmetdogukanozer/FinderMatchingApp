namespace NesneProje
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.top1 = new System.Windows.Forms.Label();
            this.top2 = new System.Windows.Forms.Label();
            this.top3 = new System.Windows.Forms.Label();
            this.top4 = new System.Windows.Forms.Label();
            this.top5 = new System.Windows.Forms.Label();
            this.mainuser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // top1
            // 
            this.top1.AutoSize = true;
            this.top1.BackColor = System.Drawing.Color.Brown;
            this.top1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.top1.Location = new System.Drawing.Point(130, 173);
            this.top1.Name = "top1";
            this.top1.Size = new System.Drawing.Size(373, 54);
            this.top1.TabIndex = 1;
            this.top1.Text = "deneme deneme 10";
            this.top1.Click += new System.EventHandler(this.top1_Click);
            // 
            // top2
            // 
            this.top2.AutoSize = true;
            this.top2.BackColor = System.Drawing.Color.White;
            this.top2.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top2.Location = new System.Drawing.Point(130, 286);
            this.top2.Name = "top2";
            this.top2.Size = new System.Drawing.Size(373, 54);
            this.top2.TabIndex = 1;
            this.top2.Text = "deneme deneme 10";
            this.top2.Click += new System.EventHandler(this.top2_Click);
            // 
            // top3
            // 
            this.top3.AutoSize = true;
            this.top3.BackColor = System.Drawing.Color.White;
            this.top3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.top3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.top3.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top3.ForeColor = System.Drawing.Color.Black;
            this.top3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.top3.Location = new System.Drawing.Point(128, 391);
            this.top3.Name = "top3";
            this.top3.Size = new System.Drawing.Size(375, 56);
            this.top3.TabIndex = 1;
            this.top3.Text = "deneme deneme 10";
            this.top3.Click += new System.EventHandler(this.top3_Click);
            // 
            // top4
            // 
            this.top4.AutoSize = true;
            this.top4.BackColor = System.Drawing.Color.White;
            this.top4.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top4.Location = new System.Drawing.Point(130, 498);
            this.top4.Name = "top4";
            this.top4.Size = new System.Drawing.Size(373, 54);
            this.top4.TabIndex = 1;
            this.top4.Text = "deneme deneme 10";
            this.top4.Click += new System.EventHandler(this.top4_Click);
            // 
            // top5
            // 
            this.top5.AutoSize = true;
            this.top5.BackColor = System.Drawing.Color.White;
            this.top5.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top5.Location = new System.Drawing.Point(130, 606);
            this.top5.Name = "top5";
            this.top5.Size = new System.Drawing.Size(373, 54);
            this.top5.TabIndex = 1;
            this.top5.Text = "deneme deneme 10";
            this.top5.Click += new System.EventHandler(this.top5_Click);
            // 
            // mainuser
            // 
            this.mainuser.AutoSize = true;
            this.mainuser.BackColor = System.Drawing.Color.Transparent;
            this.mainuser.Font = new System.Drawing.Font("Yu Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mainuser.Location = new System.Drawing.Point(412, 40);
            this.mainuser.Name = "mainuser";
            this.mainuser.Size = new System.Drawing.Size(216, 52);
            this.mainuser.TabIndex = 2;
            this.mainuser.Text = "username";
            this.mainuser.Click += new System.EventHandler(this.mainuser_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.mainuser);
            this.panel1.Location = new System.Drawing.Point(-6, -9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 122);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NesneProje.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 118);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(160)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(660, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.top5);
            this.Controls.Add(this.top4);
            this.Controls.Add(this.top3);
            this.Controls.Add(this.top2);
            this.Controls.Add(this.top1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label top1;
        private System.Windows.Forms.Label top2;
        private System.Windows.Forms.Label top3;
        private System.Windows.Forms.Label top4;
        private System.Windows.Forms.Label top5;
        private System.Windows.Forms.Label mainuser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}