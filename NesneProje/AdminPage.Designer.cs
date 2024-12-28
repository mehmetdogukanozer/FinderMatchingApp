using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace NesneProje
{
    public partial class AdminPage : Form
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
            this.UserControlList = new System.Windows.Forms.ListBox();
            this.UserControlList.Location = new System.Drawing.Point(12, 12);
            this.UserControlList.Name = "UserControlList";
            this.UserControlList.Size = new System.Drawing.Size(200, 400);
            this.UserControlList.TabIndex = 0;

            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1.Location = new System.Drawing.Point(450, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(300, 200);
            this.richTextBox1.TabIndex = 1;

            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.Location = new System.Drawing.Point(450, 250);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(500, 200);
            this.dataGridView1.TabIndex = 2;

            this.button1 = new System.Windows.Forms.Button();
            this.button1.Location = new System.Drawing.Point(12, 420);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 30);
            this.button1.Text = "Kullanıcıyı Sil";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            this.button3 = new System.Windows.Forms.Button();
            this.button3.Location = new System.Drawing.Point(450, 220);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(300, 30);
            this.button3.Text = "Kullanıcının Hakkında Bilgisini Göster";
            this.button3.Click += new System.EventHandler(this.button3_Click);

            this.btnOnayla = new System.Windows.Forms.Button();
            this.btnOnayla.Location = new System.Drawing.Point(450, 460);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(100, 30);
            this.btnOnayla.Text = "Onayla";
            this.btnOnayla.Click += new System.EventHandler(this.btnOnayla_Click);

            this.btnReddet = new System.Windows.Forms.Button();
            this.btnReddet.Location = new System.Drawing.Point(560, 460);
            this.btnReddet.Name = "btnReddet";
            this.btnReddet.Size = new System.Drawing.Size(100, 30);
            this.btnReddet.Text = "Reddet";
            this.btnReddet.Click += new System.EventHandler(this.btnReddet_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);

            this.Controls.Add(this.UserControlList);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnOnayla);
            this.Controls.Add(this.btnReddet);

            this.Name = "AdminPage";
            this.Text = "Admin Sayfası";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();

            // Form arka plan rengi
            this.BackColor = System.Drawing.Color.Black;

            // UserControlList renk ayarları
            this.UserControlList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.UserControlList.ForeColor = System.Drawing.Color.White;

            // RichTextBox renk ayarları
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;

            // DataGridView renk ayarları
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.dataGridView1.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // Buton renk ayarları
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.button1.ForeColor = System.Drawing.Color.White;

            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.button3.ForeColor = System.Drawing.Color.White;

            this.btnOnayla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.btnOnayla.ForeColor = System.Drawing.Color.White;

            this.btnReddet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(140)))), ((int)(((byte)(145)))));
            this.btnReddet.ForeColor = System.Drawing.Color.White;

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox UserControlList;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnOnayla;
        private System.Windows.Forms.Button btnReddet;
    }
}