using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ToDoList.Models
{

  public class Category
  {
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

    public List<Item> GetItems()
    {
      return _itemList;
    }

    public void Save()
    {
      DB.OpenConnection();
      DB.SetCommand(@"INSERT INTO categories (category) VALUES (@Name);");
      DB.AddParameter("@Name", _name);
      DB.RunSqlCommand();
      DB.ResetCommand();
      DB.SetCommand(@"SELECT Max(id) FROM categories;");
      MySqlDataReader rdr = DB.ReadSqlCommand();
      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      DB.CloseConnection();
    }
  }
}
