using System;
using System.Collections.Generic;
using System.Linq;
using FirstExample.Core.Entities.Dto;
using FirstExample.Core.Entities.Domain;
using FirstExample.Core.Interfaces.Repositories;
using FirstExample.Core.Interfaces.BusinessLogic;

namespace FirstExample.BusinessLogic.Concrete
{
    public class PostManager : IPostManager
    {
        private readonly IUnitOfWork _unit;

        public PostManager(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public int AddPost(AddPost model)
        {
            Post post = new Post
            {
                Name = model.Name,
                Description = model.Description,
                DateTime = DateTime.Now,
                UserId = GetAuthorsId(model.Author)
            };
            _unit.Posts.Add(post);
            _unit.Complete();
            return post.Id;
        }

        public PostWithComments GetPost(int postId, string author, int page, int items)
        {
            Post post = _unit.Posts.Get(postId);
            if (post != null)
            {
                IEnumerable<EditableComment> comments = _unit.Comments
                    .GetFullRange(postId, (page - 1) * items, items, (x) => x.DateTime)
                    .Select(x => new EditableComment
                    {
                        Text = x.Text,
                        PostId = x.PostId,
                        CommentId = x.Id,
                        Author = x.User?.Name ?? "noname",
                        DateTime = x.DateTime,
                        IsAbleToEdit = (x.User?.Name == author)
                    }).ToList();

                PostWithComments newPost = new PostWithComments
                {
                    Name = post.Name,
                    Description = post.Description,
                    Author = author,
                    DateTime = post.DateTime,
                    Comments = new Pagination<EditableComment>
                    {
                        Items = comments,
                        PageInfo = new PagInfo
                        {
                            CurrPage = page,
                            ItemsPerPage = items,
                            TotalItems = _unit.Comments
                                                .GetByPost(postId)
                                                .Count()
                        }
                    }
                };
                return newPost;
            }
            return null;
        }

        public void AddComment(AddComment model)
        {
            Comment comment = new Comment
            {
                PostId = model.PostId,
                DateTime = DateTime.Now,
                Text = model.Text,
                UserId = GetAuthorsId(model.Author)
            };
            _unit.Comments.Add(comment);
            _unit.Complete();
        }

        public void UpdateComment(int commentId, string text)
        {
            Comment comment = _unit.Comments.Get(commentId);
            comment.Text = text;
            _unit.Comments.Update(comment);
            _unit.Complete();
        }

        private int GetAuthorsId(string name)
        {
            return _unit.Users.GetAll()
                              .FirstOrDefault(x => x.Name == name).Id;
        }

        public Pagination<PostWithAuthor> GetPosts(int page = 1, int items = 4)
        {
            IEnumerable<PostWithAuthor> list = _unit.Posts
                                                    .GetFullRange((page - 1) * items, items, (x) => x.DateTime)
                                                    .Select(x => new PostWithAuthor
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        Author = x.User?.Name ?? "noname",
                                                        CommentsQuantity = x.Comments.Count
                                                    });

            Pagination<PostWithAuthor> posts = new Pagination<PostWithAuthor>
            {
                Items = list,
                PageInfo = new PagInfo
                {
                    CurrPage = page,
                    ItemsPerPage = items,
                    TotalItems = _unit.Posts.Count
                }
            };

            return posts;
        }
    }
}
