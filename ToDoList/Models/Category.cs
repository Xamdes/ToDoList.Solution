using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ToDoList.Models
{

  public class Category()
  {
    private int _id;
    private string _name;
    private List<Items> _itemList;


    public Category(string name, int id = 0)
    {
      _name = name;
      _id = id;
      _itemList = new List<Item>;
    }

  }
}
