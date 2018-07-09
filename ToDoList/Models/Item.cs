using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace ToDoList.Models
{
  public class Item
  {
    private int _id;
    private string _description;

    public Item(string description, int Id = 0)
    {
      _id = Id;
      _description = description;
    }

    // Getters and Setters will go here
    public static List<Item> GetAll()
    {
      //return _instances;
      List<Item> allItems = new List<Item>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    //Check if Unit Test setup correctly
    public bool Default()
    {
      return true;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public void Save()
    {
      //_instances.Add(this);
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description) VALUES (@Description);";
      cmd.Parameters.Add(new MySqlParameter("@Description", _description));
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      //_instances.Clear();
    }
  }
}
