namespace MoviesMVC.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<MovieGenre>? MovieGenres { get; set; }
        public DbSet<MovieMember>? MovieMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MovieGenre>()
              .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<MovieGenre>()
              .HasOne(x => x.Genre)
              .WithMany(x => x.Movies)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MovieGenre>()
              .HasOne(x => x.Movie)
              .WithMany(x => x.Genres)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MovieMember>()
              .HasKey(x => new { x.MovieId, x.MemberId });

            builder.Entity<MovieMember>()
              .HasOne(x => x.Member)
              .WithMany(x => x.Movies)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MovieMember>()
              .HasOne(x => x.Movie)
              .WithMany(x => x.Members)
              .OnDelete(DeleteBehavior.Cascade);
        }

    }
}