using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUsuario.Context;
using ApiUsuario.Models;
using NuGet.Common;
using System.Text.RegularExpressions;

namespace ApiUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _passwordPattern;

        public UsersController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _passwordPattern = configuration["PasswordRegex"];
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //Expresion regular para validad el formato del correo.
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.cl$";
            if (!Regex.IsMatch(user.Email, emailPattern))
            {
                return BadRequest(new { mensaje = "El correo no tiene un formato valido" });
            }
            //Verifica si el Correo ya existe en la base de datos
            if(_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest(new { mensaje = "El correo ya registrado" });
            }

            //Validacion de la contraseña con una expresion regular configurable
            if(!Regex.IsMatch(user.Password, _passwordPattern))
            {
                return BadRequest(new { mensaje = "la contraseña no cumple con el formato requerido" });
            }

            //Asigna valores al nuevo usuario
            user.Id = Guid.NewGuid();
            user.Created = DateTime.UtcNow;
            user.Modified = DateTime.UtcNow;
            user.LastLogin = DateTime.UtcNow;
            user.Token = Guid.NewGuid();
            user.IsActive = true;

            //Guarda el usuario en la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //Estructura la respuesta para ajustarse al formato deseado

            var response = new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                created = user.Created,
                modified = user.Modified,
                lastlogin = user.LastLogin,
                token = user.Token,
                isActive = user.IsActive,
                phone = user.Phones.Select(phone => new
                {
                    number = phone.Number,
                    cityCode = phone.CityCode,
                    countryCode = phone.CountryCode,
                }).ToList()
            };

            return CreatedAtAction("GetUser", new { id = user.Id }, response);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
