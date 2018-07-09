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
      string commandText = @"INSERT INTO items (description) VALUES (@Description);";
      List<MySqlParameter> parameters = new List<MySqlParameter>(){};
      DB.AddParameters(parameters,"@Description", _description);
      DB.RunSqlCommand(commandText,parameters);
    }

    public static Item Find(int id)
    {
      int itemId = -1;
      string itemDescription = "";
      DB.OpenConnection();
      string commandText = @"SELECT * FROM 'items' WHERE id = @thisId;";
      MySqlDataReader rdr = DB.ReadConnection(commandText);
      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      DB.CloseConnection();
      return (new Item(itemDescription,itemId));

    }

    public static void ClearAll(bool saveUniqueIds)
    {
      //_instances.Clear();
      if(saveUniqueIds)
      {
        string commandText = @"DELETE FROM items";
        DB.RunSqlCommand(commandText);
      }
      else
      {
        string commandText = @"TRUNCATE TABLE items;";
        DB.RunSqlCommand(commandText);
      }
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
