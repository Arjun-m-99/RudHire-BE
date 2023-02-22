using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rudhire_BE.Models;

namespace Rudhire_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<TblUserDetail>> PostTblUserDetail(TblUserDetail tblUserDetail)
        {
            
          if (_context.TblUserDetails == null)
          {
              return Problem("Entity set 'RudHireDbContext.TblUserDetails'  is null.");
          };
            var userDetails = new TblUserDetail
            {
                FirstName = tblUserDetail.FirstName,
                LastName = tblUserDetail.LastName,
                EmailId = tblUserDetail.EmailId,
                UserName = tblUserDetail.UserName,
                PhoneNumber = tblUserDetail.PhoneNumber,
                Password = tblUserDetail.Password,
                Gender = tblUserDetail.Gender,
                Dob = tblUserDetail.Dob,
                NickName = tblUserDetail.NickName,
                TblUserQualification = null,
            };
            _context.TblUserDetails.Add(userDetails);
            await _context.SaveChangesAsync();
            var qualification = new TblUserQualification
            {
                Degree = tblUserDetail.TblUserQualification.Degree,
                UniversityName = tblUserDetail.TblUserQualification.UniversityName,
                StartDate = tblUserDetail.TblUserQualification.StartDate,
                EndDate = tblUserDetail.TblUserQualification.EndDate,
                Percentage = tblUserDetail.TblUserQualification.Percentage,

            };
            //qualification = tblUserDetail.TblUserQualification;
            

            qualification.UserId = tblUserDetail.UserId;
            _context.TblUserQualifications.Add(qualification);
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

        private bool TblUserDetailExists(int id)
        {
            return (_context.TblUserDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
