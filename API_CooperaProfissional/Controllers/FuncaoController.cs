using API_CooperaProfissional.Data;
using API_CooperaProfissional.DTOs;
using API_CooperaProfissional.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_CooperaProfissional.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncaoController : ControllerBase
    {
        private dbCooperaEntities db;
        
        public FuncaoController(dbCooperaEntities _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<FuncaoListadto> Get()
        {
            var list = new List<FuncaoListadto>();

            var b = db.Funcaos.Select(i => new
            {
                Id = i.IdFuncao,
                Nome = i.NomeFuncao
            }).ToList();

            foreach (var i in b)
            {
                var func = new FuncaoListadto()
                {
                    Id = i.Id,
                    Nome = i.Nome
                };

                list.Add(func);
            }

            return list;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            var b = db.Funcaos.FirstOrDefault(i => i.IdFuncao == id);
            return b.NomeFuncao;
        }
    }
}
