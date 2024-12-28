using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NesneProje
{
    public partial class AdminPage : Form
    {
        private SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;");

        public AdminPage()
        {
            InitializeComponent();
            LoadUsers();
            LoadPendingAdvices();
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT username FROM info ORDER BY username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            UserControlList.Items.Clear();
                            while (reader.Read())
                            {
                                UserControlList.Items.Add(reader["username"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UserControlList.SelectedItem != null)
            {
                string selectedUser = UserControlList.SelectedItem.ToString();
                ShowUserAbout(selectedUser);
            }
            else
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin!");
            }
        }

        private void ShowUserAbout(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT username, name, ZodiacSign, gender, interests, insta, tel, about, puan FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                richTextBox1.Clear();
                                richTextBox1.AppendText($"Kullanıcı Adı: {reader["username"]}\n");
                                richTextBox1.AppendText($"Ad Soyad: {reader["name"]}\n");
                                richTextBox1.AppendText($"Burç: {reader["ZodiacSign"]}\n");
                                richTextBox1.AppendText($"Cinsiyet: {(Convert.ToInt32(reader["gender"]) == 1 ? "Erkek" : "Kadın")}\n");
                                richTextBox1.AppendText($"İlgi Alanları: {reader["interests"]}\n");
                                richTextBox1.AppendText($"Instagram: {reader["insta"]}\n");
                                richTextBox1.AppendText($"Telefon: {reader["tel"]}\n");
                                richTextBox1.AppendText($"Puanı: {reader["puan"]}\n");
                                richTextBox1.AppendText($"Hakkında: {reader["about"]}\n");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserControlList.SelectedItem != null)
            {
                string selectedUser = UserControlList.SelectedItem.ToString();
                if (MessageBox.Show($"{selectedUser} kullanıcısını silmek istediğinize emin misiniz?", "Kullanıcı Sil",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteUser(selectedUser);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek kullanıcıyı seçin!");
            }
        }

        private void DeleteUser(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Kullanıcı başarıyla silindi.");
                LoadUsers(); // Kullanıcı listesini güncelle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı silinirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadPendingAdvices()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                    id as 'ID',
                                    sender_username as 'Gönderen',
                                    advice_text as 'Tavsiye',
                                    CONVERT(varchar, created_date, 103) as 'Tarih'
                                    FROM relationship_advice 
                                    WHERE is_approved = 0 OR is_approved IS NULL";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.ReadOnly = true;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1.MultiSelect = false;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Bekleyen tavsiye bulunmamaktadır.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tavsiyeler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int adviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                string senderUsername = dataGridView1.SelectedRows[0].Cells["Gönderen"].Value.ToString();
                ApproveAdvice(adviceId, senderUsername);
            }
            else
            {
                MessageBox.Show("Lütfen onaylanacak tavsiyeyi seçin!");
            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int adviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                RejectAdvice(adviceId);
            }
            else
            {
                MessageBox.Show("Lütfen reddedilecek tavsiyeyi seçin!");
            }
        }

        private void ApproveAdvice(int adviceId, string senderUsername)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Tavsiyeyi onayla (1 = onaylandı)
                            string updateQuery = "UPDATE relationship_advice SET is_approved = 1 WHERE id = @id";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id", adviceId);
                                cmd.ExecuteNonQuery();
                            }

                            // Kullanıcıya puan ekle
                            string updatePointsQuery = "UPDATE info SET puan = puan + 200 WHERE username = @username";
                            using (SqlCommand cmd = new SqlCommand(updatePointsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@username", senderUsername);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Tavsiye onaylandı ve kullanıcıya 200 puan eklendi!");

                            // DataGridView'den seçili satırı kaldır
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tavsiye onaylanırken hata oluştu: " + ex.Message);
            }
        }

        private void RejectAdvice(int adviceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(baglanti.ConnectionString))
                {
                    conn.Open();
                    // Tavsiyeyi reddet (2 = reddedildi)
                    string query = "UPDATE relationship_advice SET is_approved = 2 WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", adviceId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // DataGridView'den seçili satırı kaldır
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }

                MessageBox.Show("Tavsiye reddedildi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tavsiye reddedilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
