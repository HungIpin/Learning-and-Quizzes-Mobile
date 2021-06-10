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
    public class AccountinLessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountinLessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetAccountinLessonList
        [HttpGet]
        [Route("GetAccountinLessonList")]
        public async Task<ActionResult<IEnumerable<AccountinLesson>>> GetAccountinLessons()
        {
            return await _context.AccountinLessons.ToListAsync();
        }

        // GET: api/GetAccountinLesson1
        [HttpGet]
        [Route("GetAccountinLesson{id}")]
        public async Task<ActionResult<AccountinLesson>> GetAccountinLesson(int id)
        {
            var accountinLesson = await _context.AccountinLessons.FindAsync(id);

            if (accountinLesson == null)
            {
                return NotFound();
            }

            return accountinLesson;
        }

        // PUT: api/EditAccountinLesson1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditAccountinLesson{id}")]
        public async Task<IActionResult> PutAccountinLesson(int id, AccountinLesson accountinLesson)
        {
            if (id != accountinLesson.AccountinLessonID)
            {
                return BadRequest();
            }

            _context.Entry(accountinLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountinLessonExists(id))
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

        // POST: api/AddAccountinLesson
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddAccountinLesson")]
        public async Task<ActionResult<AccountinLesson>> PostAccountinLesson(AccountinLesson accountinLesson)
        {
            _context.AccountinLessons.Add(accountinLesson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountinLessonExists(accountinLesson.AccountinLessonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountinLesson", new { id = accountinLesson.AccountinLessonID }, accountinLesson);
        }

        // DELETE: api/DeleteAccountinLesson1
        [HttpDelete]
        [Route("DeleteAccountinLesson{id}")]
        public async Task<ActionResult<AccountinLesson>> DeleteAccountinLesson(int id)
        {
            var accountinLesson = await _context.AccountinLessons.FindAsync(id);
            if (accountinLesson == null)
            {
                return NotFound();
            }

            _context.AccountinLessons.Remove(accountinLesson);
            await _context.SaveChangesAsync();

            return accountinLesson;
        }

        private bool AccountinLessonExists(int id)
        {
            return _context.AccountinLessons.Any(e => e.AccountinLessonID == id);
        }
        [HttpGet]
        [Route("GetExamQuizHistory")]
        public async Task<ActionResult<IEnumerable<AccountinLesson>>> GetExamQuizHistory(string quizCode)
        {
            return await _context.AccountinLessons.Where(a => a.ExamQuizCode == quizCode).ToListAsync();
        }
    [HttpGet]
    [Route("GetExamQuizAttempByAccountId")]
    public async Task<ActionResult<IEnumerable<AccountinLesson>>> GetExamQuizAttempByAccountId(string accountId)
    {
      var result = await _context.AccountinLessons.Where(a => a.AccountId == accountId).ToListAsync();
      if(result != null)
      {
        List<AccountinLesson> list = new List<AccountinLesson>();
        var quizCodeCheck = result[0].ExamQuizCode;
        list.Add(result[0]);
        result.ForEach(e =>
        {
          if (e.ExamQuizCode != quizCodeCheck)
          {
            list.Add(e);
            quizCodeCheck = e.ExamQuizCode;
          }
        });
        return list;
      }
      return result;
    }

  }
}
