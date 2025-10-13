// Crear cliente HTTP
using System.Text.Json;

using var client = new HttpClient();

// URL de la API
string url = "https://jsonplaceholder.typicode.com/posts";

Console.WriteLine("Descargando datos desde la API...");

// Hacer la petición GET
var response = await client.GetAsync(url);
response.EnsureSuccessStatusCode(); // Lanza error si falla

// Leer el contenido como texto
string json = await response.Content.ReadAsStringAsync();

// Convertir el JSON a una lista de objetos Post
var posts = JsonSerializer.Deserialize<List<Objet>>(json);

Console.WriteLine($"Se descargaron {posts.Count} publicaciones.\n");

// Mostrar algunos resultados
foreach (var p in posts.Take(3))
{
    Console.WriteLine($"ID: {p.id} | Usuario: {p.userId}");
    Console.WriteLine($"Título: {p.title}");
    Console.WriteLine($"Cuerpo: {p.body}\n");
}
