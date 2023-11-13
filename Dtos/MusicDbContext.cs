using Microsoft.EntityFrameworkCore;

namespace AngelosAPI.Dtos
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }

        public DbSet<ArtistDto> Artists { get; set; }

        public DbSet<SongDto> Songs { get; set; }
    }
}
