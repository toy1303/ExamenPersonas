using Microsoft.AspNetCore.Mvc;
using APIPersona.Modelo;
using APIPersona.DtoEntrada;
using APIPersona.DtoSalida;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace APIPersona.Controllers
{
    [Produces("application/json")]
    public class PersonaController : Controller
    {
        private readonly ExamenPersonasContext _ContextoBD;
        public PersonaController(ExamenPersonasContext examenPersonasContext)
        {
            _ContextoBD = examenPersonasContext;
        }

        [HttpGet]
        [Route("ObtienePersonas")]
        public IActionResult ObtienePersonas()
        {
            List<Persona> personaList = _ContextoBD.Personas.ToList();
            return Ok(personaList);
        }

        [HttpPost]
        [Route("agregapersona")]
        public IActionResult AgregaPersona([FromBody] DtoPersona dtoPersona)
        {
            Respuesta respuesta = new Respuesta();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    if (dtoPersona.Nombre.Length > 50)
                    {
                        respuesta.Mensaje = "El nombre tiene mas de 50 caracteres";
                        return Ok(respuesta);
                    }
                    if (!ValidaEmail(dtoPersona.Email))
                    {
                        respuesta.Mensaje = dtoPersona.Email + " No Tiene el formato Correcto";
                        return Ok(respuesta);
                    }
                    Persona persona = new Persona();
                    persona.Nombre= dtoPersona.Nombre;
                    persona.Edad= dtoPersona.Edad;
                    persona.Email= dtoPersona.Email;
                    _ContextoBD.Add(persona);
                    this._ContextoBD.SaveChanges();
                    respuesta.Mensaje = "Operación Exitosa";
                    return Ok(respuesta);

                }
                catch
                (Exception ex)
                {
                    respuesta.Mensaje =ex.Message;
                    respuesta.Datos = null;
                }
            }

            return Ok(respuesta);

        }

        [HttpPut]
        [Route("modificapersona")]
        public IActionResult ModificaPersona([FromBody] DtoPersona dtoPersona)
        {
            Respuesta respuesta = new Respuesta();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    if (dtoPersona.Nombre.Length > 50)
                    {
                        respuesta.Mensaje = "El nombre tiene mas de 50 caracteres";
                        return Ok(respuesta);
                    }
                    if (!ValidaEmail(dtoPersona.Email))
                    {
                        respuesta.Mensaje = dtoPersona.Email + " No Tiene el formato Correcto";
                        return Ok(respuesta);
                    }


                    Persona persona = this._ContextoBD.Personas.Where(m => m.Id == dtoPersona.Id).FirstOrDefault();
                    persona.Nombre = dtoPersona.Nombre;
                    persona.Edad = dtoPersona.Edad;
                    persona.Email = dtoPersona.Email;
                    this._ContextoBD.Entry(persona).State = EntityState.Modified;
                    this._ContextoBD.SaveChanges();
                    respuesta.Mensaje = "Operación Exitosa";
                    return Ok(respuesta);

                }
                catch
                (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;
                    respuesta.Datos = null;
                }
            }

            return Ok(respuesta);

        }

        [HttpDelete]
        [Route("eliminapersona/{id}")]
        public IActionResult Eliminapersona(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Persona p = this._ContextoBD.Personas.Where(m=>m.Id== id).FirstOrDefault();
                if (p != null)
                {
                    this._ContextoBD.Personas.Remove(p);
                    this._ContextoBD.SaveChanges();
                    respuesta.Mensaje = "Operación Exitosa";
                }
                else
                {
                    respuesta.Mensaje = "No se encontró la persona a eliminar";
                    respuesta.Datos = null;
                }

            }
            catch (Exception ex)
            {
                respuesta.Mensaje += ex.Message;
                respuesta.Datos = null;
            }

            return Ok(respuesta);
        }

        private bool ValidaEmail(string Email)
        {
            bool valid = false;
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false; // Si el email está vacío o solo tiene espacios, no es válido.
            }

            // Expresión regular para validar el formato del correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            // Retorna true si el email coincide con el patrón, de lo contrario false.
            valid= regex.IsMatch(Email);

            return valid;
        }

    }
}
