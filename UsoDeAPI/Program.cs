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
Console.WriteLine("\n1. Posts del userId = 1 ------------------------------------------------------------------------------------------------------");
user1Posts.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n "+await ia.ConsultIAAsync("Resumen del uso de 'posts.Where(p => p.userId == userId).ToList();' en C# "));//Consulta IA

// posts que contienen la palabra “qui”
var postsConQui = Consultas.PostsConPalabra(posts, "qui");
Console.WriteLine("\n2. Posts que contienen la palabra 'qui': -------------------------------------------------------------------------------------");
postsConQui.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.OrderBy(p => p.title).Select(p => p.title).ToList();' en C# "));// Consulta IA

//  titulos ordenados alfabéticamente
var titulosOrdenados = Consultas.ObtenerTitulosOrdenados(posts);
Console.WriteLine("\n3. Títulos ordenados alfabéticamente: ------------------------------------------------------------------------------------------");
titulosOrdenados.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine("\n---- Explicacion IA:" +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Where(p => p.body.Contains(palabra, StringComparison.OrdinalIgnoreCase)).ToList();' en C# "));// Consulta IA

//  posts con cuerpo > 200 caracteres
var cuerpoLargo = Consultas.PostsCuerpoLargo(posts);
Console.WriteLine("\n4. Posts con cuerpo > 200 caracteres: ------------------------------------------------------------------------------------------");
cuerpoLargo.ForEach(p => Console.WriteLine($"- {p.title} ({p.body.Length} caracteres)"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("posts.Where(p => p.body.Length > 200).ToList();' en C# "));// Consulta IA

//  Top 10 títulos más cortos
var cortos = Consultas.TitulosMasCortos(posts);
Console.WriteLine("5. Top 10 títulos más cortos: ---------------------------------------------------------------------------------------------------");
cortos.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.OrderBy(p => p.title.Length).Take(10).Select(p => p.title).ToList();' en C# "));// Consulta IA

//  Agrupar posts por usuario
var agrupados = Consultas.AgruparPorUsuario(posts);
Console.WriteLine("\n6. Agrupar posts por usuario: --------------------------------------------------------------------------------------------------------");
foreach (var grupo in agrupados)
{
    Console.WriteLine($"Usuario {grupo.Key}: {grupo.Value.Count} posts");
}
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g => g.ToList());' en C# "));// Consulta IA

// Contar cuántos posts tiene cada usuario
var conteo = Consultas.ConteoPorUsuario(posts);
Console.WriteLine("\n7. Conteo de posts por usuario: ------------------------------------------------------------------------------------------------------");
foreach (var kv in conteo)
{
    Console.WriteLine($"Usuario {kv.Key}: {kv.Value} posts");
}
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.OrderBy(p => p.title).Select(p => p.title).ToList();' en C# "));// Consulta IA

// Promedio de longitud del cuerpo por usuario
var promedio = Consultas.PromedioCuerpoPorUsuario(posts);
Console.WriteLine("\n8. Promedio de longitud del cuerpo por usuario: ---------------------------------------------------------------------------------------");
foreach (var kv in promedio)
{
    Console.WriteLine($"Usuario {kv.Key}: {kv.Value:F2} caracteres");
}
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.OrderBy(p => p.title).Select(p => p.title).ToList();' en C# "));// Consulta IA

//Post más largo
var largo = Consultas.PostMasLargo(posts);
Console.WriteLine("\n9. Post más largo: --------------------------------------------------------------------------------------------------------------------");
Console.WriteLine($"- {largo.title} ({largo.body.Length} caracteres)\n");
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g => g.Count());' en C# "));// Consulta IA

//  Post más corto
var corto = Consultas.PostMasCorto(posts);
Console.WriteLine("\n10. Post más corto: --------------------------------------------------------------------------------------------------------------------");
Console.WriteLine($"- {corto.title} ({corto.body.Length} caracteres)\n");
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.userId).ToDictionary" +
    "(g => g.Key, g => g.Average(p => p.body.Length));' en C# "));// Consulta IA

//  Primeros 5 registros
var primeros = Consultas.PrimerosCinco(posts);
Console.WriteLine("\n11. Primeros 5 registros: -----------------------------------------------------------------------------------------------------------------");
primeros.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Take(5).ToList();' en C# "));// Consulta IA

//  Saltar los primeros 10
var saltados = Consultas.SaltarDiez(posts);
Console.WriteLine("\n12. Posts después de saltar los primeros 10: ----------------------------------------------------------------------------------------------");
saltados.Take(5).ToList().ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Skip(10).ToList();' en C# "));// Consulta IA

