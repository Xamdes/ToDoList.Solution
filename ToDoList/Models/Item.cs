using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;


namespace ToDoList.Models
{
  public class Item
  {
    private static string _tableName = "items";
    private int _id;
    private int _categoryId;
    private string _description;
    private DateTime _date;

    public Item(string description, DateTime date, int Id = 0)
    {
      _id = Id;
      _description = description;
      _date = date;
      _categoryId = -1;
    }

    public Item(string description, int Id = 0)
    {
      _id = Id;
      _description = description;
      _date = new DateTime(1);
      _categoryId = -1;
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

    public int GetCategoryId()
    {
      return _categoryId;
    }

    public void SetCategoryId(int newId)
    {
      _categoryId = newId;
      //Update items database with category id
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
      List<string> values = new List<string>(){"@Description","@Date","@Category"};
      List<Object> parameters = new List<Object>(){_description,_date,_categoryId};
      DB.SaveToTable(_tableName,"description,date,category_id",values,parameters);
      _id = DB.LastInsertId(_tableName);
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
      DB.ClearTable(_tableName,saveUniqueIds);
    }

    public static void DeleteId(int deleteId)
    {
      DB.DeleteById(_tableName,deleteId);
    }

    public void Delete()
    {
      DB.DeleteById(_tableName,_id);
    }

    public void Edit(string what, Object editValue)
    {
      switch(what.ToLower())
      {
        case "category":
        _categoryId = (int)editValue;
        DB.Edit(_tableName,_id,"category_id",_categoryId);
        break;
        case "date":
        _date = (DateTime)editValue;
        DB.Edit(_tableName,_id,"date",_date);
        break;
        case "description":
        _description = editValue.ToString();
        DB.Edit(_tableName,_id,"description",_description);
        break;
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
