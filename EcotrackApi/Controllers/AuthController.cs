using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using EcotrackApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System;
using AutoMapper;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;

namespace EcotrackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:  MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public AuthController(SignInManager<IdentityUser> signInManager,
                                IClienteRepository clienteRepository,
                                IClienteService clienteService,
                                INotificador notificador,
                                UserManager<IdentityUser> userManager,
                                IOptions<JwtSettings> jwtSettings,
                                IMapper mappper) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mappper;
            _jwtSettings = jwtSettings.Value;
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(RegisterClienteViewModel registerCliente)
        {
                    // Validação inicial do ModelState
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            // Cria um novo IdentityUser para o ASP.NET Identity
            var user = new IdentityUser
            {
                UserName = registerCliente.Email, 
                Email = registerCliente.Email,
                EmailConfirmed = true
            };

            // Cria o usuário no Identity com a senha fornecida
            var result = await _userManager.CreateAsync(user, registerCliente.Senha);

            // Adiciona o cliente no banco usando o serviço de cliente
            var cliente = _mapper.Map<Cliente>(registerCliente);
            var clienteAdicionado = await _clienteService.Adicionar(cliente);

            if (!clienteAdicionado)
            {
                // Se a adição do cliente falhar, notifica o erro
                NotificarErro("Erro ao adicionar o cliente ao banco de dados.");
                return CustomResponse();
            }

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(GerarJwt());

            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginClienteViewModel loginCliente)
        {
            if(ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginCliente.Email, loginCliente.Password, false, true);

            if(result.Succeeded)
            {
                return Ok(GerarJwt());
            }

            return Problem();
        }

        [NonAction]
        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
    
            return encodedToken;
        }
    }
}