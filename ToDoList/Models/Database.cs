using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }

    public static MySqlConnection Open()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      return conn;
    }

    public static void Close(MySqlConnection conn)
    {
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public static void CreateCommand(string commandText)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public static void CreateCommand(string commandText, List<MySqlParameter> commandList)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      foreach (MySqlParameter param in commandList)
      {
        cmd.Parameters.Add(param);
      }
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

  }
}
