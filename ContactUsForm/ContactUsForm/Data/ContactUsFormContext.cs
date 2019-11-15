using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactUsForm.Models
{
    public class ContactUsFormContext : DbContext
    {
        public ContactUsFormContext (DbContextOptions<ContactUsFormContext> options)
            : base(options)
        {
        }

        public DbSet<ContactUsForm.Models.Form> Form { get; set; }
    }
}
