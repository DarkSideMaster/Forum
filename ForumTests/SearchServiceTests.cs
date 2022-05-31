using Forum.Data;
using Forum.ForumServises;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ForumTests
{
    [TestFixture]
    public class Post_Service_Should
    {
        [TestCase("coffee",3)]
        [TestCase("coffEE", 3)]
        [TestCase("WaTer", 0)]
        public void Return_Filtred_Results_Corresponding_To_Query(string query, int expected) 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum.Models.Forums
                {
                    Id = 14,
                });

                ctx.Posts.Add(new Forum.Models.Post
                {
                    Forums = ctx.Forums.Find(14),
                    Id = 24545,
                    Title = "First Post",
                    Content = "Coffee",

                });

                ctx.Posts.Add(new Forum.Models.Post
                {
                    Forums = ctx.Forums.Find(14),
                    Id = -24345,
                    Title = "Coffee",
                    Content = "Coffee",

                });

                ctx.Posts.Add(new Forum.Models.Post
                {
                    Forums = ctx.Forums.Find(14),
                    Id = 24,
                    Title = "Third Post",
                    Content = "CoffEE",

                });

                ctx.SaveChanges();
            }


            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);

                var result = postService.GetFiltredPosts(query);

                var postCount =result.Count();

                //Assert
                Assert.AreEqual(expected, postCount);
            }
        }
    }
}