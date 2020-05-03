using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data.Helpers;
using Soccer.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess
{
    public class UserHelper : IUserHelper  /*la clase UserHelper va a ser la unica que va a inyectar el UserManager*/
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task AddRoleToUserAsync(ApplicationUser user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task CheckRoleAsync(string v)
        {
            var roleExist = await this.roleManager.RoleExistsAsync(v);

            if (!roleExist)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = v
                });
            }

        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }

        //public async Task<SignInResult> LoginAsync(LoginViewModel loginViewModel)
        //{
        //    /*en este caso el parametro false significa que no nay bloqueo en caso de equivocarse al digitar mal varias veces el password o usuario*/
        //    return await this.signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
        //}

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password)
        {
            /*Similar al meotdo LoginAsync anterior, en cuanto al parametro false*/
            /*en este caso el parametro false significa que no nay bloqueo en caso de equivocarse al digitar mal varias veces el password o usuario*/
            return await this.signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return await this.userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password) /*se pasa el usuario, el token y el nuevo password*/
        {
            return await this.userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            /*en linq todo se mapea internamente como un consulta de base de datos, por eso no se hace un ordentamiento de FullName (que es de lectura), porque esa
            columna no existe en la tabla Users, debe hacerse con las columnas existentes*/
            return await this.userManager.Users
                .OrderBy(u => u.Nombre)
                .ThenBy(u => u.Apellidos) /*ordena primero por nombre y luego por apellido, por ejm si hay Juan Mora y Juan Morales, ordenad primero a Juan Mora*/
                .ToListAsync();
        }

        public async Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName)
        {
            await this.userManager.RemoveFromRoleAsync(user, roleName); /*quitarle un rol determinado a un usuario*/
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await this.userManager.DeleteAsync(user);
        }

        /*Metodos de bloqueo y desbloqueo*/

        public async void BloquearUsuario(string idUsuario)
        {
            var usuarioDesdeBD = await GetUserByEmailAsync(idUsuario);

            usuarioDesdeBD.LockoutEnd = DateTime.Now.AddYears(100);

            /*hay que colocar codigo para guardar cambios*/

        }

        public async void DesbloquearUsuario(string idUsuario)
        {
            var usuarioDesdeBD = await GetUserByEmailAsync(idUsuario);

            usuarioDesdeBD.LockoutEnd = DateTime.Now;

            /*hay que colocar codigo para guardar cambios*/

        }

    }
}
