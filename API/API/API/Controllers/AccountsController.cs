using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using BC = BCrypt.Net.BCrypt;
namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetAccountList
        [HttpGet]
        [Route("GetAccountList")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/GetAccount1
        [HttpGet]
        [Route("GetAccount{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("EditAccount{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("AddAccount")]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (!AccountExists(account.Username))
            {
                account.Password = BC.HashPassword(account.Password);
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
            }
            return null;
        }
        // POST: api/Accounts/Signin
        [HttpPost]
        [Route("Signin")]
        public async Task<ActionResult<Account>> signIn(Account account)
        {
            var accountdb = await _context.Accounts.ToListAsync();

            foreach (Account a in accountdb)
            {
                if (a.Username == account.Username && BC.Verify(account.Password, a.Password))
                {
                    string username = a.Username;
                    var accountfind = _context.Accounts.Where(b => b.Username == username).FirstOrDefault();
                    return CreatedAtAction("GetAccount", new { id = accountfind.AccountId }, accountfind);
                }
            }
            return null;
        }

        // DELETE: api/DeleteAccount1
        [HttpDelete]
        [Route("DeleteAccount{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
        private bool AccountExists(string username)
        {
            return _context.Accounts.Any(e => e.Username == username);
        }
        [HttpPut]
        [Route("updateinstructor/{id}")]
        public async Task<IActionResult> upToInstructor(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            account.Role = "instructor";
            _context.Entry(account).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
            //return await _context.Users.Where(u => u.UserId == id).Include(a => a.Account).FirstAsync();
        }
        [HttpGet("AccountRole")]
        public async Task<ActionResult<IEnumerable<Account>>> GetListAccountByRoles(string role1, string role2)
        {
            return await _context.Accounts.Where(acc => acc.Role == role1 || acc.Role == role2).ToListAsync();
        }
    }
}
