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

    public static void AddParameter(MySqlParameter para)
    {
      _cmd.Parameters.Add(para);
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
      OpenConnection();
      SetCommand(@"UPDATE "+tableName+" SET "+what+" = @updateValue WHERE id = @searchId;");
      AddParameter("@searchId",id);
      AddParameter("@updateValue",editValue);
      RunSqlCommand();
      CloseConnection();
    }

    public static void ClearTable(string tableName, bool saveUniqueIds = true)
    {
      OpenConnection();
      if(saveUniqueIds)
      {
        SetCommand(@"DELETE FROM "+tableName+";");
      }
      else
      {
        SetCommand(@"TRUNCATE TABLE "+tableName+";");
      }
      RunSqlCommand();
      CloseConnection();
    }

    public static void DeleteById(string tableName, int deleteId)
    {
      OpenConnection();
      SetCommand(@"DELETE FROM "+tableName+" WHERE id=@id");
      AddParameter("@id",deleteId);
      RunSqlCommand();
      CloseConnection();
    }

    public static void SaveToTable(string tableName,string columns,List<string> values,List<Object> parameters)
    {
      string valueString = string.Join(",",values);
      OpenConnection();
      SetCommand(@"INSERT INTO "+tableName+" ("+columns+") VALUES ("+valueString+");");
      for(int i = 0; i<parameters.Count();i++)
      {
        AddParameter(values[i], parameters[i]);
      }
      RunSqlCommand();
      CloseConnection();
    }

    public static int LastInsertId(string tableName, string sid = "id")
    {
      int id = -1;
      OpenConnection();
      SetCommand(@"SELECT Max("+sid+") FROM "+tableName+";");
      MySqlDataReader rdr = ReadSqlCommand();
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
      }
      CloseConnection();
      return id;
    }
  }
}



/*
CREATE TABLE `sc_todolist`.`items` ( `id` INT NOT NULL AUTO_INCREMENT , `description` VARCHAR(255) NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;
*/
