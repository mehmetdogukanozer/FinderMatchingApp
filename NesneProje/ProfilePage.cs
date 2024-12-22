using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NesneProje
{
    public partial class ProfilePage : Form
    {
        public ProfilePage()
        {
            InitializeComponent();

            EditButton.Visible = false;
            SaveButton.Visible = false;

            label1.Text = UserProfile.controluser;

            string username = UserProfile.controluser; // Giriş yapan kullanıcının adı
            string connectionString = "Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Veritabanından about bilgilerini almak için SQL sorgusu
                    string query = "SELECT about FROM info WHERE username = @username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Eğer kullanıcı bulunursa
                            {
                                // About bilgisini RichTextBox'a yazdır
                                string aboutText = reader["about"] != DBNull.Value ? reader["about"].ToString() : "";
                                richTextBox1.Text = aboutText;
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı bilgileri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (UserProfile.controluser == CurrentUser.current_user)
            {
                EditButton.Visible = true;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            EditButton.Hide();
            SaveButton.Visible = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            EditButton.Show();

            string username = UserProfile.controluser;
            string aboutText = richTextBox1.Text;
            string connectionString = "Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcının about bilgisini güncelleme
                    string query = "UPDATE info SET about = @about WHERE username = @username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@about", aboutText);
                        command.Parameters.AddWithValue("@username", username);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Bilgiler başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Bilgiler kaydedilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            SaveButton.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
            
        }
    }
}