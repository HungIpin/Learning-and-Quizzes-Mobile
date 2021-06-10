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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetUserList
        [HttpGet]
        [Route("GetUserList")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        // GET: api/GetUser5
        [HttpGet]
        [Route("GetUser{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Where(u => u.UserId == id).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/EditUser5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditUser{id}")]
        public async Task<IActionResult> PutUser(int id, [FromForm] User user)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                user.AvatarPath = fileData;
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetUsers", "Users");
                }
            }
            return NoContent();
            //return await _context.Users.Where(u => u.UserId == id).Include(a => a.Account).FirstAsync();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<User>> PostUser([FromForm] User user)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                user.AvatarPath = fileData;
            }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }


        // DELETE: api/DeleteUser5
        [HttpDelete]
        [Route("DeleteUser{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        [HttpGet]
        [Route("GetUserByAccountId")]
        public async Task<ActionResult<User>> GetUserByAccountId(int id)
        {
            var user = await _context.Users.Where(u => u.Account.AccountId == id).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
