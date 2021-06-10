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
    public class AccountinCoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountinCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetAccountInCoursesList
        [HttpGet]
        [Route("GetAccountInCoursesList")]
        public async Task<ActionResult<IEnumerable<AccountinCourse>>> GetAccountinCourses()
        {
            return await _context.AccountinCourses.ToListAsync();
        }

        // GET: api/GetAccountInCourses5
        [HttpGet]
        [Route("GetAccountInCourses{id}")]
        public async Task<ActionResult<AccountinCourse>> GetAccountinCourse(int id)
        {
            var accountinCourse = await _context.AccountinCourses.FindAsync(id);

            if (accountinCourse == null)
            {
                return NotFound();
            }

            return accountinCourse;
        }

        // PUT: api/AccountinCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditAccountInCourse{id}")]
        public async Task<IActionResult> PutAccountinCourse(int id, AccountinCourse accountinCourse)
        {
            if (id != accountinCourse.AccountinCourseID)
            {
                return BadRequest();
            }

            _context.Entry(accountinCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountinCourseExists(id))
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

        // POST: api/AddAccountInCourse
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddAccountInCourse")]
        public async Task<ActionResult<AccountinCourse>> PostAccountinCourse(AccountinCourse accountinCourse)
        {
            _context.AccountinCourses.Add(accountinCourse);
            var course = await _context.Courses.FindAsync(accountinCourse.CourseId);
            course.NumberOfParticipants++;
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountinCourseExists(accountinCourse.AccountinCourseID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountinCourse", new { id = accountinCourse.AccountinCourseID }, accountinCourse);
        }

        // DELETE: api/AccountinCourses/5
        [HttpDelete]
        [Route("DeleteAccountInCourse{id}")]
        public async Task<ActionResult<AccountinCourse>> DeleteAccountinCourse(int id)
        {
            var accountinCourse = await _context.AccountinCourses.FindAsync(id);
            if (accountinCourse == null)
            {
                return NotFound();
            }

            _context.AccountinCourses.Remove(accountinCourse);
            await _context.SaveChangesAsync();

            return accountinCourse;
        }

        private bool AccountinCourseExists(int id)
        {
            return _context.AccountinCourses.Any(e => e.AccountinCourseID == id);
        }
        [HttpGet]
        [Route("GetAccountInCoursesByAccountid")]
        public async Task<IEnumerable<AccountinCourse>> GetAccountInCoursesByAccountid(int id,int option)
        {

        var accountinCourse = (option == 1) ? await _context.AccountinCourses.Where(e => e.AccountId == id && e.IsBought == true).OrderByDescending(e => e.AccountinCourseID).ToListAsync() :
        await _context.AccountinCourses.Where(e => e.AccountId == id && e.GetPayment == true).OrderByDescending(e => e.AccountinCourseID).ToListAsync();

            if (accountinCourse == null)
            {
                return null;
            }

            return accountinCourse;
        }
    [HttpGet]
    [Route("GetAccountInCoursesByInvoiceCode")]
    public async Task<IEnumerable<AccountinCourse>> GetAccountInCoursesByInvoiceCode(int option,string invoiceCode)
    {

      var accountinCourse = (option == 1) ? await _context.AccountinCourses.Where(e => e.InvoiceCode == invoiceCode && e.IsBought == true).OrderByDescending(e => e.AccountinCourseID).ToListAsync() :
      await _context.AccountinCourses.Where(e => e.InvoiceCode == invoiceCode && e.GetPayment == true).OrderByDescending(e => e.AccountinCourseID).ToListAsync();

      if (accountinCourse == null)
      {
        return null;
      }

      return accountinCourse;
    }
    [HttpGet]
    [Route("GetMyCourseList")]
    public async Task<IEnumerable<Course>> GetMyCourseList(int id)
    {
      List<Course> courseList = new List<Course>();
      var accountinCourse = await _context.AccountinCourses.Where(e => e.AccountId == id && e.IsBought == true).ToListAsync();
      if (accountinCourse != null)
      {
        var list = await _context.Courses.Where(e => e.IsActive == true).ToListAsync();
        accountinCourse.ForEach(e =>
        {
          var result = list.Find(course => course.CourseId == e.CourseId);
          courseList.Add(result);
        });
        return courseList;
      }
      return courseList;
    }
  }
}
