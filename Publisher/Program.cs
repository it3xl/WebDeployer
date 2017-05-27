using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        public static readonly string assemblyName = System.Reflection.Assembly
            .GetExecutingAssembly().GetName().Name;

        private static readonly HttpClient client = new HttpClient
        ();

        static void Main(string[] args)
        {
            try
            {
                var sourcePath = args[0];
                CheckSourceFile(sourcePath);
                var targetUrl = args[1];
                var pulishUrl = new Uri(targetUrl);
                client.BaseAddress = pulishUrl;

                ProcessPublish(sourcePath)
                    .Wait();
            }
            catch (Exception ex)
            {
                Console.Write("<{0}: Exception {1}>", assemblyName, ex.Message);
            }
        }

        private async static Task ProcessPublish(string sourcePath)
        {
            // https://stackoverflow.com/a/22530157
            // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            // https://stackoverflow.com/a/40970671

            HttpResponseMessage response;
            using (var file = new StreamReader(sourcePath))
            {
                var content = new MultipartContent();
                content.Add(new StreamContent(file.BaseStream));
                response = await client.PostAsync("api/deployer?fileName=oppaName", content);
            }



        }

        private static string CheckSourceFile(string sourcePath)
        {
            string absPath;
            if (Path.IsPathRooted(sourcePath))
            {
                absPath = sourcePath;
            }
            else
            {
                absPath = Path.Combine(Environment.CurrentDirectory, sourcePath);
            }
            if (!File.Exists(absPath))
            {
                throw new Exception(string.Format("<{0}: There'is no such a file {1}>", assemblyName, absPath));
            }

            return sourcePath;
        }
    }
}