// Títulos distintos
var distintos = Consultas.TitulosDistintos(posts);
Console.WriteLine("\n13. Títulos distintos: --------------------------------------------------------------------------------------------------------------------");
distintos.ForEach(t => Console.WriteLine($"- {t}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Select(p => p.title).Distinct().ToList();' en C# "));// Consulta IA

// Usuarios con más de 8 posts
var usuarios = Consultas.UsuariosConMasDeOchoPosts(posts);
Console.WriteLine("\n14. Usuarios con más de 8 posts:-----------------------------------------------------------------------------------------------------------");
usuarios.ForEach(u => Console.WriteLine($"- Usuario {u}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.userId).Where(g => g.Count() > 8).Select(g => g.Key).ToList();' en C# "));// Consulta IA

//Tres títulos más largos por usuario
var largosPorUsuario = Consultas.TitulosLargosPorUsuario(posts);
Console.WriteLine("\n15. Tres títulos más largos por usuario:---------------------------------------------------------------------------------------------------");
foreach (var kv in largosPorUsuario)
{
    Console.WriteLine($"Usuario {kv.Key}:");
    kv.Value.ForEach(t => Console.WriteLine($"  - {t}"));
}
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g =>" +
    " g.OrderByDescending(p => p.title.Length).Take(3).Select(p => p.title).ToList());' en C# "));// Consulta IA

//Agrupar por número par e impar de ID
var paridad = Consultas.AgruparPorParidadID(posts);
Console.WriteLine("\n16. Agrupación por paridad de ID: ---------------------------------------------------------------------------------------------------------");
foreach (var kv in paridad)
{
    Console.WriteLine($"{kv.Key}: {kv.Value.Count} posts");
}
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.GroupBy(p => p.id % 2 == 0 ? \"Par\" : \"Impar\")" +
    ".ToDictionary(g => g.Key, g => g.ToList());' en C# "));// Consulta IA

//Combinar filtros: usuario  palabra clave
var combinados = Consultas.FiltrarUsuarioYPalabra(posts, 1, "qui");
Console.WriteLine("\n17. Posts del usuario 1 que contienen 'qui': -----------------------------------------------------------------------------------------------");
combinados.ForEach(p => Console.WriteLine($"- {p.title}"));
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Where(p => p.userId == userId && p.body" +
    ".Contains(palabra, StringComparison.OrdinalIgnoreCase)).ToList();' en C# "));// Consulta IA

//  Promedio global de longitud del cuerpo
var promedioGlobal = Consultas.PromedioGlobalCuerpo(posts);
Console.WriteLine($"\n18. Promedio global de longitud del cuerpo: {promedioGlobal:F2} caracteres -----------------------------------------------------------------\n");
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'posts.Average(p => p.body.Length);' en C# "));// Consulta IA

//Proporción de posts largos vs cortos
var (largosCount, cortosCount) = Consultas.ProporcionPostsLargosCortos(posts);
Console.WriteLine("\n19. Proporción de posts largos vs cortos: ---------------------------------------------------------------------------------------------------");
Console.WriteLine($"- Largos (>200): {largosCount}");
Console.WriteLine($"- Cortos (<=200): {cortosCount}");
Console.WriteLine("\n---- Explicacion IA: " +
    "\n " + await ia.ConsultIAAsync("Resumen del uso de 'int largos = posts.Count(p => p.body.Length > 200);" +
    "\r\nint cortos = posts.Count(p => p.body.Length <= 200);" +
    "\r\nreturn (largos, cortos);' en C# "));// Consulta IA


//------------------------- Consultas adicionales ----------------------------------
Console.WriteLine("------------------------------------------------------------------" +
    "                         Consultas adicionales hechas con IA " +
    "------------------------------------------------------------------");
//Resumir el contenido de los resultados
string resumen = await ia.ConsultIAAsync($"Resume brevemente los siguientes títulos: {string.Join(", ", agrupados)}\n\n");
Console.WriteLine(resumen);

//Clasificar los posts
string clasificacion = await ia.ConsultIAAsync($"Clasifica estos textos en categorías temáticas: {string.Join(", ", posts.Select(p => p.title))}\n\n");
Console.WriteLine(clasificacion);

//Generar nuevas consultas
string sugerencia = await ia.ConsultIAAsync("Genera 3 consultas LINQ útiles para analizar una lista de posts con userId, title y body.");
Console.WriteLine(sugerencia);
