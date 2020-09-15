using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.PersonalRhythms;
using Lcist.Classes.PlayersRhythms;

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

                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
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

        public static IEnumerable<Player> GetUserPlayers(string localDbFileName, LcistUser user)
        {
            using (FbConnection connection = GetConnection(localDbFileName))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadPlayers, connection);
                command.Parameters.AddWithValue("idUser", user.Id);

                connection.Open();
                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new Player(reader);

                    reader.Close();
                }
            }
        }

        public static IEnumerable<Match> GetPlayerMatches(string localDbFileName, Player player)
        {
            using (FbConnection connection = GetConnection(localDbFileName))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadMatches, connection);
                command.Parameters.AddWithValue("idPlayer", player.Id);

                connection.Open();
                using (FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new Match(reader);

                    reader.Close();
                }
            }
        }

        public static IEnumerable<Query> GetPlayerQueries(string localDbFile, Player player)
        {
            using (FbConnection connection = GetConnection(localDbFile))
            {
                FbCommand command = new FbCommand(Resources.FbQueries.LoadQueries, connection);
                command.Parameters.AddWithValue("idPlayer", player.Id);

                connection.Open();
                using (FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        yield return new Query(reader);
                    reader.Close();
                }
            }
        }
    }
}
