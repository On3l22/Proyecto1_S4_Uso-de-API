// Crear cliente HTTP
using System.Text;
using System.Text.Json;
using UsoDeAPI;

string mensaje = "";
ConsultaIA ia = new ConsultaIA();

//------------------- Conexion con API Rest --------------------------

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
JsonSerializer.Deserialize<List<Objet>>(json);

//--------------------- Consultas linq ------------------------------
Console.WriteLine("Se imprimen las consultas de LINQ \n");
//Posts del userId = 1
var user1Posts = Consultas.FiltrarPorUserId(posts, 1);
Console.WriteLine("1. Posts del userId = 1:");
user1Posts.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine(await ia.ConsultIAAsync("Resumen del uso de 'posts.Where(p => p.userId == userId).ToList();' en C# "));

// posts que contienen la palabra “qui”
var postsConQui = Consultas.PostsConPalabra(posts, "qui");
Console.WriteLine("2. Posts que contienen la palabra 'qui':");
postsConQui.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine();

//  titulos ordenados alfabéticamente
var titulosOrdenados = Consultas.ObtenerTitulosOrdenados(posts);
Console.WriteLine("3. Títulos ordenados alfabéticamente:");
titulosOrdenados.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine();

//  posts con cuerpo > 200 caracteres
var cuerpoLargo = Consultas.PostsCuerpoLargo(posts);
Console.WriteLine("4. Posts con cuerpo > 200 caracteres:");
cuerpoLargo.ForEach(p => Console.WriteLine($"- {p.title} ({p.body.Length} caracteres)"));
Console.WriteLine();

//  Top 10 títulos más cortos
var cortos = Consultas.TitulosMasCortos(posts);
Console.WriteLine("5. Top 10 títulos más cortos:");
cortos.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine();

//  Agrupar posts por usuario
var agrupados = Consultas.AgruparPorUsuario(posts);
Console.WriteLine("6. Agrupar posts por usuario:");
foreach (var grupo in agrupados)
{
    Console.WriteLine($"Usuario {grupo.Key}: {grupo.Value.Count} posts");
}
Console.WriteLine();

// Contar cuántos posts tiene cada usuario
var conteo = Consultas.ConteoPorUsuario(posts);
Console.WriteLine("7. Conteo de posts por usuario:");
foreach (var kv in conteo)
{
    Console.WriteLine($"Usuario {kv.Key}: {kv.Value} posts");
}
Console.WriteLine();

// Promedio de longitud del cuerpo por usuario
var promedio = Consultas.PromedioCuerpoPorUsuario(posts);
Console.WriteLine("8. Promedio de longitud del cuerpo por usuario:");
foreach (var kv in promedio)
{
    Console.WriteLine($"Usuario {kv.Key}: {kv.Value:F2} caracteres");
}
Console.WriteLine();

//Post más largo
var largo = Consultas.PostMasLargo(posts);
Console.WriteLine("9. Post más largo:");
Console.WriteLine($"- {largo.title} ({largo.body.Length} caracteres)\n");

//  Post más corto
var corto = Consultas.PostMasCorto(posts);
Console.WriteLine("10. Post más corto:");
Console.WriteLine($"- {corto.title} ({corto.body.Length} caracteres)\n");

//  Primeros 5 registros
var primeros = Consultas.PrimerosCinco(posts);
Console.WriteLine("11. Primeros 5 registros:");
primeros.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine();

//  Saltar los primeros 10
var saltados = Consultas.SaltarDiez(posts);
Console.WriteLine("12. Posts después de saltar los primeros 10:");
saltados.Take(5).ToList().ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine();

// Títulos distintos
var distintos = Consultas.TitulosDistintos(posts);
Console.WriteLine("13. Títulos distintos:");
distintos.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine();

// Usuarios con más de 8 posts
var usuarios = Consultas.UsuariosConMasDeOchoPosts(posts);
Console.WriteLine("14. Usuarios con más de 8 posts:");
usuarios.ForEach(u => Console.WriteLine($"- Usuario {u}"));
Console.WriteLine();

//Tres títulos más largos por usuario
var largosPorUsuario = Consultas.TitulosLargosPorUsuario(posts);
Console.WriteLine("15. Tres títulos más largos por usuario:");
foreach (var kv in largosPorUsuario)
{
    Console.WriteLine($"Usuario {kv.Key}:");
    kv.Value.ForEach(t => Console.WriteLine($"  - {t}"));
}
Console.WriteLine();

//Agrupar por número par e impar de ID
var paridad = Consultas.AgruparPorParidadID(posts);
Console.WriteLine("16. Agrupación por paridad de ID:");
foreach (var kv in paridad)
{
    Console.WriteLine($"{kv.Key}: {kv.Value.Count} posts");
}
Console.WriteLine();

//Combinar filtros: usuario  palabra clave
var combinados = Consultas.FiltrarUsuarioYPalabra(posts, 1, "qui");
Console.WriteLine("17. Posts del usuario 1 que contienen 'qui':");
combinados.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine();

//  Promedio global de longitud del cuerpo
var promedioGlobal = Consultas.PromedioGlobalCuerpo(posts);
Console.WriteLine($"18. Promedio global de longitud del cuerpo: {promedioGlobal:F2} caracteres\n");

//Proporción de posts largos vs cortos
var (largosCount, cortosCount) = Consultas.ProporcionPostsLargosCortos(posts);
Console.WriteLine("19. Proporción de posts largos vs cortos:");
Console.WriteLine($"- Largos (>200): {largosCount}");
Console.WriteLine($"- Cortos (<=200): {cortosCount}");