﻿using Forum.Models;

namespace Forum.Data
{
    public interface IPosts
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFiltredPosts(int id,string serachQuery);
        IEnumerable<Post> GetFiltredPosts(string serachQuery);
        IEnumerable<Post> GetPostsByForum(int id);

        Task AddPost(Post post);
        Task DeletePost(int post);
        Task EditPostContent(int id, string newcContent);
        Task AddReply(PostReply reply);
        IEnumerable<Post> GetLastestPost(int id);
    }
}
