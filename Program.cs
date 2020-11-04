using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace data_generator
{
    class Program
    {
        static async Task Main()
        {
            /*List<string> names = new List<string>();
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
            NameGenerator nameGenerator = new NameGenerator();
            var allUsers = nameGenerator.generete_names(names, secondnames);
            */
            var cs = "Host=localhost;Username=postgres;Password=mam5lat;Database=SongDB";

            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();



            /*foreach (User user in allUsers)
            {
                var queryString = $"INSERT INTO \"Users\" VALUES({user.Id}, '{user.Username}', '{user.Email}' ,'{user.Hash}',{user.Subscription})";
                Console.WriteLine(queryString);
                await using (var cmd = new NpgsqlCommand(queryString, con))
                {
                    
                    await cmd.ExecuteNonQueryAsync();
                    
                }
            }*/
            /*UserPlaylistGenerator userPlaylistGenerator = new UserPlaylistGenerator();
            await userPlaylistGenerator.PopulateUserPlaylists(con);*/

            PlaylistGenerator playlistGenerator = new PlaylistGenerator();
            await playlistGenerator.PopulatePlaylists(con);




            

            

            


        }
    }
}
