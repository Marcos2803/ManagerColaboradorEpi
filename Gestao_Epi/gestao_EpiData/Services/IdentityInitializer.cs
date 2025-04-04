using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Entities.Enumerables;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace gestao.EpiData.Services
{
    public static class IdentityInitializer
    {
        public static async Task CriarRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Create" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task CriarUsuarioAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            string matriculaAdmin = "6982149";
            string senhaAdmin = "@#45eDSWDgbnj68588"; 
            string nomeAdmin = "Admin Usuário"; 
            string emailAdmin = "admin1@example.com"; 

            var user = await userManager.FindByEmailAsync(emailAdmin);

            if (user == null)
            {
                user = new User
                {
                    UserName = matriculaAdmin,  
                    Email = emailAdmin,         
                    Matricula = matriculaAdmin, 
                    NomeCompleto = nomeAdmin,   
                    CreatedDate = DateTime.Now,
                    StatusUser = StatusUserEnum.Ativo 
                };
                var result = await userManager.CreateAsync(user, senhaAdmin);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

    }
}

