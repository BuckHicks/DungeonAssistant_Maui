using System;
using System.Data;
using System.Linq;

namespace DungeonAssistant.DataContracts.Interfaces;

public interface ISQLiteDataAccess
{
    DataSet ExecuteQuery(string query);
    DataSet ExecuteQuery(string query, params IDataParameter[] parameters);
    bool ExecuteNonQuery(string query);
    bool ExecuteNonQuery(string query, params IDataParameter[] parameters);
}
