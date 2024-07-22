using CatalogDb.API.DTOs;
using CatalogDb.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; }

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await AuthService.CreateRoleAsync(roleName);

            return StatusCode(StatusCodes.Status201Created, new ResponseDTO(HttpStatusCode.Created, $"Role '{roleName}' added successfully!"));
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            await AuthService.AddUserToRoleAsync(email, roleName);

            return StatusCode(StatusCodes.Status201Created, new ResponseDTO(HttpStatusCode.Created, $"User '{email}' added to the '{roleName}' role!"));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            AuthResponseDTO result = await AuthService.LoginAsync(loginDTO);

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await AuthService.RegisterAsync(registerDTO);

            return StatusCode(StatusCodes.Status201Created, new ResponseDTO(HttpStatusCode.Created, "User created successfully!"));
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenDTO tokenDto)
        {
            AuthResponseDTO result = await AuthService.RefreshTokenAsync(tokenDto);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Revoke/{userName}")]
        public async Task<IActionResult> Revoke(string userName)
        {
            await AuthService.RevokeAsync(userName);

            return NoContent();
        }
    }
}
