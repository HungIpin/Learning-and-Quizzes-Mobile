using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetCommentList
        [HttpGet]
        [Route("GetCommentList")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comment5
        [HttpGet]
        [Route("GetComment{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditComment{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AddComment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddComment")]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete]
        [Route("DeleteComment{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var subComments = await _context.SubComments.Where(e => e.ParentCommentId == comment.CommentId).ToListAsync();
            _context.SubComments.RemoveRange(subComments);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
        // GET: api/GetCommentList
        [HttpGet]
        [Route("GetCommentListByCourseId")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentListByCourseId(int id)
        {
            var comment = await _context.Comments.Where(e=>e.CourseId == id && e.Type == "Comment").OrderByDescending(e => e.CommentId).ToListAsync(); 
            if(comment != null)
            {
              var userList = await _context.Users.ToListAsync();
              comment.ForEach(e => {
                var result = userList.Find( u => u.UserId == e.UserId);
                e.User = (result != null) ? result : null;
              });
            }
            return comment;
        }
        // GET: api/GetRatingListByCourseId
        [HttpGet]
        [Route("GetRatingListByCourseId")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetRatingListByCourseId(int id)
        {
            return await _context.Comments.Where(e => e.CourseId == id && e.Type=="Rating").ToListAsync();
        }
    }
}
