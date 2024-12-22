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
        public List<string> Interests { get; set; }

        public User()
        {
            Interests = new List<string>();
        }

        // Yapıcı (constructor)
        public User(string username, string password, string name, string zodiacSign, int gender)
        {
            Username = username;
            Password = password;
            Name = name;
            ZodiacSign = zodiacSign;
            Gender = gender;
        }
        
    }

}

