using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
  public class DB
  {
    private static MySqlConnection _conn;
    private static string connectionString = DBConfiguration.ConnectionString;

    public static MySqlConnection GetConnection()
    {
      return _conn;
    }

    public static void OpenConnection()
    {
      _conn = new MySqlConnection(connectionString);
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

    public static void CreateCommand(string commandText)
    {
      OpenConnection();
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      cmd.ExecuteNonQuery();
      CloseConnection();
    }

    public static void CreateCommand(string commandText, List<MySqlParameter> commandList)
    {
      //MySqlConnection conn = DB.Connection();
      OpenConnection();
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      foreach (MySqlParameter param in commandList)
      {
        cmd.Parameters.Add(param);
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
