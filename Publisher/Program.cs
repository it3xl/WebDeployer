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
        private static readonly string AssemblyName = System.Reflection.Assembly
            .GetExecutingAssembly().GetName().Name;

        private static readonly HttpClient Client = new HttpClient();

        static void Main(string[] args)
        {
            try
            {
                var sourcePath = args[0];
                CheckSourceFile(sourcePath);
                var targetUrl = args[1];
                var pulishUrl = new Uri(targetUrl);
                Client.BaseAddress = pulishUrl;
                Client.Timeout = TimeSpan.FromMinutes(20);

                ProcessPublish(sourcePath)
                    .Wait();

                Console.WriteLine(FormatMessage("Successfully done"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(FormatMessage($"Exception {ex.Message}"));
                Environment.Exit(1);
            }
        }

        private static async Task ProcessPublish(string sourcePath)
        {
            // https://stackoverflow.com/a/42277472/390940
            // https://stackoverflow.com/a/39416373/390940

            HttpResponseMessage response;
            using (var file = new StreamReader(sourcePath))
            {
                //var content = new MultipartContent();

                //var content = new MultipartFormDataContent();
                //content.Add(new StreamContent(file.BaseStream));
                //response = await Client.PostAsync("api/deployer?fileName=oppaName", content);

                var content = new StreamContent(file.BaseStream);
                Console.WriteLine(FormatMessage("Filestream HTTP-transfer started"));
                response = await Client.PostAsync("api/deployer", content);
            }

            if (!response.IsSuccessStatusCode)
            {
                BreakByError($"Fail web response - {response.StatusCode}");
                BreakByError("Be aware of strange and unclear HTTP errors when sizing-limits are exceeded");
            }
        }

        private static void CheckSourceFile(string sourcePath)
        {
            var absPath = Path.IsPathRooted(sourcePath) 
                ? sourcePath
                : Path.Combine(Environment.CurrentDirectory, sourcePath);

            if (File.Exists(absPath))
                return;

            BreakByError($"There'is no such a file {absPath}");
        }

        private static void BreakByError(string message)
        {
            var output = FormatMessage(message);
            Console.WriteLine(output);

            throw new Exception(output);
        }

        private static string FormatMessage(string message)
        {
            var output = $"<{AssemblyName}>: " + message;

            return output;
        }
    }
}
