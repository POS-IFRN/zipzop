using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        Models.ZipZopDataContext dc = new Models.ZipZopDataContext();
        private string foto_path = String.Empty;
        // GET: api/Usuario
        public IEnumerable<Models.Usuario> Get()
        {
            return dc.Usuarios;
        }

        // GET: api/Usuario/5
        public Models.Usuario Get(int id)
        {
            return dc.Usuarios.Where(u => u.id == id).FirstOrDefault();
        }

        // POST: api/Usuario
        public async void Post([FromBody] Models.Usuario usuario)
        {
            await PostFormData();
            usuario.foto = foto_path;
            dc.Usuarios.InsertOnSubmit(usuario);
            dc.SubmitChanges();
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody] Models.Usuario usuario)
        {
            Models.Usuario us = dc.Usuarios.Where(u => u.id == id).FirstOrDefault();
            us.nome = usuario.nome;
            us.uri = usuario.uri;
            us.foto = usuario.foto;
            dc.SubmitChanges();
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
            dc.Usuarios.DeleteOnSubmit(dc.Usuarios.Where(u => u.id == id).FirstOrDefault());
            dc.SubmitChanges();
        }

        private async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Fotos");
            var provider = new MultipartFormDataStreamProvider(root);
            //provider.GetLocalFileName(file.Headers)
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + provider.GetLocalFileName(file.Headers));
                    foto_path = provider.GetLocalFileName(file.Headers);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
