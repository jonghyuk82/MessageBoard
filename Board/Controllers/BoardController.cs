using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace Board.Controllers
{
    public class BoardController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Board
        public ActionResult Index()
        {
            var post = db.Posts.Include(p => p.SubPost);

            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            ViewBag.ImportanceID = new SelectList(db.Importances, "ID", "Description");
            ViewBag.PriorityID = new SelectList(db.Priorities, "ID", "Description");

            return View(post.ToList());
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            ViewBag.ImportanceID = new SelectList(db.Importances, "ID", "Description");
            ViewBag.PriorityID = new SelectList(db.Priorities, "ID", "Description");

            return View();
        }

        //POST: Board/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Post post)
        {
            if(ModelState.IsValid)
            {
                post.UserID = Convert.ToInt32(Session["LoginID"].ToString());
                post.PriorityID = 1;
                post.RegisterDate = DateTime.Now;

                List<PostFile> postFiles = new List<PostFile>();
                for(int i=0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        PostFile uploadFile = new PostFile()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName)
                        };
                        postFiles.Add(uploadFile);

                        var path = Path.Combine(Server.MapPath("~/UploadFiles/") + uploadFile.FileName);
                        file.SaveAs(path);
                    }
                }

                post.PostFile = postFiles;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //  GET: Board/Edit
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);

            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", post.StatusID);
            ViewBag.ImportanceID = new SelectList(db.Importances, "ID", "Description", post.ImportanceID);
            ViewBag.PriorityID = new SelectList(db.Priorities, "ID", "Description", post.PriorityID);

            return View(post);
        }

        //  POST: Board/Edit
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID, UserID, Title, Message, StatusID, ImportanceID, DueDate, RegisterDate")]Post post, int priorityID)
        {
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            ViewBag.ImportanceID = new SelectList(db.Importances, "ID", "Description");
            ViewBag.PriorityID = new SelectList(db.Priorities, "ID", "Description");

            if(ModelState.IsValid)
            {
                post.PriorityID = priorityID;
                post.RegisterDate = DateTime.Now;
                post.UserID = Convert.ToInt32(Session["LoginID"].ToString());
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //  GET: Board/Delete
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Board/Detail
        public ActionResult Details(int id)
        {
            Post post = db.Posts
                .Include("PostFile")
                .Include("SubPostFile")
                .Include("SubPost").SingleOrDefault(x => x.ID == id);

            if(post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Update Priority
        [HttpPost]
        public ActionResult UpdatePriority(int postID, int priorityID)
        {
            Post p = (from x in db.Posts
                      where x.ID == postID
                      select x).First();
            p.PriorityID = priorityID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //  POST: Update Status
        [HttpPost]
        public ActionResult UpdateStatus (int postID, int statusID)
        {
            Post p = (from x in db.Posts
                      where x.ID == postID
                      select x).First();
            p.StatusID = statusID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //  GET: Post/Reply
        public ActionResult Reply(int id)
        {
            return View();
        }

        // POST: Post/Reply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(int id, SubPost sub)
        {
            if(ModelState.IsValid)
            {
                sub.PostID = id;
                sub.UserID = Convert.ToInt32(Session["LoginID"].ToString());
                sub.RegisterDate = DateTime.Now;

                List<SubPostFile> subPostFiles = new List<SubPostFile>();
                for(int i=0; i <Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        SubPostFile subPostFile = new SubPostFile()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            SubPostID = sub.ID
                        };
                        subPostFiles.Add(subPostFile);

                        var path = Path.Combine(Server.MapPath("~/UploadFiles/") + subPostFile.FileName);
                        file.SaveAs(path);
                    }
                }
                sub.SubPostFile = subPostFiles;
                db.SubPosts.Add(sub);
                db.SaveChanges();
                return RedirectToAction("Details", "Board", new { id = id });
            }
            return View(sub);
        }

    }
}