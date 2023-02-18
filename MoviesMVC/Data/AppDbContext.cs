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
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Message>? Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Genre - movie Relationship
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

            // Cast(Members) of movie Relationship
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

            // Orders Relationship
            builder.Entity<Order>()
              .HasKey(x => new { x.MovieId, x.UserId });

            builder.Entity<Order>()
              .HasOne(x => x.User)
              .WithMany(x => x.Orders)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
              .HasOne(x => x.Movie)
              .WithMany(x => x.Orders)
              .OnDelete(DeleteBehavior.Cascade);

            // Messages Relationship
            builder.Entity<Message>()
              .HasOne(message => message.Sender)
              .WithMany(user => user.MessagesSent)
              .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Message>()
              .HasOne(message => message.Recipient)
              .WithMany(user => user.MessagesReceived)
              .OnDelete(DeleteBehavior.SetNull);

        }

    }
}