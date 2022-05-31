using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TNAI.Model;
using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        //private ApplicationUserManager _userManager;
        // private AppDbContext db = new AppDbContext();

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Posts
        public async Task<ActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return View(posts);
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postRepository.GetPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Content")] Post post)
        {
            if (!ModelState.IsValid)
                return View(post);


            if (post != null)
                // Jako autora ustawiamy zalogowanego użytkownika (jego e-mail - ewn. można wyciąć tylko to, co jest przed małpą).
                post.Author = System.Web.HttpContext.Current.User.Identity.Name; // Czy to dobry sposób na dostanie się do bieżącego użytkownika?


            var result = await _postRepository.SavePostAsync(post);
            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await _postRepository.GetPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Author,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                await _postRepository.SavePostAsync(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await _postRepository.GetPostAsync(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await _postRepository.GetPostAsync(id);
            await _postRepository.DeletePostAsync(post.Id);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        public ActionResult CommentListPartial(int? id)
        {
            var post = Task.Run(() => _postRepository.GetPostAsync(id.Value)).Result;
            var comments = post.Comments;
            return PartialView("_commentsListPartial", comments);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
