using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }

    [HttpGet("/items/date")]
    public ActionResult IndexDate()
    {
      List<Item> allItems = Item.GetAll("date","ASC");
      return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpGet("/items/{id}")]
    public ActionResult Details(int id)
    {
      Item item = Item.Find(id);
      return View(item);
    }

    [HttpGet("/items/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
        Item thisItem = Item.Find(id);
        return View(thisItem);
    }

    [HttpPost("/items/{id}/update")]
    public ActionResult Update(int id)
    {
        Item thisItem = Item.Find(id);
        thisItem.Edit(Request.Form["newDesc"]);
        return RedirectToAction("Index");
    }

    [HttpGet("/items/{id}/delete")]
    public ActionResult Delete(int id)
    {
        Item.DeleteId(id);
        return RedirectToAction("Index");
    }

    [HttpPost("/items")]
    public ActionResult Create(string description, DateTime date)
    {
      Item newItem = new Item (description,date);
      newItem.Save();
      return RedirectToAction("Index");
    }

    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()
    {
      Item.ClearAll(true);
      return View();
    }
  }

}
