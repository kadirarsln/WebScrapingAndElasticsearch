using ElasticsearchManager.Services;
using HtmlAgilityPack;

namespace WebCrawler;

    public class CrawlerService
    {
        private readonly ElasticsearchService _elasticsearchService;

        public CrawlerService()
        {
            _elasticsearchService = new ElasticsearchService();
        }

        public async Task CrawlAndStoreDataAsync()
        {
            var url = "https://www.sozcu.com.tr";  // Web scraping yapılacak site
            var web = new HtmlWeb();
            var document = web.Load(url);

            var headlines = document.DocumentNode.SelectNodes("//header/a");  // Başlıkları çeken XPath
            if (headlines != null)
            {
                foreach (var headline in headlines)
                {
                    var title = headline.InnerText;
                    await _elasticsearchService.IndexDataAsync(title);  // Elasticsearch’e gönderme
                }
            }
        }
    }
