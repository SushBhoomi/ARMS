using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Core.Entities;
using ARMS.WebAPI.Models;
using System.Collections;
using System.Security.Claims;

namespace ARMS.WebAPI.Services.Interfaces
{
    public interface ILdapAuhenticationService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<string> GetJwtTokenAsync(string username);
        string CreateRefreshToken();
        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
        Task<ActivityLogModel> CreateActivityLog(string Activity, string username,int id);
        
    }
}
