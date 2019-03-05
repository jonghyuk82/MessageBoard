using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Controllers
{
    public class RegisterController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Register
        public ActionResult Register()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Description");
            return View();
        }

        //  POST : Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "FirstName, LastName, Email, Password, RoleID")]User user)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("LogIn", "Register");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "ID", "Description", user.RoleID);
            return View(user);
        }

        //  GET: Log in
        public ActionResult LogIn()
        {
            return View();
        }

        //  POST: Log in
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(string email, string password)
        {
            if(ModelState.IsValid)
            {
                var obj = db.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
                if(obj != null)
                {
                    Session["LoginEmail"] = obj.Email.ToString();
                    Session["LoginName"] = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                    Session["LoginRole"] = obj.RoleID.ToString();
                    Session["LoginID"] = obj.ID.ToString();
                    return RedirectToAction("Index", "Board", null);
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Enter valide email and password.");
                    return View();
                }
            }
            return View(email, password);
        }

        //  GET: Log out
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn");
        }
    }
}