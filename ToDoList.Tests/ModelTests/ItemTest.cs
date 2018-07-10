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

    }
    [AssemblyInitialize]
    public void ItemTests()
    {
      DBConfiguration.SetConnection("server=localhost;user id=root;password=root;port=8889;database=sc_todolist_test;");
    }
    [ClassCleanup]
    public static void Cleanup()
    {
      Item.ClearAll();
    }
    [TestMethod]
    public void Return_True()
    {
      //Eventual Tests
      Assert.AreEqual(true, Item.Default());
    }
    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);
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
