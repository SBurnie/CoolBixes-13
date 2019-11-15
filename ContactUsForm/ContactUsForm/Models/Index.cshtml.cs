using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactUsForm.Models
{
    public class IndexModel : PageModel
    {
        private readonly ContactUsForm.Models.ContactUsFormContext _context;

        public IndexModel(ContactUsForm.Models.ContactUsFormContext context)
        {
            _context = context;
        }

        public IList<Form> Form { get;set; }

        public async Task OnGetAsync()
        {
            Form = await _context.Form.ToListAsync();
        }
    }
}
