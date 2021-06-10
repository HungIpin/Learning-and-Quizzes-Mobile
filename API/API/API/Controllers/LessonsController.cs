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
    public class LessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetLessonList
        [HttpGet]
        [Route("GetLessonList")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _context.Lessons.ToListAsync();
        }

        // GET: api/Lessons/5
        [HttpGet]
        [Route("GetLesson{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        [HttpGet]
        [Route("CountLessons/{id}")]
        public async Task<ActionResult> GetNumOfLesson(int id)
        {
            var list = await _context.Lessons.Where(c => c.SubTopicId == id).ToListAsync();
            return Content(list.Count.ToString());
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditLesson{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/AddLesson
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddLesson")]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.LessonId }, lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete]
        [Route("DeleteLesson{id}")]
        public async Task<ActionResult<Lesson>> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return lesson;
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
        [HttpGet]
        [Route("GetLesson/{id}")]
        //https://localhost:44387/api/GetLesson/3
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonByCourseId(int id)
        {
            var list = await _context.Topics.Where(c => c.CourseId == id).ToListAsync();
            var list1 = await _context.SubTopics.ToListAsync();
            var list2 = await _context.Lessons.ToListAsync();
            //var subtopicount = list1.Count();
            List<SubTopic> result1 = new List<SubTopic>();
            List<Lesson> result2 = new List<Lesson>();
            int length = list.Count(), length1 = list1.Count(), length2 = list2.Count();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    if (list1[j].TopicId == list[i].TopicId)
                    {
                        result1.Add(list1[j]);
                    }
                }
            }
            for (int i = 0; i < result1.Count(); i++)
            {
                for (int j = 0; j < length2; j++)
                {
                    if (result1[i].SubTopicId == list2[j].SubTopicId)
                    {
                        result2.Add(list2[j]);
                    }
                }
            }
            return result2;
        }
    [HttpGet]
    [Route("GetLessonBeforeBought")]
    //https://localhost:44387/api/GetLesson/3
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonBeforeBoughtByCourseId(int id)
    {
      var list = await _context.Topics.Where(c => c.CourseId == id && c.IsLocked == false).ToListAsync();
      var list1 = await _context.SubTopics.ToListAsync();
      var list2 = await _context.Lessons.ToListAsync();
      //var subtopicount = list1.Count();
      List<SubTopic> result1 = new List<SubTopic>();
      List<Lesson> result2 = new List<Lesson>();
      int length = list.Count(), length1 = list1.Count(), length2 = list2.Count();
      for (int i = 0; i < length; i++)
      {
        for (int j = 0; j < length1; j++)
        {
          if (list1[j].TopicId == list[i].TopicId)
          {
            result1.Add(list1[j]);
          }
        }
      }
      for (int i = 0; i < result1.Count(); i++)
      {
        for (int j = 0; j < length2; j++)
        {
          if (result1[i].SubTopicId == list2[j].SubTopicId)
          {
            result2.Add(list2[j]);
          }
        }
      }
      return result2;
    }
    // GET: api/GetSubtopicBySubtopic/1
    [HttpGet]
        [Route("GetLessonBySubtopic/{id}")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonBySubtopicId(int id)
        {
            return await _context.Lessons.Where(l => l.SubTopicId == id).ToListAsync();
        }
    }
}
