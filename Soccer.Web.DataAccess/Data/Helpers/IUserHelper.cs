using Microsoft.AspNetCore.Identity;
using Soccer.Web.Models.Entities;
using Soccer.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Helpers
{
    public interface IUserHelper
    {

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password); /*devuelve un IdentityResult, que permite indicar el porque se pudo o no realizar 
        el agregado del usuario*/

        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password); /*este metodo no loguea, sino lo que permite es indicar si los parametros que se pasan son validos para loguearse, es como un punto intermedio*/
        Task CheckRoleAsync(string roleName);

        Task AddRoleToUserAsync(ApplicationUser user, string roleName);

        Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user); /*generar el token de confirmacion para un usuario, se envia el usuario, y devuelve el token en un string*/

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token); /*confirmacion, se envia el usuario y el token especificado*/

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password);

        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName);  /*le quita el permiso a un usuario, por ejm si es administrador, se le puede quitar ese privilegio*/

        Task DeleteUserAsync(ApplicationUser user);  /*permite borrar usuarios*/

        /*Metodos de bloqueo y desbloqueo*/
        void BloquearUsuario(string idUsuario);

        void DesbloquearUsuario(string idUsuario);
    }
}
