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
    public class TopicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet]
        [Route("GetTopicList")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }
        [HttpGet]
        [Route("CountTopics/{id}")]
        public async Task<ActionResult> GetNumOfTopic(int id)
        {
            var list = await _context.Topics.Where(c => c.CourseId == id).ToListAsync();
            return Content(list.Count.ToString());
        }
        [HttpGet]
        [Route("GetTopicByCourseId/{id}")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicByCourseId(int id)
        {
            return await _context.Topics.Where(c => c.CourseId == id).ToListAsync();
        }
        // GET: api/GetTopic5
        [HttpGet]
        [Route("GetTopic{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditTopic{id}")]
        public async Task<IActionResult> PutTopic(int id, Topic topic)
        {
            if (id != topic.TopicId)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // POST: api/AddTopic
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddTopic")]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopic", new { id = topic.TopicId }, topic);
        }

        // DELETE: api/Topics/5
        [HttpDelete]
        [Route("DeleteTopic{id}")]
        public async Task<ActionResult<Topic>> DeleteTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            var subtopic = await _context.SubTopics.Where(e => e.TopicId == topic.TopicId).ToListAsync();
            if (subtopic != null)
            {
                var lessonBefore = await _context.Lessons.ToListAsync();
                var lessonList = new List<Lesson>();
                for (var i = 0; i < subtopic.Count(); i++)
                {
                    lessonBefore.ForEach(
                      e =>
                      {
                          if (e.SubTopicId == subtopic[i].SubTopicId)
                          {
                              lessonList.Add(e);
                          }
                      }
                    );
                }
                lessonList.ForEach(e => _context.Lessons.Remove(e));
                subtopic.ForEach(e => _context.SubTopics.Remove(e));
            }

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return topic;
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.TopicId == id);
        }
    }
}
