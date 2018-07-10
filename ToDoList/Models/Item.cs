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
      DB.OpenConnection();
      string commandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = DB.ReadConnection(commandText);
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      DB.CloseConnection();
      return allItems;
    }

    //Check if Unit Test setup correctly
    public static bool Default()
    {
      return true;
    }

    public int GetId()
    {
      return _id;
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
      string commandText = @"INSERT INTO items (description) VALUES (@Description);";
      DB.OpenConnection();
      DB.AddParameter("@Description", _description);
      DB.RunSqlCommand(commandText);
      DB.CloseConnection();
    }

    public static Item Find(int id)
    {
      int itemId = -1;
      string itemDescription = "";
      string commandText = @"SELECT * FROM items WHERE id=@thisId;";
      DB.OpenConnection();
      DB.AddParameter("@thisId",id);
      MySqlDataReader rdr = DB.ReadConnection(commandText);
      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      DB.CloseConnection();
      return (new Item(itemDescription,itemId));

    }

    public static void ClearAll(bool saveUniqueIds = true)
    {
      //_instances.Clear();
      string commandText = "";
      DB.OpenConnection();
      if(saveUniqueIds)
      {
        commandText = @"DELETE FROM items";
        DB.RunSqlCommand(commandText);
      }
      else
      {
        commandText = @"TRUNCATE TABLE items;";
        DB.RunSqlCommand(commandText);
      }
      DB.CloseConnection();
    }

    public override bool Equals(System.Object otherItem)
    {
      if (otherItem is Item)
      {
        Item newItem = (Item) otherItem;
        return this.GetDescription().Equals(newItem.GetDescription());
      }
      else
      {
        return false;
      }
    }

    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }
  }
}
