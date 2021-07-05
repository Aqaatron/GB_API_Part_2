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

            StreamWriter SW = new StreamWriter(new FileStream("Result.txt", FileMode.Create, FileAccess.Write));

            for (int i = 4; i < 14; i++)
            {
                HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/" + i.ToString());

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;

                    var json = await responseContent.ReadAsStreamAsync();

                    var post = await JsonSerializer.DeserializeAsync<Post>(json);


                    SW.WriteLine(post.userId.ToString());

                    SW.WriteLine(post.id.ToString());

                    SW.WriteLine(post.title);

                    SW.WriteLine(post.body);

                    SW.WriteLine();




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
