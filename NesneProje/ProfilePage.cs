using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace NesneProje
{
    public partial class ProfilePage : Form
    {
        private bool isOwnProfile;
        private int userPoints;
        private bool userInfoPurchased = false;
        private bool contactInfoPurchased = false;
        private readonly string connectionString = "Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;";

        public ProfilePage()
        {
            InitializeComponent();
            isOwnProfile = (UserProfile.controluser == CurrentUser.current_user);
            LoadUserPoints();
            SetupProfileView();
        }

        private void LoadUserPoints()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT puan FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", CurrentUser.current_user);
                        var result = cmd.ExecuteScalar();
                        userPoints = result != null ? Convert.ToInt32(result) : 0;
                        UpdatePointsLabel();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Puan bilgisi yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void UpdatePointsLabel()
        {
            label3.Text = $"Mevcut Puanınız: {userPoints}";
        }

        private void SetupProfileView()
        {
            label1.Text = UserProfile.controluser;

            if (isOwnProfile)
            {
                // Kendi profilimiz
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                richTextBox1.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                pictureBox1.Visible = true;

                LoadProfilePicture();
                ShowUserInfo();
                ShowContactInfo();
            }
            else
            {
                // Başkasının profili
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                richTextBox1.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                pictureBox1.Visible = false;
            }
        }

        private void CheckAndLoadProfilePicture()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT pictures FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserProfile.controluser);
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            byte[] imageData = (byte[])result;
                            using (var ms = new System.IO.MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null;
                            MessageBox.Show("Bu kullanıcının profil fotoğrafı bulunmamaktadır.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Profil fotoğrafı kontrolü yapılırken hata oluştu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!userInfoPurchased)
            {
                if (userPoints >= 100)
                {
                    DialogResult result = MessageBox.Show(
                        "Kullanıcı bilgilerini görmek için 100 puan harcanacak. Onaylıyor musunuz?",
                        "Puan Harcama Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (DeductPoints(100))
                        {
                            userInfoPurchased = true;
                            ShowUserInfo();
                            LoadUserPoints();
                            button1.Visible = false; // Bilgiler gösterildikten sonra butonu gizle
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Yeterli puanınız yok! Gerekli puan: 100");
                }
            }
            else
            {
                ShowUserInfo();
                button1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!contactInfoPurchased)
            {
                if (userPoints >= 300)
                {
                    DialogResult result = MessageBox.Show(
                        "İletişim bilgilerini görmek için 300 puan harcanacak. Onaylıyor musunuz?",
                        "Puan Harcama Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (DeductPoints(300))
                        {
                            contactInfoPurchased = true;
                            ShowContactInfo();
                            LoadUserPoints();
                            button2.Visible = false; // Bilgiler gösterildikten sonra butonu gizle
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Yeterli puanınız yok! Gerekli puan: 300");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isOwnProfile && !string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                SendAdvice();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isOwnProfile)
            {
                UpdateProfilePicture();
            }
        }

        private bool DeductPoints(int points)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE info SET puan = puan - @points WHERE username = @username AND puan >= @points";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@points", points);
                        cmd.Parameters.AddWithValue("@username", CurrentUser.current_user);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{points} puan harcandı. Bilgiler görüntüleniyor.");
                            LoadUserPoints();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Puan düşme işlemi başarısız!");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                return false;
            }
        }

        private void ShowUserInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT name, ZodiacSign, interests, about FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserProfile.controluser);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                richTextBox2.Text = $"İsim: {reader["name"]}\n" +
                                                  $"Burç: {reader["ZodiacSign"]}\n" +
                                                  $"İlgi Alanları: {reader["interests"]}\n" +
                                                  $"Hakkında: {reader["about"]}";
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

        private void ShowContactInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT insta, tel FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserProfile.controluser);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string instagram = reader["insta"].ToString();
                                string telefon = reader["tel"].ToString();

                                richTextBox3.Clear();
                                richTextBox3.AppendText($"Instagram: {instagram}\n");
                                richTextBox3.AppendText($"Telefon: {telefon}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İletişim bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void SendAdvice()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO relationship_advice 
                                   (sender_username, advice_text, created_date, is_approved) 
                                   VALUES (@sender, @advice, GETDATE(), 0)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sender", CurrentUser.current_user);
                        cmd.Parameters.AddWithValue("@advice", richTextBox1.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tavsiyeniz başarıyla gönderildi ve admin onayına sunuldu.");
                        richTextBox1.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tavsiye gönderilirken hata oluştu: " + ex.Message);
            }
        }

        private void UpdateProfilePicture()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Profil Fotoğrafı Seç";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Resmi yükle ve boyutunu kontrol et
                        using (var img = Image.FromFile(openFileDialog.FileName))
                        {
                            // Resmi byte dizisine çevir
                            using (var ms = new System.IO.MemoryStream())
                            {
                                img.Save(ms, img.RawFormat);
                                byte[] imageData = ms.ToArray();

                                // Veritabanına kaydet
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    conn.Open();
                                    string query = "UPDATE info SET pictures = @imageData WHERE username = @username";
                                    using (SqlCommand cmd = new SqlCommand(query, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@imageData", imageData);
                                        cmd.Parameters.AddWithValue("@username", CurrentUser.current_user);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // PictureBox'a göster
                                pictureBox1.Image = Image.FromStream(new System.IO.MemoryStream(imageData));
                                MessageBox.Show("Profil fotoğrafı başarıyla güncellendi!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Profil fotoğrafı güncellenirken hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void LoadProfilePicture()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT pictures FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserProfile.controluser);
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            byte[] imageData = (byte[])result;
                            using (var ms = new System.IO.MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Resmi düzgün göstermek için
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Profil foto��rafı yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT pictures FROM info WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserProfile.controluser);
                        var result = cmd.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            // Fotoğraf varsa puan işlemlerini yap
                            if (userPoints >= 200)
                            {
                                DialogResult dialogResult = MessageBox.Show(
                                    "Profil fotoğrafını görmek için 200 puan harcanacak. Onaylıyor musunuz?",
                                    "Puan Harcama Onayı",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (DeductPoints(200))
                                    {
                                        byte[] imageData = (byte[])result;
                                        using (var ms = new System.IO.MemoryStream(imageData))
                                        {
                                            pictureBox1.Image = Image.FromStream(ms);
                                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                        }
                                        pictureBox1.Visible = true;
                                        button3.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Yeterli puanınız yok! Gerekli puan: 200");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu kullanıcının profil fotoğrafı bulunmamaktadır.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Profil fotoğrafı kontrolü yapılırken hata oluştu: " + ex.Message);
            }
        }


    }
}
