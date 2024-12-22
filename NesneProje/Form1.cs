using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NesneProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;");
        private void Form1_Load(object sender, EventArgs e)
        {
            Password.PasswordChar = '*';

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            registerForm.StartPosition = FormStartPosition.Manual;
            registerForm.Location = this.Location;
            registerForm.Size = this.Size;
            
            this.Hide();

            registerForm.ShowDialog();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = registerForm.Location;

            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Text;



            if (ValidateUser(username, password))
            {
                // Giriş başarılıysa
                MessageBox.Show("Giriş başarılı!");

                CurrentUser.current_user = username;

                MainMenu mainpage = new MainMenu();

                mainpage.StartPosition = FormStartPosition.Manual;
                mainpage.Location = this.Location;
                

                this.Hide();

                mainpage.ShowDialog();

                this.StartPosition = FormStartPosition.Manual;
                this.Location = mainpage.Location;

                this.Show();


               
            }
            else
            {
                // Giriş başarısızsa
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
            }
        }


        private bool ValidateUser(string username, string password)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;");
            
            {
                baglanti.Open();

                // 2. SQL sorgusunu hazırla
                string query = "SELECT COUNT(1) FROM info WHERE username = @username AND password = @password";

                using (SqlCommand command = new SqlCommand(query, baglanti))
                {
                    // 3. Parametreleri ekle
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // 4. Sonucu al ve kontrol et
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 1; 
                }
            }
        }

        
    }
}
