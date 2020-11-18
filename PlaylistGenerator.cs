using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    public class PlaylistGenerator
    {
        public async Task PopulatePlaylists(NpgsqlConnection con)
        {
            UserGenerator nameGenerator = new UserGenerator();

            Random random = new Random();
            for (int i = 1; i < 700001; i++)
            {
                var queryString = $"INSERT INTO \"Playlists\" VALUES({i},'{nameGenerator.RandomPassword(random.Next(5,10))}',{random.Next(1, 500000)})";
                Console.WriteLine(queryString);
                await using (var cmd = new NpgsqlCommand(queryString, con))
                {

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
