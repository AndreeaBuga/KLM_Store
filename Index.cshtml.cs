using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KLM_Store.Data;
using KLM_Store.Models;
using System.Security.Policy;

namespace KLM_Store.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly KLM_Store.Data.KLM_StoreContext _context;

        public IndexModel(KLM_Store.Data.KLM_StoreContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get;set; } = default!;
        public BrandIndexData BrandData { get; set; }
        public int BrandID { get; set; }
        public int ProdusID { get; set; }

        public async Task OnGetAsync(int? id, int? produsID)
        {
            BrandData = new BrandIndexData();
            BrandData.Brands = await _context.Brand
           .Include(i => i.Produse)
     //      .ThenInclude(c => c.Produs)
           .OrderBy(i => i.BrandName)
           .ToListAsync();
            if (id != null)
            {
                BrandID = id.Value;
                Brand brand = BrandData.Brands
               .Where(i => i.ID == id.Value).Single();
                BrandData.Produse = brand.Produse;
            }
        }
    }
}
