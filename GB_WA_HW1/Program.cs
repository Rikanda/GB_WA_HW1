using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            List<Post> posts = new List<Post>();

            for(int i = 4; i < 14; i++)
            {
                string request = "https://jsonplaceholder.typicode.com/posts/" + i;
                var response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Post post = JsonConvert.DeserializeObject<Post>(responseBody);
                posts.Add(post);
            }

            string path = @"c:\temp\result.txt";
            string appendText = "";
            if (!File.Exists(path))
            {
                string createText = "This is posts for my homework" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }

            foreach(Post post in posts)
            {
                appendText = post.userId + Environment.NewLine + post.id+ Environment.NewLine + post.title+ Environment.NewLine + post.body+ Environment.NewLine+ Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
            
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);
        }
    }

    class Post
    {
        public string userId { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string body { get; set; }

    }
}