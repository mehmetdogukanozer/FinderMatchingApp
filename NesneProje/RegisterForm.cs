﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NesneProje
{
    public partial class RegisterForm : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0258IVR\\SQLEXPRESS;Initial Catalog=Profiles;Integrated Security=True;");
        public RegisterForm()
        {
            InitializeComponent();
            MessageBox.Show("KAYIT YAPTIRDIĞINIZ ANDA KULLANICI SÖZLEŞMESİNİ KABUL ETMİŞ SAYILACAKSINIZ");
            MessageBox.Show("KULLANICI SÖZLEŞMESİ= HER SEYİ KABUL EDİYORUM");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<string> interestlist = new List<string>();
            string username = RegisterUsername.Text;
            string password = RegisterPassword.Text;
            string name = RegisterName.Text;
            string zodiacsign = burcbox.SelectedItem.ToString();
            string insta = textBox1.Text;
            string telno = textBox2.Text;
            foreach (var item in InterestsCheckBox.CheckedItems)
            {
                interestlist.Add(item.ToString());
            }

            int gender = 0;
            if (MaleButton.Checked)
            {
                gender = 1;
            }

            
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Gender = gender;
            user.Name = name;
            user.ZodiacSign = zodiacsign;
            user.Interests = interestlist;
            user.Insta = insta;
            user.Telno = telno;

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();

                    // İlgi alanlarını virgülle ayırarak tek bir string haline getiriyoruz
                    string interests = string.Join(", ", user.Interests);

                    // Kullanıcı kaydını yapıyoruz
                    string kayit = "insert into info (username, name, ZodiacSign, gender, password, interests, insta, tel) values(@Username, @Name, @ZodiacSign, @Gender, @Password, @Interest, @Insta, @Telno)";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);

                    komut.Parameters.AddWithValue("@Username", user.Username);
                    komut.Parameters.AddWithValue("@Name", user.Name);
                    komut.Parameters.AddWithValue("@ZodiacSign", user.ZodiacSign);
                    komut.Parameters.AddWithValue("@Gender", user.Gender);
                    komut.Parameters.AddWithValue("@Password", user.Password);
                    komut.Parameters.AddWithValue("@Interest", interests);  // İlgi alanlarını tek bir string olarak buraya ekliyoruz
                    komut.Parameters.AddWithValue("@Insta", insta);
                    komut.Parameters.AddWithValue("@Telno", telno);

                    komut.ExecuteNonQuery();

                    baglanti.Close();
                    MessageBox.Show("Kayıt Başarılı");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayıt Başarısız \n  " + hata.Message);
            }
            
            

            this.Close();
            

        }
    }
}
