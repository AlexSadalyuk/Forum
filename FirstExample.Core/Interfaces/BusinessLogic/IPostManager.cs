using System.Collections.Generic;
using FirstExample.Core.Entities.Dto;

namespace FirstExample.Core.Interfaces.BusinessLogic
{
    public interface IPostManager
    {
        PostWithComments GetPost(int postId, string author, int page, int items);
        Pagination<PostWithAuthor> GetPosts(int page, int items);

        int AddPost(AddPost model);
        void UpdateComment(int commentId, string text);
        void AddComment(AddComment model);
    }
}
