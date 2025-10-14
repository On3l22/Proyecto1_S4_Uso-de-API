using System.Text;
using Newtonsoft.Json.Linq;


public class ConsultaIA
{
    public HttpClient client;

    public ConsultaIA()
    {
        this.client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
        client.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/");
        client.DefaultRequestHeaders.Add("X-Title", "UsoDeAPI");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<string> ConsultIAAsync(string mensaje)
    {
        var json2 = new
        {
            model = "gryphe/mythomax-l2-13b",
            messages = new[]
            {
                new { role = "user", content = mensaje }
            }
        };

        var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(json2), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return $"Error en la peticion: {response.StatusCode}";
        }

        try
        {
            var jsonObj = JObject.Parse(result);
            var messaje = jsonObj["choices"]?[0]?["message"]?["content"]?.ToString();

            if (string.IsNullOrEmpty(messaje))
                return "El modelo no devolvio ningun mensaje.";
            else
                return $"Respuesta del modelo: \n {messaje}";
        }catch (Exception ex)
        {
            return $"Error al analizar JSON {ex.Message}";
        }
    }
}

