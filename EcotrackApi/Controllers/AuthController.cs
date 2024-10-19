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
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using IEmailSender = EcotrackBusiness.Interfaces.IEmailSender;
using Microsoft.AspNetCore.Http;

namespace EcotrackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IEmailSender _emailsender;
        
        public AuthController(SignInManager<IdentityUser> signInManager,
                                IClienteRepository clienteRepository,
                                IClienteService clienteService,
                                INotificador notificador,
                                IEmailSender emailSender,
                                UserManager<IdentityUser> userManager,
                                IOptions<JwtSettings> jwtSettings) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _emailsender = emailSender;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(RegisterClienteViewModel registerCliente)
        {
            // Verifica se as senhas coincidem
            if (registerCliente.Senha != registerCliente.ConfirmarSenha) return CustomResponse();

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
                return CustomResponse(ModelState);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
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

        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> EsqueceuSenha(ForgotPasswordViewModel forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                return CustomResponse();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    
            var sendEmail = await _emailsender.EnviarEmail(forgotPassword.Email, token);

            if (!sendEmail)
            {
                return CustomResponse();
            }

            return Ok("Um link para redefinir a senha foi enviado para o e-mail.");
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RestaurarSenha(ResetPasswordViewModel ResetPassword)
        {
            // Verifica se o user existe
            var user = await _userManager.FindByEmailAsync(ResetPassword.Email);
            if (user == null)
            {
                return NotFound();
            }

            // Tenta redefinir a senha com o token recebido
            var result = await _userManager.ResetPasswordAsync(user, ResetPassword.Token, ResetPassword.Senha);
            if (result.Succeeded)
            {
                return Ok();
            }

            return CustomResponse(result.Errors);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ObterPorEmail(string email)
        {
            // Verifica se o cliente existe
            var cliente = await _clienteRepository.ObterClientePorEmail(email);

            if(cliente == null) return NotFound();

            return Ok(cliente);
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

            // Retorna o Json Web Token
            return encodedToken;
        }
    }
}