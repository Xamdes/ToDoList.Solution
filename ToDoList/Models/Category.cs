using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ToDoList.Models
{
  public class Category
  {
    private static string _tableName = "categories";
    private int _id;
    private string _name;
    private List<Item> _itemList;


    public Category(string name, int id = 0)
    {
      _name = name;
      _id = id;
      _itemList = new List<Item>(){};
    }

    public int GetId()
    {
      return _id;
    }

    public void AddItem(Item newItem)
    {
      newItem.SetCategoryId(_id);
      _itemList.Add(newItem);
    }

    public List<Item> GetAllItems(string tableName, string orderBy = "id", string order = "ASC")
    {
      List<Item> allItems = new List<Item>{};
      DB.OpenConnection();
      DB.SetCommand(@"SELECT * FROM items WHERE category_id=@id ORDER BY "+orderBy+" "+order+";");
      DB.AddParameter("@id",_id);
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
      _itemList = allItems;
      return _itemList;
    }

    public void Save()
    {
      string columns = "category";
      List<string> ValueNames = new List<string>(){"@Name"};
      List<Object> values = new List<Object>(){_name};
      DB.SaveToTable(_tableName,columns,ValueNames,values);
      _id = DB.LastInsertId(_tableName);
    }
  }
}
