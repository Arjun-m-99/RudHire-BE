using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rudhire_BE.Models;

namespace Rudhire_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUserDetailsController : ControllerBase
    {
        private readonly RudHireDbContext _context;

        public TblUserDetailsController(RudHireDbContext context)
        {
            _context = context;
        }

        // GET: api/TblUserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUserDetail>>> GetTblUserDetails()
        {
          if (_context.TblUserDetails == null)
          {
              return NotFound();
          }
            return await _context.TblUserDetails.ToListAsync();
        }

        // GET: api/TblUserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUserDetail>> GetTblUserDetail(int id)
        {
          if (_context.TblUserDetails == null)
          {
              return NotFound();
          }
            var tblUserDetail = await _context.TblUserDetails.FindAsync(id);

            if (tblUserDetail == null)
            {
                return NotFound();
            }

            return tblUserDetail;
        }

        // PUT: api/TblUserDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUserDetail(int id, TblUserDetail tblUserDetail)
        {
            if (id != tblUserDetail.UserId)
            {
                return BadRequest();
            }

            _context.Entry(tblUserDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserDetailExists(id))
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

        // POST: api/TblUserDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUserDetail>> PostTblUserDetail(TblUserDetail tblUserDetail)
        {
          if (_context.TblUserDetails == null)
          {
              return Problem("Entity set 'RudHireDbContext.TblUserDetails'  is null.");
          }
            _context.TblUserDetails.Add(tblUserDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUserDetail", new { id = tblUserDetail.UserId }, tblUserDetail);
        }

        // DELETE: api/TblUserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserDetail(int id)
        {
            if (_context.TblUserDetails == null)
            {
                return NotFound();
            }
            var tblUserDetail = await _context.TblUserDetails.FindAsync(id);
            if (tblUserDetail == null)
            {
                return NotFound();
            }

            _context.TblUserDetails.Remove(tblUserDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult UserLogin(LoginRequest loginRequest)
        {
            var user = _context.TblUserDetails.SingleOrDefault(x => x.UserName == loginRequest.UserName.ToLower() && x.Password == loginRequest.Password);


            return NotFound();
        }




        private bool TblUserDetailExists(int id)
        {
            return (_context.TblUserDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
