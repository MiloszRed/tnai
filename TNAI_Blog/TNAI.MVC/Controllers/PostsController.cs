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
using TNAI.Repository.Abstract;

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
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            return View(posts);
        }

        // GET: Posts/Author/{author}
        [AllowAnonymous]
        public async Task<ActionResult> Author(string id) // Nie działa jak zmieni się nazwę argumentu na inną...
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // BadRequest: 400

            var posts = await _postRepository.GetAuthorPostsAsync(id);

            if (posts == null || posts.Count == 0)
                return HttpNotFound("No posts for that user."); // NotFound: 404

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
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Content")] Post post)
        {
            if (!ModelState.IsValid)
                return View(post);


            if (post != null)
                // Jako autora ustawiamy zalogowanego użytkownika (UserName).
                post.Author = System.Web.HttpContext.Current.User.Identity.Name; // Czy to dobry sposób na dostanie się do bieżącego użytkownika?
            
            System.Diagnostics.Debug.WriteLine(post.Author);

            post.DateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");
            var result = await _postRepository.SavePostAsync(post);

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = await _postRepository.GetPostAsync(id.Value);

            if (post == null)
                return HttpNotFound();

            //if (post.Author != System.Web.HttpContext.Current.User.Identity.Name)
            //    return View("AccessDenied");

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                // Aby zachować zawartość pozostałych pól (Author i DataTime).
                Post orginal = await _postRepository.GetPostAsync(post.Id);

                if (orginal == null)
                    return HttpNotFound();

                if (User.IsInRole("User") && User.Identity.Name != orginal.Author)
                    return View("AccessDenied");

                orginal.Content = post.Content;
                orginal.Title = post.Title;

                await _postRepository.SavePostAsync(orginal);
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = await _postRepository.GetPostAsync(id.Value);

            if (post == null)
                return HttpNotFound();

            //if (post.Author != System.Web.HttpContext.Current.User.Identity.Name)
            //    return View("AccessDenied");

            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Admin, User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await _postRepository.GetPostAsync(id);
            if (User.IsInRole("User") && User.Identity.Name != post.Author)
                return View("AccessDenied");
            await _postRepository.DeletePostAsync(post.Id);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        [AllowAnonymous]
        public ActionResult CommentListPartial(int? id)
        {
            var post = Task.Run(() => _postRepository.GetPostAsync(id.Value)).Result;
            var comments = post.Comments;
            return PartialView("_commentsListPartial", comments);
        }

        //public ActionResult PostListPartial(IEnumerable<Post> posts)
        //{
        //    return PartialView("_postsListPartial", posts);
        //}

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
