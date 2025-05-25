using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace PharmacyWebApp.Pages.Medicines
{
    public class UpdateModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        public UpdateModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [BindProperty]
        [Required]
        public MedicineInformation Medicine { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (HttpContext.Session.GetString("Role") != "2") //Manager only
                    {
                ErrorMessage = "You do not have permission to do this function!";	
                return Page();
            }

            Medicine = await _medicineService.GetMedicineByIdAsync(id);
            if (Medicine == null)
            {
                return NotFound();
            }

            Manufacturers = await _medicineService.GetManufacturersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("Role") != "2")
            {
                ErrorMessage = "You do not have permission to do this function!";	
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Manufacturers = await _medicineService.GetManufacturersAsync();
                return Page();
            }

            try
            {
                await _medicineService.UpdateMedicineAsync(Medicine);
                return RedirectToPage("/Medicines/Index");
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
                Manufacturers = await _medicineService.GetManufacturersAsync();
                return Page();
            }
        }
    }
}