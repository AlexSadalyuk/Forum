using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FirstExample.WebUI.Models;
using FirstExample.Core.Entities.Dto;
using FirstExample.Core.Interfaces.BusinessLogic;

namespace FirstExample.WebUI.Controllers
{
    [Authorize]
    public class PostController : Controller
    {

        private readonly IPostManager _post;
        private int _pageSize = 4;
        private int _commentsPerPage = 8;

        public PostController(IPostManager post)
        {
            _post = post;
        }

        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {

            Pagination<PostWithAuthor>  postList = _post.GetPosts(page, _pageSize);

            postList.PageInfo.Action = "Index";

            return View(postList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostCreate model)
        {
            if (ModelState.IsValid)
            {
                int newPost = _post.AddPost(new AddPost
                {
                    Name = model.Name,
                    Description = model.Description,
                    Author = HttpContext.User.Identity.Name
                });

                return RedirectToAction("Open", new { id = newPost });
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Open(int id, int page = 1)
        {

            PostWithComments post = 
                _post.GetPost(id, HttpContext.User.Identity.Name, page, _commentsPerPage);
            if (post != null)
            {
                post.Comments.PageInfo.Action = "Open";
                post.Comments.PageInfo.RoutId = id;
                
                return View(post);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateComment(CommentCreate model)
        {
            if (ModelState.IsValid)
            {
                _post.AddComment(new AddComment
                {
                    Text = model.Text,
                    Author = HttpContext.User.Identity.Name,
                    PostId = model.PostId
                });
            }
            return RedirectToAction("Open", new { id = model.PostId });
        }

        [HttpPost]
        public ActionResult UpdateComment(CommentUpdate model)
        {
            if (ModelState.IsValid)
            {
                _post.UpdateComment(model.CommentId, model.Text);
            }
            return RedirectToAction("Open", new { id = model.PostId, page = model.PageNum });
        }

    }
}