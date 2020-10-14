using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Blog.Controllers;
using System.Linq;
using System.Collections.Generic;

namespace TestBlog
{
    public class Tests
    {
        private string nameDB;
        private BlogContext context; 

        [SetUp]
        public void Setup()
        {
            nameDB = Guid.NewGuid().ToString();
            context = CreateContext(nameDB);
            var authors = new Author[] {
                    new Author{Name = "Author1", Pass = "123", Mail = "author1@gmail.com"},
                    new Author{Name = "Author2", Pass = "123", Mail = "author2@gmail.com"},
                    new Author{Name = "Author3", Pass = "123", Mail = "author3@gmail.com"}
                };

            foreach (Author a in authors)
            {
                context.Author.Add(a);
            }

            var posts = new Post[] {
                    new Post{Title = "Post1", Body = "Body1", Date = DateTime.Now.Date, AuthorId = 1 },
                    new Post{Title = "Post2", Body = "Body2", Date = DateTime.Now.AddDays(1).Date, AuthorId = 1 },
                    new Post{Title = "Post3", Body = "Body3", Date = DateTime.Now.AddDays(2).Date, AuthorId = 2 },
                    new Post{Title = "Post4", Body = "Body4", Date = DateTime.Now.AddDays(1).Date, AuthorId = 2 },
                    new Post{Title = "Post5", Body = "Body5", Date = DateTime.Now.AddDays(3).Date, AuthorId = 3 }
                };

            foreach (Post p in posts)
            {
                context.Post.Add(p);
            }
        }

        [Test]
        public void TestGetAll()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);

            var controller = new BlogController(context2);
            var result = controller.GetAll().Result;

            Assert.AreEqual(5,result.Value.Count());
        }

        [Test]
        public void TestGetByAuthorId()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);

            var controller = new BlogController(context2);
            var result = controller.GetByAuthorId(1).Result;
            Post[] posts = new Post[] {
               new Post{PostId = 1 , Title = "Post1",Body = "Body1",Date = DateTime.Now.Date,AuthorId = 1,
                Author = new Author { Name = "Author1", Pass = "123", Mail = "author1@gmail.com" }
                }
            };
            Assert.AreEqual(posts, result.Value);
            Assert.AreEqual(posts.Count(), result.Value.Count());
        }

        [Test]
        public void TestGetById()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);

            var controller = new BlogController(context2);
            var result = controller.GetByAuthorId(1).Result;
           
            Post post = new Post
            {
                PostId = 1,
                Title = "Post1",
                Body = "Body1",
                Date = DateTime.Now.Date,
                AuthorId = 1,
                Author = new Author { Name = "Author1", Pass = "123", Mail = "author1@gmail.com" }
            };
            Assert.AreEqual(post, result.Value);
        }

        [Test]
        public void TestPost()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);
            Post post = new Post
            {
                Title = "PostNew",
                Body = "BodyNew",
                Date = DateTime.Now.Date,
                AuthorId = 1
            };

            var controller = new BlogController(context2);
            var result = controller.Post(post).Result;

            Assert.AreEqual(6, result.Value.Count());
        }

        [Test]
        public void TestDelete()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);
           
            Post post = new Post
            {
                PostId = 1,
                Title = "Post1",
                Body = "Body1",
                Date = DateTime.Now.Date,
                AuthorId = 1,
                Author = new Author { Name = "Author1", Pass = "123", Mail = "author1@gmail.com" }
            };

            var controller = new BlogController(context2);
            var result = controller.DeletePost(1).Result;

            Assert.AreEqual(post, result.Value);
        }

        [Test]
        public void TestPut()
        {
            this.Setup();
            var context2 = CreateContext(nameDB);
            Post post = new Post
            {
                PostId = 1,
                Title = "Post1",
                Body = "Body1",
                Date = DateTime.Now.Date,
                AuthorId = 1,
                Author = new Author { Name = "Author1", Pass = "123", Mail = "author1@gmail.com" }
            };

            var controller = new BlogController(context2);
            var result = controller.Put(1, post).Result;

            Assert.AreEqual(post, result.Value);
        }

        private BlogContext CreateContext(string nameDB)
        {
            var options = new DbContextOptionsBuilder<BlogContext>().UseInMemoryDatabase(nameDB).Options;
            var dbContext = new BlogContext(options);
            return dbContext;
        }
    }
}