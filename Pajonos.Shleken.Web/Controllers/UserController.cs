//using Pajonos.Shleken.Services.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//namespace Pajonos.Shleken.Web.Controllers
//{
//    public class UsersController : Controller
//    {
//        private ShlekenEntities3 db = new ShlekenEntities3();

//        // GET: Users
//        public ActionResult Index()
//        {
//            return View(db.Users.ToList());
//        }

//        // GET: Users/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Users Users = db.Users.Find(id);
//            if (Users == null)
//            {
//                return HttpNotFound();
//            }
//            return View(Users);
//        }

//        // GET: Users/Create
//        public ActionResult Create()
//        {
//            return View();
//        }
//        //פונקציה ליצירת משתשמש חדש
//        // POST: Users/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        /// <summary>
//        /// וכן בודקת האם המשתמש לא קיים במערכת
//        /// וכן מכניסה עמודות ברירת מחדל לאותו משתמש שעה וזמן התראה
//        ///  פונקציה המקבלת פרטי משתמש חדש ומכניסה אותם למסד נתונים ושומרת אותם ב session ובcookie
//        /// </summary>
//        /// <param name="Users"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "UserId,EmailAsLoginName,FirstName,LastName,Password")] Users Users)
//        {
//            Users u = new Services.Entities.Users();
//            Models.EmailSent email = new Models.EmailSent(); ;
//            Users b = null;
//            Session["dateofscreen"] = new DateTime(Convert.ToInt32(DateTime.Now.Year.ToString("0000")), Convert.ToInt32(DateTime.Now.Month.ToString("00")), Convert.ToInt32(DateTime.Now.Day.ToString("00")));
//            if (ModelState.IsValid)
//            {
//                var a = db.Users.Where(x => x. == Users.Password).ToList();
//                if (a.Count() > 0)
//                {
//                    b = a.Where(x => x.EmailAsLoginName == Users.EmailAsLoginName).FirstOrDefault();
//                }
//                if (b == null || a.Count() == 0)
//                {

//                    HttpCookie myCookie = new HttpCookie("UserId");
//                    myCookie.Value = Users.UserId.ToString();
//                    myCookie.Expires = DateTime.Now.AddYears(50);
//                    Response.Cookies.Add(myCookie);
//                    Session["Users"] = Users;
//                    if (Session["Users"] != null)
//                    {
//                        u = Session["Users"] as Users;
//                    }
//                    db.Users.Add(Users);
//                    db.SaveChanges();
//                    Models.EmailSent.sendemail(u, " ברוך הבא ל Custom - Cal", "  ברוך הבא" + " אנו מקווים שתהנה/י, בהצלחה ויום נעים ");
//                    ColumsInDiary c = new ColumsInDiary();
//                    c.UserId = u.UserId;
//                    c.ColName = "שעה";
//                    db.ColumsInDiarys.Add(c);
//                    ColumsInDiary c1 = new ColumsInDiary();
//                    c1.UserId = u.UserId;
//                    c1.ColName = "זמן התראה";
//                    db.ColumsInDiarys.Add(c1);
//                    db.SaveChanges();

//                    return RedirectToAction("Create", "Settings");
//                }
//                else
//                {
//                    ViewData["errormessage"] = "משתמש קיים במערכת,נסה שנית";
//                    return View(Users);
//                }

//            }

//            return View(Users);
//        }

//        // GET: Users/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Users Users = db.Users.Find(id);
//            if (Users == null)
//            {
//                return HttpNotFound();
//            }
//            return View(Users);
//        }

//        // POST: Users/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "UserId,EmailAsLoginName,FirstName,LastName,Password")] Users Users)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(Users).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(Users);
//        }

//        // GET: Users/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Users Users = db.Users.Find(id);
//            if (Users == null)
//            {
//                return HttpNotFound();
//            }
//            return View(Users);
//        }

//        // POST: Users/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Users Users = db.Users.Find(id);
//            db.Users.Remove(Users);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//        public ActionResult CheckUsers()
//        {
//            return View();
//        }
//        //פונקציה לבדיקת משתמש קיים
//        /// <summary>
//        ///  הפונקציה בודקת בעת כניסת משתמש קיים האם המשתמש נמצא במערכת 
//        /// </summary>
//        /// <param name="Users"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult CheckUsers([Bind(Include = "EmailAsLoginName,Password")] Users Users)
//        {

//            Session["dateofscreen"] = new DateTime(Convert.ToInt32(DateTime.Now.Year.ToString("0000")), Convert.ToInt32(DateTime.Now.Month.ToString("00")), Convert.ToInt32(DateTime.Now.Day.ToString("00")));

//            if (ModelState.IsValid)
//            {
//                var a = db.Users.Where(x => x.EmailAsLoginName == Users.EmailAsLoginName).ToList();
//                if (a.Count() > 0)
//                {
//                    var b = a.Where(x => x.Password == Users.Password).FirstOrDefault();
//                    if (b != null)
//                    {
//                        Session["Users"] = b;
//                        HttpCookie myCookie = new HttpCookie("UserId");
//                        myCookie.Value = b.UserId.ToString();
//                        myCookie.Expires = DateTime.Now.AddYears(50);
//                        Response.Cookies.Add(myCookie);
//                        Users u = Session["Users"] as Users;
//                        return RedirectToAction("tableView", "Settings");
//                    }
//                }


//            }
//            ViewData["errormessage"] = "משתמש לא קיים במערכת,נסה שנית";
//            return View(Users);
//        }
//        public ActionResult checkUsers_passpage()
//        {
//            return View();
//        }

//    }
//}