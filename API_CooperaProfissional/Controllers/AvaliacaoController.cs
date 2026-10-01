using API_CooperaProfissional.Data;
using API_CooperaProfissional.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_CooperaProfissional.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private dbCooperaEntities db = new dbCooperaEntities();

        public AvaliacaoController(dbCooperaEntities _db)
        {
            db = _db;
        }

        // RETORNA A MEDIA JA ARREDONDADA DE AVALIACAO DE UM PROFISSIONAL
        [HttpGet("usuario/{idProfissional}")]
        public IActionResult GetAvaliacaoProfissional(int idProfissional)
        {
            int media = (int)Math.Round(db.Avaliacoes.Where(i => i.IdUsuario == idProfissional).Select(i => (double?)i.QtEstrelas).Average() ?? 0,MidpointRounding.AwayFromZero);

            return Ok(media);
        }

        // REGISTRA UMA AVALIACAO
        [HttpPost("avaliar/{idProfissional}/{nomeAvaliador}/{qtEstrelas}")]
        public IActionResult PostAvaliar(int idProfissional, string nomeAvaliador, int qtEstrelas)
        {
            var avaliacao = new Avaliaco()
            {
                NomeAvaliador = nomeAvaliador,
                IdUsuario = idProfissional,
                QtEstrelas = qtEstrelas
            };

            db.Avaliacoes.Add(avaliacao);
            db.SaveChanges();

            return Ok();
        }
    }
}
