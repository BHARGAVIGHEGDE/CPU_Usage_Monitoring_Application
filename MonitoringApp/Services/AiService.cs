using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;


namespace MonitoringApp.Services
{
    public class AiService
    {

        private readonly string _apiKey;
        private readonly bool _enabled;
        private readonly HttpClient _httpClient;//for my app to talk to API
        public AiService(HttpClient httpClient)

        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("OPEN_API");
         
        }
        public async Task<string> GenerateReportAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return "OpenAI API key is missing.";

            var body = new
            {
                model = "gpt-4o-mini",
                input = prompt
            };

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.openai.com/v1/responses");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            request.Content = new StringContent(
                JsonSerializer.Serialize(body),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)s
                return $"AI error {response.StatusCode}: {responseText}";

            using var doc = JsonDocument.Parse(responseText);

            return doc.RootElement
                .GetProperty("output_text")[0]
                .GetString() ?? "No AI output.";
        }


    }
}
