using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data
{
    public class DBInicializador
    {

        public static void Inicializar(BlogContext context)
        {
            context.Database.EnsureCreated();

            //chequeo si ya existen usuarios 
            if (!context.Author.Any())
            {

                var authors = new Author[] {
                    new Author{Name = "Author1", Pass = "123", Mail = "author1@gmail.com"},
                    new Author{Name = "Author2", Pass = "123", Mail = "author2@gmail.com"},
                    new Author{Name = "Author3", Pass = "123", Mail = "author3@gmail.com"}
                };

                foreach (Author a in authors)
                {
                    context.Author.Add(a);
                }
            }
            if (!context.Post.Any())
            {
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

            context.SaveChanges();

        }
    }
}
