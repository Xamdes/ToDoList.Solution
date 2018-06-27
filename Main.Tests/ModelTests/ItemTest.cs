using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;
using System.Collections.Generic;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {
    public void Dispose()
    {
      Item.ClearAll();
    }
    [TestMethod]
    public void Return_True()
    {
      //Eventual Tests
      Item list = new Item("NA");
      Assert.AreEqual(true, list.Default());
    }
    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);
      newItem.Save();

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }
    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);
      description = "Do the dishes";
      //Act
      newItem.SetDescription(description);
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }
    [TestMethod]
    public void GetDescription_ReturnsDescription_String_2nd()
    {
      //Arrange
     string description = "Walk the dog.";
     Item newItem = new Item(description);
     newItem.Save();

     //Act
     List<Item> instances = Item.GetAll();
     Item savedItem = instances[0];

     //Assert
     Assert.AreEqual(newItem.GetDescription(), savedItem.GetDescription());
    }
  }
}
