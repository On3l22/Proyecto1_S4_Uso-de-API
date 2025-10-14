using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsoDeAPI
{
    public class Consultas
    {

        //Consulta de prueba, filtrar la cantidad de publicaciones por userId
        public static List<Objet> FiltrarPorUserId(List<Objet> posts, int userId)
        {
            return posts.Where(p => p.userId == userId).ToList();
        }

        public static List<string> ObtenerTitulosOrdenados(List<Objet> posts)
        {
            return posts.OrderBy(p => p.title).Select(p => p.title).ToList();
        }

        //  Posts que contienen la palabra “qui”
        public static List<Objet> PostsConPalabra(List<Objet> posts, string palabra) =>
            posts.Where(p => p.body.Contains(palabra, StringComparison.OrdinalIgnoreCase)).ToList();

        // Posts con cuerpo > 200 caracteres
        public static List<Objet> PostsCuerpoLargo(List<Objet> posts) =>
            posts.Where(p => p.body.Length > 200).ToList();

        // Top 10 títulos más cortos
        public static List<string> TitulosMasCortos(List<Objet> posts) =>
            posts.OrderBy(p => p.title.Length).Take(10).Select(p => p.title).ToList();

        //  Agrupar posts por usuario
        public static Dictionary<int, List<Objet>> AgruparPorUsuario(List<Objet> posts) =>
            posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g => g.ToList());

        // Contar cuántos posts tiene cada usuario
        public static Dictionary<int, int> ConteoPorUsuario(List<Objet> posts) =>
            posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g => g.Count());

        //Promedio de longitud del cuerpo por usuario
        public static Dictionary<int, double> PromedioCuerpoPorUsuario(List<Objet> posts) =>
            posts.GroupBy(p => p.userId).ToDictionary(g => g.Key, g => g.Average(p => p.body.Length));

        // Post más largo
        public static Objet PostMasLargo(List<Objet> posts) =>
            posts.OrderByDescending(p => p.body.Length).First();

        // Post más corto
        public static Objet PostMasCorto(List<Objet> posts) =>
            posts.OrderBy(p => p.body.Length).First();

        // Primeros 5 registros
        public static List<Objet> PrimerosCinco(List<Objet> posts) =>
            posts.Take(5).ToList();

        //Saltar los primeros 10
        public static List<Objet> SaltarDiez(List<Objet> posts) =>
            posts.Skip(10).ToList();

        //Obtener títulos distintos
        public static List<string> TitulosDistintos(List<Objet> posts) =>
            posts.Select(p => p.title).Distinct().ToList();

        // Usuarios con más de 8 posts
        public static List<int> UsuariosConMasDeOchoPosts(List<Objet> posts) =>
            posts.GroupBy(p => p.userId).Where(g => g.Count() > 8).Select(g => g.Key).ToList();

        // Tres títulos más largos por usuario
        public static Dictionary<int, List<string>> TitulosLargosPorUsuario(List<Objet> posts) =>
            posts.GroupBy(p => p.userId)
                 .ToDictionary(g => g.Key,
                               g => g.OrderByDescending(p => p.title.Length)
                                     .Take(3)
                                     .Select(p => p.title)
                                     .ToList());

        // Agrupar por número par/impar de ID
        public static Dictionary<string, List<Objet>> AgruparPorParidadID(List<Objet> posts) =>
            posts.GroupBy(p => p.id % 2 == 0 ? "Par" : "Impar")
                 .ToDictionary(g => g.Key, g => g.ToList());

        // Combinar filtros usuario palabra clave
        public static List<Objet> FiltrarUsuarioYPalabra(List<Objet> posts, int userId, string palabra) =>
            posts.Where(p => p.userId == userId && p.body.Contains(palabra, StringComparison.OrdinalIgnoreCase)).ToList();

        //  Promedio global de longitud del cuerpo
        public static double PromedioGlobalCuerpo(List<Objet> posts) =>
            posts.Average(p => p.body.Length);

        //  Proporción de posts largos (>200) vs cortos (<=200)
        public static (int largos, int cortos) ProporcionPostsLargosCortos(List<Objet> posts)
        {
            int largos = posts.Count(p => p.body.Length > 200);
            int cortos = posts.Count(p => p.body.Length <= 200);
            return (largos, cortos);
        }


    }
}
