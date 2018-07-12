using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using ToDoList;

namespace ToDoList.Models
{
  public class DB
  {
    private static MySqlConnection _conn;
    private static MySqlCommand _cmd;
    private static string _connectionString = DBConfiguration.GetConnection();

    public static MySqlConnection GetConnection()
    {
      return _conn;
    }

    public static void OpenConnection()
    {
      _conn = new MySqlConnection(_connectionString);
      _conn.Open();
      _cmd = _conn.CreateCommand() as MySqlCommand;
    }

    public static void CloseConnection()
    {
      _conn.Close();
      if(_conn!=null)
      {
        _conn.Dispose();
      }
      _cmd = null;
    }

    public static void ResetCommand()
    {
      _cmd = _conn.CreateCommand() as MySqlCommand;
    }

    public static void AddParameter(string name, Object parameterValue)
    {
      _cmd.Parameters.Add(new MySqlParameter(name, parameterValue));
    }

    public static void SetCommand(string commandText)
    {
      _cmd.CommandText = commandText;
    }

    public static void RunSqlCommand()
    {
      _cmd.ExecuteNonQuery();
    }

    public static MySqlDataReader ReadSqlCommand()
    {
      return (_cmd.ExecuteReader() as MySqlDataReader);
    }

    public static void Edit(string tableName,int id, string what,  Object editValue)
    {
      DB.OpenConnection();
      DB.SetCommand(@"UPDATE "+tableName+" SET "+what+" = @updateValue WHERE id = @searchId;");
      DB.AddParameter("@searchId",id);
      DB.AddParameter("@updateValue",editValue);
      DB.RunSqlCommand();
      DB.CloseConnection();
    }
  }
}



/*
CREATE TABLE `sc_todolist`.`items` ( `id` INT NOT NULL AUTO_INCREMENT , `description` VARCHAR(255) NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;
*/
