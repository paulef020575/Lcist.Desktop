using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Lcist.Classes.PersonalRhythms;
using Lcist.Resources;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.BaseClasses
{
    public class MySqlDataProvider
    {
        public static IEnumerable<LcistUser> GetLcistUsers()
        {
            List<LcistUser> result = new List<LcistUser>();
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(MySqlQueries.LoadUsers, connection);

                connection.Open();
                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        result.Add(new LcistUser(reader));
                    }

                    reader.Close();
                }
            }

            return result;
        }

        public static IEnumerable<PersonalDay> GetPersonalDays(LcistUser user)
        {
            List<PersonalDay> result = new List<PersonalDay>();
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(MySqlQueries.LoadDays, connection);
                command.Parameters.AddWithValue(nameof(user), user.Id);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        result.Add(new PersonalDay(reader));
                    }

                    reader.Close();
                }
            }

            return result;
        }

        public static MySqlConnection GetConnection()
        {
            MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = "lcist.ru",
                Database = "neiro",
                UserID = "neiroUser",
                Password = "52108"
            };

            return new MySqlConnection(connectionStringBuilder.ConnectionString);
        }

        public static IEnumerable<bool> CheckUploading(LcistUser user, IEnumerable<PersonalDay> days)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.CheckId, connection);
                command.Parameters.AddWithValue("user", user.Id);
                command.Parameters.Add("date", MySqlDbType.Date);

                connection.Open();
                foreach (PersonalDay day in days)
                    yield return day.CheckUploading(command);
                connection.Close();
            }
        }
    }
}
