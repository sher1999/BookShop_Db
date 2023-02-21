using System.Text;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.SeedData;

public static  class SeedData
{
    public static void Seed(DataContext context)
    {
        if(context.Roles.Any()) return;
        var roles = new List<IdentityRole>()
        {
            new IdentityRole(Roles.Admin){NormalizedName=Roles.Admin.ToUpper()},
            new IdentityRole(Roles.Customer){NormalizedName=Roles.Customer.ToUpper()},
  
        };
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
    
}