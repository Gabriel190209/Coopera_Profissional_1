using API_CooperaProfissional.Data;
using API_CooperaProfissional.DTOs;
using API_CooperaProfissional.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace API_CooperaProfissional.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private dbCooperaEntities db;

        public UsuarioController(dbCooperaEntities _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<Usuariodto> Get()
        {
            var list = new List<Usuariodto>();

            var b = db.Usuarios.Where(i => i.IdServicoNavigation != null).Select(i => new
            {
                ID = i.IdUsuario,
                Nome = i.NomeCompleto,
                Email = i.Email,
                Descricao = i.Descricao,
                Telefone = i.Telefone,
                Servico = i.IdServicoNavigation.NomeServico,
                FuncaoUsuario = i.IdFuncaoNavigation.NomeFuncao
            }).ToList();

            foreach (var i in b)
            {
                int media = (int)Math.Round(db.Avaliacoes.Where(j => j.IdUsuario == i.ID).Select(j => (double?)j.QtEstrelas).Average() ?? 0, MidpointRounding.AwayFromZero);

                var u = new Usuariodto()
                {
                    ID = i.ID,
                    NomeCompleto = i.Nome,
                    Email = i.Email,
                    Descricao = i.Descricao,
                    Telefone = i.Telefone,
                    Estrelas = media,
                    Servico = i.Servico,
                    FuncaoUsuario = i.FuncaoUsuario
                };

                list.Add(u);
            }

            return list;
        }

        //serve para a pesquisa (ainda só esta funcionando com o nome, da uma olhada no maui na função filtro_changed na pagina de inicio)
        [HttpGet("list")]
        public List<Usuariodto> GetListUsuariosPesquisa(string? nome, string? servico)
        {
            var list = new List<Usuariodto>();

            var b = db.Usuarios.Where(u => (string.IsNullOrWhiteSpace(nome) || u.NomeCompleto.Contains(nome))
            && (string.IsNullOrWhiteSpace(servico) || u.IdServicoNavigation.NomeServico == servico) && u.IdServicoNavigation != null).Select(i => new
             {
                ID = i.IdUsuario,
                Nome = i.NomeCompleto,
                Email = i.Email,
                Descricao = i.Descricao,
                Telefone = i.Telefone,
                Servico = i.IdServicoNavigation.NomeServico,
                FuncaoUsuario = i.IdFuncaoNavigation.NomeFuncao,
                FotoPerfil = i.FotoPerfil
            }).ToList();

            foreach (var i in b)
            {
                int media = (int)Math.Round(db.Avaliacoes.Where(j => j.IdUsuario == i.ID).Select(j => (double?)j.QtEstrelas).Average() ?? 0, MidpointRounding.AwayFromZero);

                var u = new Usuariodto()
                {
                    ID = i.ID,
                    NomeCompleto = i.Nome,
                    Email = i.Email,
                    Descricao = i.Descricao,
                    Telefone = i.Telefone,
                    Estrelas = media,
                    Servico = i.Servico,
                    FuncaoUsuario = i.FuncaoUsuario,
                    FotoPerfil = i.FotoPerfil
                };

                list.Add(u);
            }

            return list;
        }

        //serve pra nada por enquanto e ta desatualizado
        [HttpGet("{id}")]
        public Usuariodto GetUsuario(int id)
        {
            var b = db.Usuarios.Where(i => i.IdUsuario == id).FirstOrDefault();

            if (b == null)
            {
                return null;
            }

            var u = new Usuariodto()
            {
                ID = b.IdUsuario,
                NomeCompleto = b.NomeCompleto,
                Email = b.Email,
                Descricao = b.Descricao,
                Telefone = b.Telefone,
                Servico = b.IdServicoNavigation?.NomeServico,
                FuncaoUsuario = b.IdFuncaoNavigation?.NomeFuncao,
                FotoPerfil = b.FotoPerfil
            };

            return u;
        }

        [HttpGet("login/{email}/{senha}")]
        public string GetUsuarioLogin(string email, string senha)
        {
            string senhaHash = senha.TornarHash();

            var b = db.Usuarios.Where(i => i.Email == email && i.Senha == senhaHash).FirstOrDefault();

            if(b == null)
            {
                return string.Empty;
            }
            return b.NomeCompleto;
        }

        [HttpGet("login/foto/{email}/{senha}")]
        public IActionResult GetBuscarFotoPerfil(string email, string senha)
        {
            string senhaHash = senha.TornarHash();

            var b = db.Usuarios.Where(i => i.Email == email && i.Senha == senhaHash).FirstOrDefault();

            if (b == null || b.FotoPerfil == null)
            {
                return NotFound();
            }

            return File(b.FotoPerfil, "application/octet-stream");
        }

        [HttpGet("login/funcao/{email}/{senha}")]
        public int GetVerificarFuncaoUsuario(string email, string senha)
        {
            string senhaHash = senha.TornarHash();

            var b = db.Usuarios.Where(i => i.Email == email && i.Senha == senhaHash).FirstOrDefault();

            if (b == null)
            {
                return 0;
            }
            return b.IdFuncao;
        }

        // ESSE É PARA PEGAR OS DADOS DO PROFISSIONAL PARA A "PÁGINA DE PERFIL DO TRABALHADOR". VAI FALTAR RETORNAR UMA LISTA DAS IMAGENS DE PORTIFÓLIO E A IMAGEM DE PERFIL DO PROFISSIONAL (DTO É O PerfilProfissionaldto)
        [HttpGet("profissional/dados/{email}")]
        public IActionResult GetDadosProfissional(string email)
        {
            var b = db.Usuarios.Where(i => i.Email == email).Select(i => new
            {
                IdUsuario = i.IdUsuario,
                Profissao = i.IdServicoNavigation.NomeServico,
                Nome = i.NomeCompleto,
                Descricao = i.Descricao,
                FotoPerfil = i.FotoPerfil
            }).FirstOrDefault();

            if (b == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var imagens = db.ImagensPortifolios
                .Where(i => i.IdUsuario == b.IdUsuario && i.Imagem != null)
                .Select(i => i.Imagem!)
                .ToList();

            var prof = new PerfilProfissionaldto()
            {
                Nome = b.Nome,
                Profissao = b.Profissao,
                Descricao = b.Descricao,
                FotoPerfil = b.FotoPerfil,
                Portfolio = imagens
            };

            return Ok(prof);

        }

        [HttpPost("cadastro")]
        public IActionResult PostCadastrar([FromBody] UsuarioCadastrodto user)
        {
            var c = db.Funcaos.First(i => i.NomeFuncao == user.Funcao);

            if (user.Servico == null)
            {
                var us = new Usuario()
                {
                    NomeCompleto = user.Nome,
                    Email = user.Email,
                    Senha = user.Senha.TornarHash(),
                    Pin = user.Pin,
                    IdFuncao = c.IdFuncao,
                    FotoPerfil = user.FotoPerfil
                };

                db.Usuarios.Add(us);
                db.SaveChanges();

                return Ok("Usuario cadastrado com sucesso");
            }

            var b = db.Usuarios.Where(i => i.Email == user.Email).FirstOrDefault();
            var a = db.Servicos.First(i => i.NomeServico == user.Servico);
            var j = db.ImagensPortifolios.First(i => i.Imagem == user.ImagemPortifolio);

            if (b != null)
            {
                return BadRequest("Usuario ja cadastrado");
            }

            var u = new Usuario()
            {
                NomeCompleto = user.Nome,
                Email = user.Email,
                Senha = user.Senha.TornarHash(),
                Pin = user.Pin,
                Descricao = user.Descricao,
                Telefone = user.Telefone,
                IdServico = a.IdServico,
                IdFuncao = c.IdFuncao,
                FotoPerfil = user.FotoPerfil
            };

            db.Usuarios.Add(u);
            db.SaveChanges();
            return Ok("Usuario cadastrado com sucesso");
        }

        // MEXER AINDA PARA PODER GUARDAR A IMAGEM DE PERFIL, NÃO FIZ PORQUE ESTAVA SEM O MAUI AI NÃO TINHA COMO TESTAR NO SWAGGER
        [HttpPut("atualizar/funcao/{email}")]
        public IActionResult PutAtualizarFuncaoUsuario([FromBody] UsuarioAtualizarParaProfissinal dto, string email)
        {
            var b = db.Usuarios.FirstOrDefault(i => i.Email == email);
            var a = db.Servicos.FirstOrDefault(i => i.NomeServico == dto.Servico);

            b.Descricao = dto.Descrição.ToString();
            b.Telefone = dto.Telefone;
            b.IdFuncao = 2;
            b.IdServico = a.IdServico;

            db.SaveChanges();

            return Ok();
        }

        // Atualizar senha do usuario
        [HttpPut("atualizar/senha/{email}/{pin}/{novaSenha}")]
        public IActionResult PuAtualizarSenhaUsuario(string email, int pin, string novaSenha)
        {
            var b = db.Usuarios.Where(i => i.Email == email && i.Pin == pin).FirstOrDefault();

            if (b == null)
            {
                return BadRequest("Usuario não encontrado");
            }

            b.Senha = novaSenha.TornarHash();
            db.SaveChanges();
            return Ok("Senha alterada com sucesso");
        }

        [HttpPut("atualizar/foto")]
        public IActionResult PutAtualizarFotoPerfil([FromBody] UsuarioAtualizarFotoPerfildto dto)
        {
            var b = db.Usuarios.FirstOrDefault(i => i.Email == dto.email);

            b.FotoPerfil = dto.foto;
            db.SaveChanges();

            return Ok();
        }
    }
}
