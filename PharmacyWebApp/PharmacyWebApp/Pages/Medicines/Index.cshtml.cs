using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using X.PagedList;
namespace PharmacyWebApp.Pages.Medicines
{
    public class IndexModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        public IndexModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        public IPagedList<MedicineInformation> MedicinesPaged
        {
            get; set;
        }
        public List<MedicineInformation> Medicines { get; set; }
        public async Task OnGetAsync(int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 3;
            Medicines = await _medicineService.GetMedicinesAsync(
           page, pageSize);
            MedicinesPaged = new StaticPagedList<MedicineInformation>(Medicines, page, pageSize, await _medicineService.GetTotalMedicineCountAsync());
        }
    }
}