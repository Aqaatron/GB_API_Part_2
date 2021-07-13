using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;

namespace prac1
{
    public class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();

            using StreamWriter SW = new StreamWriter(new FileStream("Result.txt", FileMode.Create, FileAccess.Write));

            int startId = 4;

            int finishId = 14;

            for (int i = startId; i < finishId; i++)
            {
                HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/" + i.ToString());

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;

                    var json = await responseContent.ReadAsStreamAsync();

                    var post = await JsonSerializer.DeserializeAsync<Post>(json);

                    await SW.WriteLineAsync(post.userId.ToString());

                    await SW.WriteLineAsync(post.id.ToString());

                    await SW.WriteLineAsync(post.title);

                    await SW.WriteLineAsync(post.body);

                    await SW.WriteLineAsync();

                    
                }
            }

            SW.Close();


        }
    }

    public class Post
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }

}
