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
using Microsoft.AspNetCore.Identity.UI.Services;

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
        
        public AuthController(SignInManager<IdentityUser> signInManager,
                                IClienteRepository clienteRepository,
                                IClienteService clienteService,
                                INotificador notificador,
                                IEmailSender emailSender,
                                UserManager<IdentityUser> userManager,
                                IOptions<JwtSettings> jwtSettings,
                                IMapper mappper) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(RegisterClienteViewModel registerCliente)
        {
            // Verifica se o model é válido
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Verifica se as senhas coincidem
        if (registerCliente.Senha != registerCliente.ConfirmarSenha)
            return BadRequest("As senhas não coincidem");

        // Cria um novo IdentityUser
        var user = new IdentityUser
        {
            UserName = registerCliente.Email,
            Email = registerCliente.Email
        };

        // Cria o usuário no Identity
        var result = await _userManager.CreateAsync(user, registerCliente.Senha);
        if (!result.Succeeded)
        {
            // Se houver falha, retorna os erros
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        // Adiciona o cliente ao banco de dados
        var cliente = new Cliente
        {
            Nome = registerCliente.Nome,
            Email = registerCliente.Email,
            Cpf = registerCliente.Cpf
        };

        await _clienteRepository.Adicionar(cliente);
        await _clienteRepository.SaveChanges();

        // Faz o login automático do usuário
        await _signInManager.SignInAsync(user, false);

        return Ok(GerarJwt());
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginClienteViewModel loginCliente)
        {
            var result = await _signInManager.PasswordSignInAsync(loginCliente.Email, loginCliente.Password, false, true);

            if (!result.Succeeded)
            {
                return Unauthorized("Usuário ou senha incorretos.");
            }

            var user = await _userManager.FindByEmailAsync(loginCliente.Email);

            // Gera o token JWT e retorna
            return Ok(new { Token = GerarJwt() });
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