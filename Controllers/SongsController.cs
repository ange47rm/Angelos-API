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
    [Route("api/songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicDbContext _dbContext;

        public SongsController(MusicDbContext musicDbContext)
        {
            _dbContext = musicDbContext;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDto>>> GetSongs()
        {
          if (_dbContext.Songs == null)
          {
              return NotFound();
          }
            return await _dbContext.Songs.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDto>> GetSong(int id)
        {
          if (_dbContext.Songs == null)
          {
              return NotFound();
          }
            var song = await _dbContext.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDto song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(song).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SongDto>> PostSong(SongDto song)
        {
          if (_dbContext.Songs == null)
          {
              return Problem("Entity set 'MusicDbContext.Songs'  is null.");
          }
            _dbContext.Songs.Add(song);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_dbContext.Songs == null)
            {
                return NotFound();
            }
            var song = await _dbContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _dbContext.Songs.Remove(song);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_dbContext.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
