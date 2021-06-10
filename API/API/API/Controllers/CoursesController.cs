using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.IO;

namespace API.Controllers
{
  [Route("api")]
  [ApiController]
  public class CoursesController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/GetCourseList
    [HttpGet]
    [Route("GetCourseList")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
      return await _context.Courses.Where(a => a.IsActive == true).OrderByDescending(c => c.CourseId).ToListAsync();
    }
    // GET: api/Courses/All
    [HttpGet]
    [Route("AllCourses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCoursesAll()
    {
      return await _context.Courses.OrderByDescending(c => c.CourseId).ToListAsync();
    }

    // GET: api/GetCourse5
    [HttpGet]
    [Route("GetCourse{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
      var course = await _context.Courses.FindAsync(id);

      if (course == null)
      {
        return NotFound();
      }

      return course;
    }
    [HttpGet]
    [Route("TopCourses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetTop5Certifications()
    {
      return await _context.Courses.Where(e => e.IsActive == true).OrderByDescending(e => e.Rating)
      .ThenByDescending(e => e.NumberOfVoters)
      .ThenByDescending(e => e.CourseId)
      .Take(6).ToListAsync();
    }
    // PUT: api/Courses/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut]
    [Route("EditCourse{id}")]
    public async Task<IActionResult> PutCourse(int id, [FromForm] Course course)
    {
      if (HttpContext.Request.Form.Files.Count > 0)
      {
        var file = HttpContext.Request.Form.Files[0];
        byte[] fileData = null;

        using (var binaryReader = new BinaryReader(file.OpenReadStream()))
        {
          fileData = binaryReader.ReadBytes((int)file.Length);
        }

        course.ThumbnailImage = fileData;
      }
      if (id != course.CourseId)
      {
        return BadRequest();
      }

      _context.Entry(course).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CourseExists(id))
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

    // POST: api/AddCourse
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    [Route("AddCourse")]
    public async Task<ActionResult<Course>> PostCourse([FromForm] Course course)
    {
      if (HttpContext.Request.Form.Files.Count > 0)
      {
        var file = HttpContext.Request.Form.Files[0];

        byte[] fileData = null;

        using (var binaryReader = new BinaryReader(file.OpenReadStream()))
        {
          fileData = binaryReader.ReadBytes((int)file.Length);
        }

        course.ThumbnailImage = fileData;
      }
      _context.Courses.Add(course);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (CourseExists(course.CourseId))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }
      return CreatedAtAction("GetCourses", new { id = course.CourseId }, course);
    }

    // DELETE: api/Courses/5
    [HttpDelete]
    [Route("DeleteCourse{id}")]
    public async Task<ActionResult<Course>> DeleteCourse(int id)
    {
      var course = await _context.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }
      var topic = await _context.Topics.Where(e => e.CourseId == id).ToListAsync();
      if (topic != null)
      {
        var subtopicResult = await _context.SubTopics.ToListAsync();
        var subtopicList = new List<SubTopic>();
        topic.ForEach(e =>
        {
          var result = subtopicResult.Find(s => s.TopicId == e.TopicId);
          if (result != null)
          {
            subtopicList.Add(result);
          }
        });
        if (subtopicList != null)
        {
          var lessonBefore = await _context.Lessons.ToListAsync();
          var lessonList = new List<Lesson>();
          for (var i = 0; i < subtopicList.Count(); i++)
          {
            lessonBefore.ForEach(
            e => {
              if (e.SubTopicId == subtopicList[i].SubTopicId)
              {
                lessonList.Add(e);
              }
            });
          }
          lessonList.ForEach(e => _context.Lessons.Remove(e));
          subtopicList.ForEach(e => _context.SubTopics.Remove(e));
          topic.ForEach(e => _context.Topics.Remove(e));
        }
      }
      _context.Courses.Remove(course);
      await _context.SaveChangesAsync();

      return course;
    }

    private bool CourseExists(int id)
    {
      return _context.Courses.Any(e => e.CourseId == id);
    }
    [HttpGet]
    [Route("GetCoursesWithAccountId")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCoursesWithAccountId(int id)
    {
      return await _context.Courses.Where(a => a.IsActive == true && a.AccountId == id).ToListAsync();
    }
    //For mobile
    public class MobileData
    {
      //public User user { get; set; }
      //public Course course { get; set; }
      public string CourseName { get; set; }
      public double CoursePrice { get; set; }
      public byte[] CourseThumbnailImage { get; set; }
      public string InstructorName { get; set; }
    }
    public class DesktopData
    {
      public User user { get; set; }
      public Course course { get; set; }
    }
    //Get Course Infomation for mobile
    [HttpGet]
    [Route("GetDataForMobile")]
    public async Task<ActionResult<IEnumerable<MobileData>>> GetDataForMobile(string option)
    {
      var course = await _context.Courses.ToListAsync();
      var account = await _context.Accounts.ToListAsync();
      var user = await _context.Users.ToListAsync();
      List<Account> accountList = new List<Account>();
      course.ForEach(e =>
      {
        var result = account.Find(a => a.AccountId == e.AccountId);
        accountList.Add(result);
      });
      List<User> userList = new List<User>();
      accountList.ForEach(a =>
      {
        var result = user.Find(u => u.UserId == a.UserId);
        userList.Add(result);
      });
      List<MobileData> data = new List<MobileData>();
      for (var i = 0; i < course.Count; i++)
      {
        var result = accountList.Find(a => a.AccountId == course[i].AccountId);
        var userResult = userList.Find(u => u.UserId == result.UserId);
        var mobiledata = new MobileData();
        mobiledata.CourseName = course[i].CourseName;
        mobiledata.CoursePrice = course[i].Price;
        mobiledata.CourseThumbnailImage = course[i].ThumbnailImage;
        mobiledata.InstructorName = userResult.FirstName + " " + userResult.LastName;
        data.Add(mobiledata);
      }
      return data;
    }
    [HttpPut]
    [Route("ViewCount")]
    public async Task<IActionResult> PutCourseCount(int id)
    {
      var course = await _context.Courses.FindAsync(id);
      course.ViewCount++;
      await _context.SaveChangesAsync();
      return NoContent();
    }
    //Get 6 Free Course / View Count High
    //Option: 1 for Free Course, 2 for view count hight 
    [HttpGet]
    [Route("GetTop6CourseMobile")]
    public async Task<ActionResult<IEnumerable<MobileData>>> GetTop6CourseMobile(int option,int accountId)
    {
      var account = await _context.Accounts.ToListAsync();
      var user = await _context.Users.ToListAsync();
      var mycourse = await _context.AccountinCourses.ToListAsync();
      List<Account> accountList = new List<Account>();
      var course = (option == 1) ? await _context.Courses.Where(e => e.Price == 0.0 && e.IsActive == true && (mycourse.Find(mc => mc.CourseId == e.CourseId && mc.IsBought == true && mc.AccountId == accountId) == null)).OrderByDescending(e => e.CourseId).Take(6).ToListAsync() :
       await _context.Courses.Where(e => e.IsActive == true && (mycourse.Find(mc => mc.CourseId == e.CourseId && mc.IsBought == true && mc.AccountId == accountId) == null)).OrderByDescending(e => e.ViewCount)
      .ThenByDescending(e => e.Rating)
      .ThenByDescending(e => e.CourseId)
      .Take(6)
      .ToListAsync();
      course.ForEach(e =>
      {
        var result = account.Find(a => a.AccountId == e.AccountId);
        accountList.Add(result);
      });
      List<User> userList = new List<User>();
      accountList.ForEach(a =>
      {
        var result = user.Find(u => u.UserId == a.UserId);
        userList.Add(result);
      });
      List<MobileData> data = new List<MobileData>();
      for (var i = 0; i < course.Count; i++)
      {
        var result = accountList.Find(a => a.AccountId == course[i].AccountId);
        var userResult = userList.Find(u => u.UserId == result.UserId);
        var mobiledata = new MobileData();
        mobiledata.CourseName = course[i].CourseName;
        mobiledata.CoursePrice = course[i].Price;
        mobiledata.CourseThumbnailImage = course[i].ThumbnailImage;
        mobiledata.InstructorName = userResult.FirstName + " " + userResult.LastName;
        data.Add(mobiledata);
      }
      return data;
    }
    [HttpGet]
    [Route("GetTop6CourseDesktop")]
    public async Task<ActionResult<IEnumerable<DesktopData>>> GetTop6CourseDesktop(int option)
    {
      var account = await _context.Accounts.ToListAsync();
      var user = await _context.Users.ToListAsync();
      List<Account> accountList = new List<Account>();
      var course = (option == 1) ? await _context.Courses.Where(e => e.Price == 0 && e.IsActive == true).OrderByDescending(e => e.CourseId).Take(6).ToListAsync() :
       await _context.Courses.Where(e => e.IsActive == true).OrderByDescending(e => e.ViewCount)
      .ThenByDescending(e => e.Rating)
      .ThenByDescending(e => e.CourseId)
      .Take(6)
      .ToListAsync();
      course.ForEach(e =>
      {
        var result = account.Find(a => a.AccountId == e.AccountId);
        accountList.Add(result);
      });
      List<User> userList = new List<User>();
      accountList.ForEach(a =>
      {
        var result = user.Find(u => u.UserId == a.UserId);
        userList.Add(result);
      });
      List<DesktopData> dataForDesktop = new List<DesktopData>();
      for (var i = 0; i < course.Count; i++)
      {
        var result = accountList.Find(a => a.AccountId == course[i].AccountId);
        var userResult = userList.Find(u => u.UserId == result.UserId);
        var desktopData = new DesktopData();
        desktopData.course = course[i];
        desktopData.user = userResult;
        dataForDesktop.Add(desktopData);
      }
      return dataForDesktop;
    }
    class CourseData
    {
      public Course course { get; set; }
      public List<TopicData> topicList { get; set; }
    }
    class TopicData{
      public Topic topic { get; set; }
      public List<SubtopicData> subtopicList { get; set; }
    }
    class SubtopicData
    {
      public SubTopic subtopic { get; set; }
      public List<Lesson> lessonList { get; set; }
    }
    [HttpGet]
    [Route("GetFullCourseData")]
    public ActionResult GetFullCourseData(int id)
    {
      var course = _context.Courses.Find(id);
      var courseData = new CourseData();
      var result = new SubTopic();
      var result1 = new Lesson();
      courseData.course = course;
      var topic = _context.Topics.Where(e => e.CourseId == id).ToList();
      var subtopic = _context.SubTopics.ToList();
      var lesson = _context.Lessons.ToList();
      if (topic != null)
      {
        courseData.topicList = new List<TopicData>();
        topic.ForEach(e =>
        {
          var topicData = new TopicData();
          topicData.topic = e;
          courseData.topicList.Add(topicData);
        });
        if(courseData.topicList.Count > 0)
        {
          courseData.topicList.ForEach(e =>
          {
            e.subtopicList = new List<SubtopicData>();
            result = subtopic.Find(s => s.TopicId == e.topic.TopicId);
            if(result != null)
            {
              var subtopicData = new SubtopicData();
              subtopicData.subtopic = result;
              subtopicData.lessonList = new List<Lesson>();
              lesson.ForEach(l =>
              {
                if(l.SubTopicId == result.SubTopicId)
                {
                  subtopicData.lessonList.Add(l);
                }
              });
              e.subtopicList.Add(subtopicData);
            }
          });
        }
      }
      return Ok(courseData);
    }
    //Search api
    // GET: api/GetCourse/name
    // 1 find course is bougth,2 course publish,default course all
    [HttpGet]
    [Route("SearchCourse")]

    public async Task<ActionResult<IEnumerable<Course>>> GetCourseSearch(string name,int option,int accountId)
    {
      List<Course> course = new List<Course>();
      Course result = new Course();
      switch (option)
      {
        case 1:
          var courseResult = await _context.Courses.Where(a => a.CourseName.ToLower().Contains(name.ToLower()) && a.IsActive == true).ToListAsync();
          var mycourse = await _context.AccountinCourses.Where(ac => ac.AccountId == accountId && ac.IsBought == true).ToListAsync();
          if (mycourse != null) {
            mycourse.ForEach(mc => {
              result = courseResult.Find(cr => cr.CourseId == mc.CourseId);
              if (result != null) course.Add(result);
            });
          }
          break;
        case 2:
          course = await _context.Courses.Where(a => a.CourseName.ToLower().Contains(name.ToLower()) && a.AccountId == accountId && a.IsActive == true).ToListAsync();
          break;
        default:
          course = await _context.Courses.Where(a => a.CourseName.ToLower().Contains(name.ToLower()) && a.IsActive == true).ToListAsync();
          break;
      }
     

      if (course == null)
      {
        return NotFound();
      }

      return course;
    }
    [HttpGet]
    [Route("FilterCourse")]

    public async Task<ActionResult<IEnumerable<Course>>> FilterCourse(string hastag)
    {
      List<Course> course = new List<Course>();
      Course result = new Course();
      //string[] hastags = hastag.Split(",");
      if(hastag != null)
      {
        string[] hastags = hastag.Split(",");
        var checkList = await _context.Courses.ToListAsync();
            for(var i = 0;i< hastags.Length; i++)
            {
              result = checkList.Find(e => e.Hastag.ToLower() == hastags[i]);
              if (result != null)
              {
                course.Add(result);
              }
            }
       }
      if (course == null)
      {
        return NotFound();
      }
      return course;
    }
  }
}
