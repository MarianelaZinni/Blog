using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            return await _context.Post.Include(a => a.Author).ToListAsync();
        }


        [Route("[action]/{PostId}")]
        [HttpGet]
        public async Task<ActionResult<Post>> GetById(int PostId)
        {
            var post = await _context.Post.Include(a => a.Author).Where(p => p.PostId == PostId).FirstOrDefaultAsync();
            
            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [Route("[action]/{AuthorId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetByAuthorId(int AuthorId)
        {
            var post = await _context.Post.Include(a => a.Author).Where(p => p.AuthorId == AuthorId).ToListAsync();

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpDelete("{PostId}")]
        public async Task<ActionResult<Post>> DeletePost(int PostId)
        {
            var post = await _context.Post.FindAsync(PostId);
            if (post == null)
            {
                return NotFound();
            }
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Post>>> Post([FromBody] Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return await this.GetAll();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post value)
        {
            var post = _context.Post.FirstOrDefault(p => p.PostId == id);
            if (post == null) 
            {
                return NotFound();
            }
             
            var author = _context.Author.FirstOrDefault(a => a.AuthorId == post.AuthorId);

            if (author == null)
            {
                return NotFound();
            }

            post.Title = value.Title;
            post.Body = value.Body;
            post.AuthorId = value.AuthorId;

            _context.Post.Update(post);
            _context.SaveChanges();
            return await this.GetById(id);
        }
    }
}
