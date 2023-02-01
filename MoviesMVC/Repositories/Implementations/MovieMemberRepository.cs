namespace MoviesMVC.Repositories.Implementations
{
    public class MovieMemberRepository : GenericRepository<MovieMember>, IMovieMemberRepository
    {
        public MovieMemberRepository(AppDbContext context) : base(context) { }

        // This method uses Imperative -Programming- Code
        public async Task EditMovieMembersAsync(IEnumerable<MovieMember>? oldMovieMembers, IEnumerable<CheckboxVM>? allMovieMembers, int movieId)
        {
            foreach (var newMovieMember in allMovieMembers!)
            {
                var oldMovieMember = oldMovieMembers!.FirstOrDefault(x => x.MemberId == newMovieMember.ItemId);
                if (oldMovieMember is not null && !newMovieMember.IsSelected)
                    _context.MovieMembers!.Remove(oldMovieMember);

                if (oldMovieMember is null && newMovieMember.IsSelected)
                    await AddToMovieAsync(movieId, newMovieMember.ItemId);
            }

            // Other Imperative code
            // foreach (var allMovieMember in allMovieMembers!)
            // {
            //     bool isMemberExist = false;
            //     foreach (var oldMovieMember in oldMovieMembers!)
            //     {
            //         if (oldMovieMember.MemberId == allMovieMember.ItemId)
            //         {
            //             if (!allMovieMember.IsSelected) _context.MovieMembers!.Remove(oldMovieMember);
            //             isMemberExist = true;
            //         }
            //     }
            //     if (allMovieMember.IsSelected && !isMemberExist)
            //         await _context.MovieMembers!.AddAsync(new MovieMember
            //         {
            //             MovieId = movieId,
            //             MemberId = allMovieMember.ItemId
            //         });
            // }

        }

        public async Task AddToMovieAsync(int movieId, int memberId)
        {
            await _context.MovieMembers!.AddAsync(new MovieMember
            {
                MovieId = movieId,
                MemberId = memberId
            });
        }

        public async Task AddMovieMembersStringAsync(string members, int movieId)
        {
            bool hadDirector = false;
            foreach (var memberName in members.Split(',').Distinct())
            {
                var member = await _context.Members!.FirstOrDefaultAsync(m => m.MemberName.ToLower() == memberName.Trim().ToLower());
                if (member is not null)
                {
                    if (member.MemberRole == MemberRoles.Director && hadDirector) continue;
                    else
                    {
                        if (member.MemberRole == MemberRoles.Director) hadDirector = true;
                        await _context.MovieMembers!.AddAsync(new MovieMember { MovieId = movieId, MemberId = member.MemberId });
                    }
                }
            }
        }
    }
}