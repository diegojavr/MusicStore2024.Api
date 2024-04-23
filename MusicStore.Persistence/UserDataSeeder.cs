using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Persistence
{
    public class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            //Repositorio de usuarios
            //instancia de user manager
            var userManager = service.GetRequiredService<UserManager<MusicStoreUserIdentity>>();

            //Repositorio de roles
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            //Crear roles
            var adminRole = new IdentityRole(Constantes.RolAdmin);
            var customerRole = new IdentityRole(Constantes.RolCustomer);

            //Si no existe rol de admin entonces lo crea
            if (!await roleManager.RoleExistsAsync(Constantes.RolAdmin))
                await roleManager.CreateAsync(adminRole);

            if (!await roleManager.RoleExistsAsync(Constantes.RolCustomer))
                await roleManager.CreateAsync(customerRole);

            var adminUser = new MusicStoreUserIdentity
            {
                FirstName = "Administrador",
                LastName = "del sistema",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Age = 26,
                DocumentType = DocumentTypeEnum.Dni,
                DocumentNumber = "300140657",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser,"Admin1234*");
            if (result.Succeeded)
            {
                //Esto me asegura que el usuario se creó correctamente
                adminUser=await userManager.FindByEmailAsync(adminUser.Email);
                if(adminUser!=null)
                {
                    //asigna rol de admin al usuario recien creado
                    await userManager.AddToRoleAsync(adminUser,Constantes.RolAdmin);
                }
            }
        }
    }
}
