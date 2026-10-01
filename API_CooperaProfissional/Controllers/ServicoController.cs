using API_CooperaProfissional.Data;
using API_CooperaProfissional.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_CooperaProfissional.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private dbCooperaEntities db;

        public ServicoController(dbCooperaEntities _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<Servicodto> Get()
        {
            var list = new List<Servicodto>();

            var b = db.Servicos.Select(i => new { NomeServico = i.NomeServico }).ToList();

            foreach(var i in b)
            {
                var s = new Servicodto()
                {
                    NomeServico = i.NomeServico
                };

                list.Add(s);
            }

            return list;
        }
    }
}
