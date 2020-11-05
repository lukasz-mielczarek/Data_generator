using CommandLine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace data_generator
{
    public class UserGenerator
    {
        Random random = new Random();
        public string RandomPassword(int size = 0)
        {
            
            StringBuilder builder = new StringBuilder();
            
            char ch;
            for (int i = 0; i < size; i++)
            {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
            }
            int num = random.Next(1000,10000);
            builder.Append(num);
            

            return builder.ToString();
            

        }
            public List<User> generete_names()
        {
            List<string> names = new List<string>();
            List<string> secondnames = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\Łukasz\source\repos\data_generator\data_generator\data\us-500.csv"))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    names.Add(values[0]);
                    secondnames.Add(values[1]);
                }
            }
            

            List<User> usernames = new List<User>();
            int index = 1; 
            foreach (string name in names)
            {
                foreach(string secondname in secondnames)
                {
                    
                    StringBuilder tempUsername = new StringBuilder();
                    tempUsername.Append(name+secondname);
                    string username = tempUsername.ToString();
                    string email = username + "@gmail.com";
                    string hash = RandomPassword(15);
                    int randbool = random.Next() % 2;
                    bool sub = Convert.ToBoolean(randbool);
                    User user = new User(
                        index,
                        username,
                        email,
                        hash,
                        sub
                        );
                    index++;
                    Console.WriteLine($"{user.Id}.{user.Username} {user.Email} {user.Hash} {user.Subscription}");
                    usernames.Add(user);
                }
                
            }



            
            return usernames;
        }
        
            public async Task PopulateUsers(NpgsqlConnection con)
        {
            var allUsers = generete_names();
            foreach (User user in allUsers)
            {
                var queryString = $"INSERT INTO \"Users\" VALUES({user.Id}, '{user.Username}', '{user.Email}' ,'{user.Hash}',{user.Subscription})";
                Console.WriteLine(queryString);
                await using (var cmd = new NpgsqlCommand(queryString, con))
                {

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
            
            


        
    }

}  

