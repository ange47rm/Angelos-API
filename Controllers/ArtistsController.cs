using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngelosAPI.Dtos;

namespace AngelosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicDbContext _dbContext;

        public ArtistsController(MusicDbContext musicDbContext)
        {
            _dbContext = musicDbContext;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists()
        {
          if (_dbContext.Artists == null)
          {
              return NotFound();
          }
            return await _dbContext.Artists.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
          if (_dbContext.Artists == null)
          {
              return NotFound();
          }
            var artist = await _dbContext.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistDto artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(artist).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtistDto>> PostArtist(ArtistDto artist)
        {
          if (_dbContext.Artists == null)
          {
              return Problem("Entity set 'MusicDbContext.Artists'  is null.");
          }
            _dbContext.Artists.Add(artist);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetArtistDto", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (_dbContext.Artists == null)
            {
                return NotFound();
            }
            var artistDto = await _dbContext.Artists.FindAsync(id);
            if (artistDto == null)
            {
                return NotFound();
            }

            _dbContext.Artists.Remove(artistDto);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return (_dbContext.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
