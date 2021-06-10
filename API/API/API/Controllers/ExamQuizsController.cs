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
    public class ExamQuizsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamQuizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetExamQuizList
        [HttpGet]
        [Route("GetExamQuizList")]
        public async Task<ActionResult<IEnumerable<ExamQuiz>>> GetExamQuizs()
        {
            return await _context.ExamQuizs.ToListAsync();
        }

        // GET: api/GetExamQuiz1
        [HttpGet]
        [Route("GetExamQuiz{id}")]
        public async Task<ActionResult<ExamQuiz>> GetExamQuiz(int id)
        {
            var examQuiz = await _context.ExamQuizs.FindAsync(id);

            if (examQuiz == null)
            {
                return NotFound();
            }

            return examQuiz;
        }

        // PUT: api/ExamQuizs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditExamQuiz{id}")]
        public async Task<IActionResult> PutExamQuiz(int id, ExamQuiz examQuiz)
        {
            if (id != examQuiz.ExamQuizId)
            {
                return BadRequest();
            }

            _context.Entry(examQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamQuizExists(id))
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

        // POST: api/ExamQuizs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPost]
        [Route("AddExamQuiz")]
        public async Task<ActionResult<ExamQuiz>> PostExamQuiz(ExamQuiz examQuiz)
        {
            if (ExamQuizCheckExists(examQuiz.QuizId, examQuiz.ExamQuizCode) != true)
            {
                _context.ExamQuizs.Add(examQuiz);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetExamQuiz", new { id = examQuiz.ExamQuizId }, examQuiz);
            }
            return null;
        }
        // DELETE: api/ExamQuizs/5
        [HttpDelete]
        [Route("DeleteExamQuizs{id}")]
        public async Task<ActionResult<ExamQuiz>> DeleteExamQuiz(int id)
        {
            var examQuiz = await _context.ExamQuizs.FindAsync(id);
            if (examQuiz == null)
            {
                return NotFound();
            }

            _context.ExamQuizs.Remove(examQuiz);
            await _context.SaveChangesAsync();

            return examQuiz;
        }

        private bool ExamQuizExists(int id)
        {
            return _context.ExamQuizs.Any(e => e.ExamQuizId == id);
        }
        private bool ExamQuizQuestionExists(string question)
        {
            return _context.ExamQuizs.Any(e => e.ExamQuestion == question);
        }
        private bool ExamQuizQuizIdExists(string id)
        {
            return _context.ExamQuizs.Any(e => e.QuizId == id);
        }
        [HttpGet]
        [Route("GetExamQuizListOrderByExamCode")]
        public async Task<ActionResult<IEnumerable<ExamQuiz>>> GetExamQuizsOrderbyExamCode(string id)
        {
            return await _context.ExamQuizs.Where(e => e.CourseId == id)
              .OrderBy(e => e.ExamQuizCode)
              .ThenBy(e => e.ExamQuizId)
              .ToListAsync();
        }
        [HttpGet]
        [Route("GetExamQuizListByExamCode")]
        public async Task<ActionResult<IEnumerable<ExamQuiz>>> GetExamQuizListByExamCode(string examcode)
        {
            return await _context.ExamQuizs.Where(e => e.ExamQuizCode == examcode)
              .OrderByDescending(e => e.ExamQuizId)
              .ToListAsync();
        }
        private bool ExamQuizCheckExists(string id, string examCode)
        {
            return _context.ExamQuizs.Any(e => e.QuizId == id && e.ExamQuizCode == examCode);
        }
    }
}
