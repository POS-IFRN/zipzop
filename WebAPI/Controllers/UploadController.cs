using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

public class UploadController : ApiController
{
    public async Task<HttpResponseMessage> PostFormData()
    {
        System.Console.WriteLine("olá");
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
            //provider.GetLocalFileName(file.Headers) salvar isso no banco
            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                Trace.WriteLine("Server file path: " + provider.GetLocalFileName(file.Headers));
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        catch (System.Exception e)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        }
    }

}