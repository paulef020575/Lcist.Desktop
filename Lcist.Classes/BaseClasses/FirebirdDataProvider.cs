using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.PersonalRhythms;

namespace Lcist.Classes.BaseClasses
{
    public class FirebirdDataProvider
    {
        private static FbConnection GetConnection(string localDbFileName)
        {
            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder
            {
                DataSource = "localhost",
                Database = localDbFileName,
                UserID = "sysdba",
                Password = "masterkey"
            };

            return new FbConnection(connectionStringBuilder.ToString());
        }

        public static IEnumerable<LcistUser> GetLcistUsers(string localDbFileName)
        {
            using (FbConnection connection = GetConnection(localDbFileName))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadUsers, connection);
                connection.Open();

                using (FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new LcistUser(reader);

                    reader.Close();
                }
            }
        }

        public static IEnumerable<PersonalDay> GetUserDays(string localDbFileName, LcistUser user)
        {
            using (FbConnection connection = GetConnection(localDbFileName))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadDays, connection);
                command.Parameters.AddWithValue("idUser", user.Id);

                connection.Open();
                using (FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new PersonalDay(reader);

                    reader.Close();
                }
            }
        }

        public static IEnumerable<PersonalResult> GetUserResults(string localDbFileName, LcistUser user)
        {
            using (FbConnection connection = GetConnection(localDbFileName))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadPersonalResults, connection);
                command.Parameters.AddWithValue("idUser", user.Id);

                connection.Open();
                using (FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new PersonalResult(reader);
                    reader.Close();
                }
            }
        }
    }
}
