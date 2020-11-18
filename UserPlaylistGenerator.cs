using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    public class UserPlaylistGenerator
    {
        
        public async Task PopulateUserPlaylists(NpgsqlConnection con)
        {
            Random random = new Random();
            for (int i = 1; i < 500000; i++)
            {
                var queryString = $"INSERT INTO \"user_playlists\" VALUES({i},{random.Next(1,250000)})";
                Console.WriteLine(queryString);
                await using (var cmd = new NpgsqlCommand(queryString, con))
                {

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
        

    }
} 
