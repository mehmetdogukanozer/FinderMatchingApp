using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; 
using System.Linq;
using System.Windows.Forms;

namespace NesneProje
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OpenProfilePage()
        {
            ProfilePage profilePage = new ProfilePage
            {
                StartPosition = FormStartPosition.Manual,
                Location = this.Location // MainMenu'nün mevcut konumunu ProfilePage'e aktar
            };

            this.Hide(); // MainMenu'yu gizle
            profilePage.ShowDialog(); // ProfilePage'i modal olarak aç

            // ProfilePage'den dönüldüğünde, MainMenu'yu göster
            this.Location = profilePage.Location; // Profil sayfasından gelen konumla güncelle
            this.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            mainuser.Text = CurrentUser.current_user;
            ScoreControl(); 
        }

        public void ScoreControl()
        {
            
            string connectionString = "Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string loggedInUser = CurrentUser.current_user;
                string query = "SELECT * FROM info WHERE username = @username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", loggedInUser);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string userZodiac = reader["ZodiacSign"].ToString();
                    int userGender = Convert.ToInt32(reader["gender"]);
                    string[] userInterests = reader["interests"].ToString()?.Split(',') ?? new string[0];
                    reader.Close();

                    
                    query = "SELECT * FROM info WHERE gender != @gender";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gender", userGender);

                    reader = command.ExecuteReader();

                    List<(string username, int score)> scores = new List<(string, int)>();

                    while (reader.Read())
                    {
                        string otherUsername = reader["username"].ToString();
                        string otherZodiac = reader["ZodiacSign"].ToString();
                        string[] otherInterests = reader["interests"].ToString()?.Split(',') ?? new string[0];

                      
                        int zodiacScore = CalculateZodiacScore(userZodiac, otherZodiac);

                       
                        int interestScore = CalculateInterestScore(userInterests, otherInterests);

                        
                        int totalScore = zodiacScore + interestScore;

                        scores.Add((otherUsername, totalScore));
                    }
                    reader.Close();

                    
                    var top5Users = scores.OrderByDescending(x => x.score).Take(5).ToList();

                   
                    if (top5Users.Count > 0) top1.Text = top5Users[0].username + " - Puan: " + top5Users[0].score;
                    if (top5Users.Count > 1) top2.Text = top5Users[1].username + " - Puan: " + top5Users[1].score;
                    if (top5Users.Count > 2) top3.Text = top5Users[2].username + " - Puan: " + top5Users[2].score;
                    if (top5Users.Count > 3) top4.Text = top5Users[3].username + " - Puan: " + top5Users[3].score;
                    if (top5Users.Count > 4) top5.Text = top5Users[4].username + " - Puan: " + top5Users[4].score;

                    
                    UserProfile.birinci = top5Users[0].username;
                    UserProfile.ikinci = top5Users[1].username;
                    UserProfile.ucuncu = top5Users[2].username;
                    UserProfile.dorduncu = top5Users[3].username;
                    UserProfile.besinci = top5Users[4].username;
                }
            }
        }


        private int CalculateZodiacScore(string userZodiac, string otherZodiac)
        {
            int[,] uyumPuanlari = {
                { 6, 3, 7, 2, 10, 4, 8, 3, 9, 5, 9, 2 },  // Koç
                { 3, 6, 5, 9, 4, 8, 4, 10, 3, 8, 3, 7 },  // Boğa
                { 7, 5, 6, 3, 8, 2, 10, 4, 9, 2, 9, 3 },  // İkizler
                { 2, 9, 3, 8, 4, 9, 4, 10, 2, 7, 3, 10 }, // Yengeç
                { 10, 4, 8, 4, 6, 3, 10, 5, 8, 4, 9, 2 }, // Aslan
                { 4, 8, 2, 9, 3, 6, 6, 7, 3, 10, 4, 8 },  // Başak
                { 8, 4, 10, 4, 10, 6, 6, 6, 7, 3, 10, 5 },// Terazi
                { 3, 10, 4, 10, 5, 7, 6, 6, 3, 8, 3, 9 }, // Akrep
                { 9, 3, 9, 2, 8, 3, 7, 3, 8, 4, 10, 3 },  // Yay
                { 5, 8, 2, 7, 4, 10, 3, 8, 4, 6, 5, 8 },  // Oğlak
                { 9, 3, 9, 3, 9, 4, 10, 3, 10, 5, 6, 3 }, // Kova
                { 2, 7, 3, 10, 2, 8, 5, 9, 3, 8, 3, 6 }   // Balık
            };

            string[] burclar = { "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık" };

            int userIndex = Array.IndexOf(burclar, userZodiac);
            int otherIndex = Array.IndexOf(burclar, otherZodiac);

            if (userIndex == -1 || otherIndex == -1) return 0;

            return uyumPuanlari[userIndex, otherIndex];
        }

        private int CalculateInterestScore(string[] userInterests, string[] otherInterests)
        {
            int score = 0;

            foreach (string interest in userInterests)
            {
                if (otherInterests.Contains(interest.Trim()))
                {
                    score++;
                }
            }

            return score;
        }

        public void mainuser_Click(object sender, EventArgs e)
        {
            
            UserProfile.controluser = CurrentUser.current_user;
            OpenProfilePage();





        }

        public void top1_Click(object sender, EventArgs e)
        {
            UserProfile.controluser = UserProfile.birinci;

            OpenProfilePage();




        }

        private void top2_Click(object sender, EventArgs e)
        {
            UserProfile.controluser = UserProfile.ikinci;

            OpenProfilePage();
        }

        private void top3_Click(object sender, EventArgs e)
        {
            UserProfile.controluser = UserProfile.ucuncu;

            OpenProfilePage();
        }

        private void top4_Click(object sender, EventArgs e)
        {
            UserProfile.controluser = UserProfile.dorduncu;

            OpenProfilePage();
        }

        private void top5_Click(object sender, EventArgs e)
        {
            UserProfile.controluser = UserProfile.besinci;

            OpenProfilePage(); ;
        }
    }
    }


