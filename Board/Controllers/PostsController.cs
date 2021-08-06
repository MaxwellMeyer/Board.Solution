using Microsoft.AspNetCore.Mvc;
using Board.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Board.Controllers
{
  public class PostsController : Controller
  {
    private readonly BoardContext _db;

    public PostsController(BoardContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Posts.ToList());
    }
    public ActionResult Create()
    {
      ViewBag.TopicId = new SelectList(_db.Topics, "TopicId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Post post, int TopicId)
    {
      _db.Posts.Add(post);
      _db.SaveChanges();
      if (TopicId != 0)
      {
        _db.TopicPost.Add(new TopicPost() { TopicId = TopicId, PostId = post.PostId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Details(int id)
    {
      var thisPost = _db.Posts
          .Include(post => post.JoinEntities)
          .ThenInclude(join => join.Topic)
          .FirstOrDefault(post => post.PostId == id);
      return View(thisPost);
    }
    public ActionResult Edit(int id)
    {
      var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
      ViewBag.TopicId = new SelectList(_db.Topics, "TopicId", "Name");
      return View(thisPost);
    }

    [HttpPost]
    public ActionResult Edit(Post post, int TopicId)
    {
      if (TopicId != 0)
      {
        _db.TopicPost.Add(new TopicPost() { TopicId = TopicId, PostId = post.PostId });
      }
      _db.Entry(post).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTopic(int id)
    {
      var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
      ViewBag.TopicId = new SelectList(_db.Topics, "TopicId", "Name");
      return View(thisPost);
    }

    [HttpPost]
    public ActionResult AddTopic(Post post, int TopicId)
    {
      if (TopicId != 0)
      {
        _db.TopicPost.Add(new TopicPost() { TopicId = TopicId, PostId = post.PostId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Delete(int id)
    {
      var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
      return View(thisPost);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
      _db.Posts.Remove(thisPost);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTopic(int joinId)
    {
      var joinEntry = _db.TopicPost.FirstOrDefault(entry => entry.TopicPostId == joinId);
      _db.TopicPost.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // [HttpGet("/posts/{postId}/statusComplete")]
    // public ActionResult StatusComplete(int postId)
    // {
    //   Post post = _db.Posts.FirstOrDefault(post => post.PostId == postId);
    //   post.Status = true;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // [HttpGet("/posts/{postId}/statusIncomplete")]
    // public ActionResult StatusIncomplete(int postId)
    // {
    //   Post post = _db.Posts.FirstOrDefault(post => post.PostId == postId);
    //   post.Status = false;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
}