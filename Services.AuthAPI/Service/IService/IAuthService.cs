using Services.AuthAPI.Models.Dto;

namespace Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Registration(RegistrationRequestDTO registrationRequestDTO);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
    }
}
