using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesMVC.Repositories.Implementations
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext context) : base(context) { }
    }
}