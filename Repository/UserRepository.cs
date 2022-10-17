using BusinessManager.Model;
using Microsoft.EntityFrameworkCore;

using System.Linq;


namespace BusinessManager.Repository
{
    class UserRepository : Repository<User>
    {
        public override IQueryable<User> GetAllItems => GetAllItems.Include(o => o.Accounts).Include(o => o.LegalEntities);
        public UserRepository(ApplicationContext db) : base(db)
        {
        }

    }
}
