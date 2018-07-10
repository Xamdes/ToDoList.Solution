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
    private static List<MySqlParameter> _parameters;

    public static MySqlConnection GetConnection()
    {
      return _conn;
    }

    public static void OpenConnection()
    {
      _parameters = new List<MySqlParameter>(){};
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

    public static void AddParameter(string command, Object parameter)
    {
      _parameters.Add(new MySqlParameter(command, parameter));
    }

    public static void RunSqlCommand(string commandText)
    {
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      if(_parameters!=null)
      {
        foreach (MySqlParameter parameter in _parameters)
        {
          cmd.Parameters.Add(parameter);
        }
      }
      cmd.ExecuteNonQuery();
    }

    public static MySqlDataReader ReadConnection(string commandText)
    {
      MySqlCommand cmd = _conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = commandText;
      if(_parameters!=null)
      {
        foreach (MySqlParameter parameter in _parameters)
        {
          cmd.Parameters.Add(parameter);
        }
      }
      return (cmd.ExecuteReader() as MySqlDataReader);
    }
  }
}



/*
CREATE TABLE `sc_todolist`.`items` ( `id` INT NOT NULL AUTO_INCREMENT , `description` VARCHAR(255) NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;
*/
