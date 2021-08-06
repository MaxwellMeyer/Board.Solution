using Microsoft.AspNetCore.Mvc;
using Board.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Board.Controllers
{
  public class TopicsController : Controller
  {
    private readonly BoardContext _db;

    public TopicsController(BoardContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Topic> model = _db.Topics.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Topic topic)
    {
      _db.Topics.Add(topic);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTopic = _db.Topics
          .Include(topic => topic.JoinEntities)
          .ThenInclude(join => join.Post)
          .FirstOrDefault(topic => topic.TopicId == id);
      return View(thisTopic);
    }

    public ActionResult Edit(int id)
    {
      var thisTopic = _db.Topics.FirstOrDefault(topic => topic.TopicId == id);
      return View(thisTopic);
    }

    [HttpPost]
    public ActionResult Edit(Topic topic)
    {
      _db.Entry(topic).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTopic = _db.Topics.FirstOrDefault(topic => topic.TopicId == id);
      return View(thisTopic);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTopic = _db.Topics.FirstOrDefault(topic => topic.TopicId == id);
      _db.Topics.Remove(thisTopic);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}