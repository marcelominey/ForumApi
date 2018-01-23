
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ForumApi.Models;

namespace ForumApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("Usuario")]
    public class UsuarioController:Controller
    {
        //Usuario usuario = new Usuario();
        DAOUsuario dusuario = new DAOUsuario(); //o cara que se comunica com a base de dados é o DAO
    
    [HttpGet]
        public IEnumerable<Usuario> Listar()
        {            
            return dusuario.ListarUsuario();
        }

    /*[HttpGet("{id}")]
        public Usuario Listar(int id)
        {
            return dusuario.ListarUsuario().Where(x => x.Idusuario == id).FirstOrDefault();
            //ao invés de criar uma outra consulta lá no meu DAO, eu to usando esse mesmo, e usando um lambda.
            //pega minha lista inteira, se ele realmente encontrar esse id, ele me retorna verdadeiro, me retorna o dado dessa lista.
            //por padrão, se ele não encontrar, ele retorna o primeiro dado para mim.
        }*/
        [HttpGet("{id}")]
        public IActionResult Listar(int id)
        {
            var rs = new JsonResult(dusuario.ListarUsuario().Where(x => x.Idusuario == id).FirstOrDefault());
            //Ao invés de retornar um Usuario, estou retornando um JSON
            rs.ContentType = "application/json";
            //vamos fazer uma verificação:
            if(rs.Value==null){
                rs.StatusCode = 204; //não tem conteúdo (em outros casos, 404)
                //se retornar nulo, é pq ele não encontrou nada
                rs.Value = "Não há resultado para essa pesquisa";
            }
            else{
                rs.StatusCode = 200;
            }
            return Json(rs);
        }

    [HttpPost]
        public void Adicionar([FromBody] Usuario usuario){
            dusuario.Cadastro(usuario);
            //qdo for mandar o FromBody, o nome dos campos tem que estar igual
        }
    }

}

      