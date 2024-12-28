using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProje
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ZodiacSign { get; set; }
        public int Gender { get; set; }

        public string Insta {  get; set; }

        public string Telno { get; set; }
        public List<string> Interests { get; set; }

        public int Puan {  get; set; }

        public User()
        {
            Interests = new List<string>();
        }

        // Yapıcı (constructor)
        public User(string username, string password, string name, string zodiacSign, int gender, string insta, string telno)
        {
            Username = username;
            Password = password;
            Name = name;
            ZodiacSign = zodiacSign;
            Gender = gender;
            Telno = telno;
            Insta = insta;
            Puan = 0;
        }
        
    }

}

