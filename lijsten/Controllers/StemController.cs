using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lijsten.Models;
using lijsten.Models;

namespace lijsten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StemController : ControllerBase
    {
        private readonly DataContext _context;

        public StemController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Stem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmen()
        {
            return await _context.Stemmen.ToListAsync();
        }
        
        // GET: api/Stem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stem>> GetStem(long id)
        {
            var stem = await _context.Stemmen.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }
        // GET: api/Stem/gebruiker/5
        [HttpGet("gebruiker/{id}")]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmenWhereGebruiker(long id)
        {
            var stemmen = await _context.Stemmen.Where(s => s.Gebruiker.GebruikerID == id).ToListAsync();

            if (stemmen == null)
            {
                return NotFound();
            }

            return stemmen;
        }

        // GET: api/Stem/itemID/gebruikerID
        [HttpGet("{itemID}/{gebruikerID}")]
        public async Task<ActionResult<Boolean>> GetExistence(long itemId, long gebruikerId)
        {
            var stem = await _context.Stemmen.Where(s => s.Gebruiker.GebruikerID == gebruikerId && s.Item.ItemID == itemId).FirstOrDefaultAsync();

            if (stem == null)
            {
                return false;
            }

            return true;
        }

        // GET: api/Stem/stem/itemID/gebruikerID
        [HttpGet("stem/{itemID}/{gebruikerID}")]
        public async Task<ActionResult<Stem>> GetExistenceStem(long itemId, long gebruikerId)
        {
            var stem = await _context.Stemmen.Where(s => s.Gebruiker.GebruikerID == gebruikerId && s.Item.ItemID == itemId).FirstOrDefaultAsync();

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }

        [HttpGet("item/{id}")]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmenWhereItem(long id)
        {
            var stemmen = await _context.Stemmen.Where(s => s.Item.ItemID == id).ToListAsync();

            if (stemmen == null)
            {
                return NotFound();
            }

            return stemmen;
        }

        // PUT: api/Stem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem(long id, Stem stem)
        {
            if (id != stem.StemID)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Stem>> PostStem(Stem stem)
        {
            stem.Gebruiker = _context.Gebruikers.Find(stem.Gebruiker.GebruikerID);
            stem.Item = _context.Items.Find(stem.Item.ItemID);
            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemID }, stem);
        }

        // DELETE: api/Stem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stem>> DeleteStem(long id)
        {
            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return stem;
        }

        private bool StemExists(long id)
        {
            return _context.Stemmen.Any(e => e.StemID == id);
        }
    }
}
