using Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbInitiallize
{
    public static class DbInitializer
    {
        public static void Initialize(BuildingConstructDbContext context)
        {
            //context.Database.Migrate();
            // Add Seed Data...
        }
    }
}
