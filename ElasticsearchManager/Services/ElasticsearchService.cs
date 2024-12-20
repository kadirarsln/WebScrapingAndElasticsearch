using Nest;

namespace ElasticsearchManager.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("sozcudata"); // Default index adı
            _client = new ElasticClient(settings);
        }

        public async Task IndexDataAsync(string title)
        {
            var document = new { Title = title };  // Belgeyi oluşturuyoruz
            var response = await _client.IndexDocumentAsync(document);  // Veriyi Elasticsearch'e ekliyoruz
            Console.WriteLine($"Data indexed: {response.IsValid}");  // Sonuçları konsola yazdırıyoruz
        }

        // Arama işlemi yapacak olan metot
        public async Task<List<string>> SearchDataAsync(string query)
        {
            var response = await _client.SearchAsync<object>(s => s
                .Query(q => q
                    .Match(m => m
                            .Field("Title")
                            .Query(query) // Arama kriteri
                    )
                )
            );

            var results = new List<string>();

            // Arama sonucunda dönen verileri ekliyoruz
            foreach (var hit in response.Hits)
            {
                results.Add(hit.Source.ToString()); // Her bir sonucu listeye ekliyoruz
            }

            return results;
        }
    }
}