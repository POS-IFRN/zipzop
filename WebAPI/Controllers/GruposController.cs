using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class GruposController : ApiController
    {
        Models.ZipZopDataContext dc = new Models.ZipZopDataContext();
        // GET: api/Grupos
        public IEnumerable<Models.GrupoUsuario> Get()
        {
            return dc.GrupoUsuarios;
        }

        // GET: api/Grupos/5
        public Models.GrupoUsuario Get(int id)
        {
            return dc.GrupoUsuarios.Where(g => g.id == id).FirstOrDefault();
        }

        // POST: api/Grupos
        public void Post([FromBody] Models.GrupoUsuario gusuario)
        {
            dc.GrupoUsuarios.InsertOnSubmit(gusuario);
            dc.SubmitChanges();
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody] Models.GrupoUsuario gusuario)
        {
            Models.GrupoUsuario gus = dc.GrupoUsuarios.Where(g => g.id == id).FirstOrDefault();
            gus.descricao = gusuario.descricao;
            gus.id_adm = gusuario.id_adm;
            dc.SubmitChanges();
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
            dc.GrupoUsuarios.DeleteOnSubmit(dc.GrupoUsuarios.Where(gu => gu.id == id).FirstOrDefault());
            dc.SubmitChanges();
        }
    }
}
