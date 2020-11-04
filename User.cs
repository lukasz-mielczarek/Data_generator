using System;
using System.Collections.Generic;
using System.Text;

namespace data_generator
{
    public class User
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Hash { set; get; }
        public bool Subscription { set; get; }

        public User(int id, string username, string email, string hash, bool sub)
        {
            Id = id;
            Username = username;
            Email = email;
            Hash = hash;
            Subscription = sub;

        }


    }
}
