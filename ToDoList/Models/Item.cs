using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace ToDoList.Models
{
  public class Item
  {
    private int _id;
    private string _description;
    private DateTime _date;

    public Item(string description, DateTime date, int Id = 0)
    {
      _id = Id;
      _description = description;
      _date = date;
    }

    public Item(string description, int Id = 0)
    {
      _id = Id;
      _description = description;
      _date = new DateTime(1);
    }

    public static List<Item> GetAll(string orderBy = "id", string order = "ASC")
    {
      List<Item> allItems = new List<Item>{};
      DB.OpenConnection();
      DB.SetCommand(@"SELECT * FROM items ORDER BY "+orderBy+" "+order+";");
      MySqlDataReader rdr = DB.ReadSqlCommand();
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
        Item newItem = new Item(itemDescription,date,itemId);
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

    public string GetDate()
    {
      string tempString = _date.ToString("MM/dd/yyyy");
      if(tempString == "01/01/0001")
      {
        return "N/A";
      }
      return tempString;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public void Save()
    {
      DB.OpenConnection();
      DB.SetCommand(@"INSERT INTO items (description,date) VALUES (@Description,@Date);");
      DB.AddParameter("@Description", _description);
      DB.AddParameter("@Date", _date);
      DB.RunSqlCommand();
      DB.ResetCommand();
      DB.SetCommand(@"SELECT Max(id) FROM items;");
      MySqlDataReader rdr = DB.ReadSqlCommand();
      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      DB.CloseConnection();
    }

    public static Item Find(int id)
    {
      int itemId = -1;
      string itemDescription = "";
      DateTime date = new DateTime(0);
      DB.OpenConnection();
      DB.SetCommand(@"SELECT * FROM items WHERE id=@thisId;");
      DB.AddParameter("@thisId",id);
      MySqlDataReader rdr = DB.ReadSqlCommand();
      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
        date = rdr.GetDateTime(2);
      }
      DB.CloseConnection();
      return (new Item(itemDescription,date,itemId));

    }

    public static void ClearAll(bool saveUniqueIds = true)
    {
      //_instances.Clear();
      string commandText = @"DELETE FROM items";
      DB.OpenConnection();
      if(!saveUniqueIds)
      {
        commandText = @"TRUNCATE TABLE items;";
      }
      DB.SetCommand(commandText);
      DB.RunSqlCommand();
      DB.CloseConnection();
    }

    public void Edit(string newDescription)
    {
      DB.OpenConnection();
      DB.SetCommand(@"UPDATE items SET description = @newDescription WHERE id = @searchId;");
      DB.AddParameter("@newDescription",newDescription);
      DB.AddParameter("@searchId",_id);
      DB.RunSqlCommand();
      DB.CloseConnection();
      _description = newDescription;
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
