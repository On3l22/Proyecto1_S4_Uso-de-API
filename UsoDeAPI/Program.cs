// Crear cliente HTTP
using System.Text;
using System.Text.Json;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;



//using var client = new HttpClient();

//// URL de la API
//string url = "https://jsonplaceholder.typicode.com/posts";

//Console.WriteLine("Descargando datos desde la API...");

//// Hacer la petición GET
//var response = await client.GetAsync(url);
//response.EnsureSuccessStatusCode(); // Lanza error si falla

//// Leer el contenido como texto
//string json = await response.Content.ReadAsStringAsync();

//// Convertir el JSON a una lista de objetos Post
//var posts = JsonSerializer.Deserialize<List<Objet>>(json);

//Console.WriteLine($"Se descargaron {posts.Count} publicaciones.\n");

//// Mostrar algunos resultados
//foreach (var p in posts.Take(3))
//{
//    Console.WriteLine($"ID: {p.id} | Usuario: {p.userId}");
//    Console.WriteLine($"Título: {p.title}");
//    Console.WriteLine($"Cuerpo: {p.body}\n");
//}




Console.Write("Escribe tu mensaje: ");
string mensaje = Console.ReadLine();

var client2 = new HttpClient();
client2.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
client2.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/");
client2.DefaultRequestHeaders.Add("X-Title", "UsoDeAPI");
client2.DefaultRequestHeaders.Add("Accept", "application/json");

var json2 = new
{
    model = "gryphe/mythomax-l2-13b",
    messages = new[]
    {
                new { role = "OnelBethx", content = mensaje }
            }
};

var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(json2), Encoding.UTF8, "application/json");
var response2 = await client2.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
var result = await response2.Content.ReadAsStringAsync();

if (!response2.IsSuccessStatusCode)
{
    Console.WriteLine("Error en la petición: " + response2.StatusCode);
    return;
}

try
{
    var jsonObj = JObject.Parse(result);
    var message = jsonObj["choices"]?[0]?["message"]?["content"]?.ToString();

    if (string.IsNullOrEmpty(message))
        Console.WriteLine("El modelo no devolvió ningún mensaje.");
    else
        Console.WriteLine("Respuesta del modelo:\n" + message);
}
catch (Exception ex)
{
    Console.WriteLine("Error al analizar JSON: " + ex.Message);
}