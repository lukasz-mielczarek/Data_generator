using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    class FollowersGenerator
    {
        public async Task PopulateFollowers(NpgsqlConnection con)
        {
            

            Random random = new Random();
            for (int i = 1; i < 200001; i++)
            {
                var queryString = $"INSERT INTO \"Followers\" VALUES({i},{random.Next(1, 250001)},{random.Next(1, 250001)})";
                Console.WriteLine(queryString);
                await using (var cmd = new NpgsqlCommand(queryString, con))
                {

                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
