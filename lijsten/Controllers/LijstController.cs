using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lijsten.Models;
using Castle.Core.Internal;
using Lijsten.Models;

namespace lijsten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LijstController : ControllerBase
    {
        private readonly DataContext _context;

        public LijstController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Lijst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lijst>>> GetLijsten()
        {
            return await _context.Lijsten.ToListAsync();
        }

        // GET: api/Lijst/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lijst>> GetLijst(long id)
        {
            var lijst = await _context.Lijsten.FindAsync(id);

            if (lijst == null)
            {
                return NotFound();
            }

            return lijst;
        }

        // GET: api/Lijst/naam/zoek
        [HttpGet("naam/{zoek?}")]
        public async Task<ActionResult<IEnumerable<Lijst>>> GetNaamContains(string zoek = null)
        {
            if (String.IsNullOrEmpty(zoek))
            {
                return await GetLijsten();
            }

            var lijsten = await _context.Lijsten.Where(l => l.Naam.Contains(zoek)).ToListAsync();

            if (lijsten == null)
            {
                return NotFound();
            }

            return lijsten;
        }

        // GET: api/Lijst/gebruiker/gebruikerID
        [HttpGet("gebruiker/{id}")]
        public async Task<ActionResult<IEnumerable<Lijst>>> GetByGebruiker(int id)
        {
            var lijsten = await _context.Lijsten.Where(l => l.Eigenaar.GebruikerID == id).ToListAsync();

            if (lijsten == null)
            {
                return NotFound();
            }

            return lijsten;
        }

        // PUT: api/Lijst/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLijst(long id, Lijst lijst)
        {
            if (id != lijst.LijstID)
            {
                return BadRequest();
            }

            _context.Entry(lijst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LijstExists(id))
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

        // POST: api/Lijst
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lijst>> PostLijst(Lijst lijst)
        {
            lijst.Eigenaar = _context.Gebruikers.Find(lijst.Eigenaar.GebruikerID);

            _context.Lijsten.Add(lijst);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLijst", new { id = lijst.LijstID }, lijst);
        }

        // DELETE: api/Lijst/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lijst>> DeleteLijst(long id)
        {
            var lijst = await _context.Lijsten.FindAsync(id);
            if (lijst == null)
            {
                return NotFound();
            }

            _context.Lijsten.Remove(lijst);
            await _context.SaveChangesAsync();

            return lijst;
        }

        private bool LijstExists(long id)
        {
            return _context.Lijsten.Any(e => e.LijstID == id);
        }
    }
}
