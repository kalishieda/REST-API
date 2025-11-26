using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Repository;
using System.ComponentModel.DataAnnotations;

namespace REST_API.Controllers
{
    [Route("paises/{idPais}/estados/{idEstado}/cidades")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Cidade>> GetCidades(
            [FromQuery] string? nome,
            [FromQuery, Range(7000, 53000)] int fromPopulacao,
            [FromRoute, Required] int idPais,
            [FromRoute, Required] int idEstado
            )
        {
            var resultado = CidadesRepository.Cidades.Where(cidades => cidades.IdPais == idPais &&
                cidades.IdEstado == idEstado).ToList();

            if (resultado.Count == 0) return NotFound(null);

            if (!string.IsNullOrEmpty(nome))
            {
                resultado = resultado.Where(cidade => cidade.Nome == nome).ToList();
            }

            if (fromPopulacao > 0)
            {
                resultado = resultado.Where(cidade => cidade.Populacao >= fromPopulacao).ToList();
            }


            return Ok(resultado);
        }

        [HttpPost]
        public ActionResult PostCidades(
            [FromRoute, Required] int idPais,
            [FromRoute, Required] int idEstado,
            [FromBody] Cidade cidade
            )
        {
            cidade.IdEstado = idEstado;
            cidade.IdPais = idPais;
            CidadesRepository.Cidades.Add(cidade);
            CidadesRepository.Save();

            string locationUrl = $"/paises/{{idPais}}/estados/{{idEstado}}/cidades/{cidade.Id}";

            return Created(locationUrl, null);
        }
    }
}
