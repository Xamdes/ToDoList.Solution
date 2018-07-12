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

    public List<Item> GetItems()
    {
      return _itemList;
    }

    public void Save()
    {
      List<string> values = new List<string>(){"@Name"};
      List<Object> parameters = new List<Object>(){_name};
      DB.SaveToTable(_tableName,"description,date,category_id",values,parameters);
      _id = DB.LastInsertId(_tableName);
    }
  }
}
