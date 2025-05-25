using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PharmacyWebApp.Data.Repositories
{
    public class MedicineInformationRepository : IMedicineInformationRepository
    {
        private readonly Lab2PharmaceuticalDBContext _context;
        public MedicineInformationRepository(Lab2PharmaceuticalDBContext context)
        {
            _context = context;
        }
        public async Task<List<MedicineInformation>> GetAllWithManufacturerAsync(int pageNumber, int pageSize)
        {
            return await _context.MedicineInformations
            .Include(m => m.Manufacturer)
            .OrderBy(m => m.MedicineId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.MedicineInformations.CountAsync
           ();
        }

        public async Task AddAsync(MedicineInformation medicine)
        {
            _context.MedicineInformations.Add(medicine);
            await _context.SaveChangesAsync();
        }
        public async Task<MedicineInformation> GetByIdAsync(string medicineId)
        {
            return await _context.MedicineInformations.Include(m => m.Manufacturer).FirstOrDefaultAsync(m => m.MedicineId == medicineId);
        }	

        public async Task UpdateAsync(MedicineInformation medicine)
        {
            _context.MedicineInformations.Update(medicine);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }
    }
}