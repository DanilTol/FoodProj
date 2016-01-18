using System.Net.Http;

namespace FoodService.WebApi2.Infrastructure
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : System.Guid.NewGuid().ToString();
            return System.Guid.NewGuid() + name.Replace("\"", string.Empty);
        }
    }
}