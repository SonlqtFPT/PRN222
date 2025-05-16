using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Data.Models;
using System.Threading.Tasks;
namespace PharmacyWebApp.Data.Repositories
{
    public class StoreAccountRepository : IStoreAccountRepository
    {
        private readonly Lab2PharmaceuticalDBContext _context;
        public StoreAccountRepository(Lab2PharmaceuticalDBContext context)
        {
            _context = context;
        }
        public async Task<StoreAccount> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.StoreAccounts
            .FirstOrDefaultAsync(a => a.EmailAddress == email
           && a.StoreAccountPassword == password);
        }
    }
}