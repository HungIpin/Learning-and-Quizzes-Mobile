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
    public class SubCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetSubCommentList
        [HttpGet]
        [Route("GetSubCommentList")]
        public async Task<ActionResult<IEnumerable<SubComment>>> GetSubComments()
        {
            return await _context.SubComments.ToListAsync();
        }

        // GET: api/GetSubComment5
        [HttpGet]
        [Route("GetSubComment{id}")]
        public async Task<ActionResult<SubComment>> GetSubComment(int id)
        {
            var subComment = await _context.SubComments.FindAsync(id);

            if (subComment == null)
            {
                return NotFound();
            }

            return subComment;
        }

        // PUT: api/SubComments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditSubComment{id}")]
        public async Task<IActionResult> PutSubComment(int id, SubComment subComment)
        {
            if (id != subComment.SubCommentId)
            {
                return BadRequest();
            }

            _context.Entry(subComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCommentExists(id))
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

        // POST: api/AddSubComment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddSubComment")]
        public async Task<ActionResult<SubComment>> PostSubComment(SubComment subComment)
        {
            _context.SubComments.Add(subComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubComment", new { id = subComment.SubCommentId }, subComment);
        }

        // DELETE: api/DeleteSubComment5
        [HttpDelete]
        [Route("DeleteSubComment{id}")]
        public async Task<ActionResult<SubComment>> DeleteSubComment(int id)
        {
            var subComment = await _context.SubComments.FindAsync(id);
            if (subComment == null)
            {
                return NotFound();
            }

            _context.SubComments.Remove(subComment);
            await _context.SaveChangesAsync();

            return subComment;
        }

        private bool SubCommentExists(int id)
        {
            return _context.SubComments.Any(e => e.SubCommentId == id);
        }
        [HttpGet]
        [Route("GetSubCommentListByParentId")]
        public async Task<ActionResult<IEnumerable<SubComment>>> GetSubCommentListByParentId(int id)
        {
            var subComment = await _context.SubComments.Where(e => e.ParentCommentId == id).OrderBy(e => e.SubCommentId).ToListAsync();
          if (subComment != null)
          {
        var userList = await _context.Users.ToListAsync();
          subComment.ForEach(e => {
          var result = userList.Find(u => u.UserId == e.UserId);
          e.User = (result != null) ? result : null;
        });
          }
      return subComment;
        }
    }
}
