using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUser _userService;
        private readonly AppSettings _appSettings;
        private IAutoMapConverter<UserDto, UserBO> mapDtoToBo;
        private IMapper _mapper;

        public UsersController(IUser userService, IOptions<AppSettings> appSettings, IAutoMapConverter<UserDto, UserBO> convertDtoToBo, IMapper mapper)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            mapDtoToBo = convertDtoToBo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Los datos no pueden ser nulos" });
            }

            var userModel = mapDtoToBo.ConvertObject(model);

            if (_userService.validarUser(userModel))
            {
                return BadRequest(new { message = "Los datos no pueden ser nulos" });
            }

            var user = _userService.Authenticate(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username o password son incorrectos" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new { message = "Los datos no pueden ser nulos" });
            }

            var user = mapDtoToBo.ConvertObject(userDto);

            if (_userService.validarUser(user))
            {
                return BadRequest(new { message = "Los datos no pueden ser nulos" });
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new Helpers.AppException("Password es requerido");
            
            try
            {
                // guardar 
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //List<UserDto> userDtos = new List<UserDto>();

            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new { message = "Los datos no pueden ser nulos" });
            }
            
            // map dto to entity and set id
            var user = _mapper.Map<UserBO>(userDto);
            user.Id = id;

            try
            {
                // guardar 
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            return Ok();
        }

    }
}
