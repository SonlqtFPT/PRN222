using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PharmacyWebApp.Services.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineInformationRepository _repository;
 public MedicineService(IMedicineInformationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MedicineInformation>> GetMedicinesAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllWithManufacturerAsync(
           pageNumber, pageSize);
        }
        public async Task<int> GetTotalMedicineCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }

        public async Task AddMedicineAsync(MedicineInformation medicine)
        {
            if (!IsValidActiveIngredients(medicine.ActiveIngredients))
                throw new ArgumentException("Invalid ActiveIngredients");
            await _repository.AddAsync(medicine);
        }
        public async Task<MedicineInformation> GetMedicineByIdAsync(string medicineId)
        {
            return await _repository.GetByIdAsync(medicineId);
        }	

        public async Task UpdateMedicineAsync(MedicineInformation medicine)
        {
            if (!IsValidActiveIngredients(medicine.ActiveIngredients))
                throw new ArgumentException("Invalid ActiveIngredients");
            await _repository.UpdateAsync(medicine);
        }	

        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            return await _repository.GetAllManufacturersAsync();
        }
        public bool IsValidActiveIngredients(string ingredients)
        {
            if (string.IsNullOrEmpty(ingredients) || ingredients.Length <= 10)
                return false;
            if (ingredients.Any(c => "#@&()".Contains(c)))
                return false;
            var words = ingredients.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.All(word => char.IsUpper(word[0]) || char.IsDigit(word[0]));
        }
    }
}