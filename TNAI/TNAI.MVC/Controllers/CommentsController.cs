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
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        // private AppDbContext db = new AppDbContext();

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: Comments
        public async Task<ActionResult> Index()
        {
            var posts = await _commentRepository.GetAllCommentsAsync();
            return View(posts);
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = await _commentRepository.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create(int PostId)
        {
            var comment = new Comment();
            comment.PostId = PostId;
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Text,PostId")] Comment comment)
        {
            if (!ModelState.IsValid)
                return View(comment);

            if (comment != null)
                comment.Author = System.Web.HttpContext.Current.User.Identity.Name;

            var result = await _commentRepository.SaveCommentAsync(comment);

            if (!result)
                return View("Error");

            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Comment comment = await _commentRepository.GetCommentAsync(id.Value);

            if (comment == null)
                return HttpNotFound();

            if (comment.Author != System.Web.HttpContext.Current.User.Identity.Name)
                return View("AccessDenied");

            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                // Aby zachować zawartość pozostałych pól).
                Comment orginal = await _commentRepository.GetCommentAsync(comment.Id);
                orginal.Text = comment.Text;

                await _commentRepository.SaveCommentAsync(orginal);

                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Posts");// Najlepiej gdyby tutaj wracać do miejsca gdzie kliknięto "Edit" na poście.
            }

            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = await _commentRepository.GetCommentAsync(id.Value);

            if (comment == null)
            {
                return HttpNotFound();
            }

            if (comment.Author != System.Web.HttpContext.Current.User.Identity.Name)
                return View("AccessDenied");

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await _commentRepository.GetCommentAsync(id);
            await _commentRepository.DeleteCommentAsync(comment.Id);
            return RedirectToAction("Index");
        }

    }

}
