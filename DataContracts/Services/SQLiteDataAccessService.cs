using System;
using System.Configuration;
using System.Data;
using DungeonAssistant.DataContracts.Interfaces;
using Microsoft.Data.Sqlite;

namespace DungeonAssistant.DataContracts.Services;

public class SQLiteDataAccessService : ISQLiteDataAccess
{
    public DataSet ExecuteQuery(string query)
    {
        return ExecuteQuery(query, new IDataParameter[0]);
    }

    public DataSet ExecuteQuery(string query, params IDataParameter[] parameters)
    {
        DataSet dt = new DataSet();

        try
        {
            using (IDbConnection cnn = new SqliteConnection(GetConnectionString()))
            {
                using (var command = cnn.CreateCommand())
                {
                    command.CommandText = query;

                    foreach (IDataParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    command.Connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.IsClosed == false)
                        {
                            dt.Tables.Add().Load(reader);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return dt;
    }

    public bool ExecuteNonQuery(string query)
    {
        return ExecuteNonQuery(query, new IDataParameter[0]);
    }

    public bool ExecuteNonQuery(string query, params IDataParameter[] parameters)
    {
        bool success = false;

        try
        {
            using (IDbConnection cnn = new SqliteConnection(GetConnectionString()))
            {
                using (var command = cnn.CreateCommand())
                {
                    command.CommandText = query;

                    foreach (IDataParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    command.Connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("ExecuteNonQuery returned with a response code other than success");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return success;
    }

    private string GetConnectionString(string id = "Default")
    {
        //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        return @"Data Source=.\DungeonAssistantDb.db; Version = 3; ";
    }
}
//" connectionString="Data Source=.\DungeonAssistantDb.db; Version = 3; " providerName="System.Data.SqlClient" />
