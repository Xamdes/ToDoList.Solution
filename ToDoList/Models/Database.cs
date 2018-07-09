using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
  public class DB
  {
    private static MySqlConnection _conn;
    private static string _connectionString = DBConfiguration.ConnectionString;

    public static MySqlConnection GetConnection()
    {
      return _conn;
    }

    public static void OpenConnection()
    {
      _conn = new MySqlConnection(_connectionString);
      _conn.Open();
    }

    public static void CloseConnection()
    {
      _conn.Close();
      if(_conn!=null)
      {
        _conn.Dispose();
      }
    }

    public static void AddParameters(List<MySqlParameter> parameters, string command, string parameter)
    {
      parameters.Add(new MySqlParameter(command, parameter));
    }

    public static void RunSqlCommand(string commandText,List<MySqlParameter> parameters = null)
    {
      OpenConnection();
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      if(parameters!=null)
      {
        foreach (MySqlParameter parameter in parameters)
        {
          cmd.Parameters.Add(parameter);
        }
      }
      cmd.ExecuteNonQuery();
      CloseConnection();
    }

    public static MySqlDataReader ReadConnection(string commandText)
    {
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      return (cmd.ExecuteReader() as MySqlDataReader);
    }
  }
}
