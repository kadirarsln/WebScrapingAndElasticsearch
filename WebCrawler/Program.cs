namespace WebCrawler;
class Program
{
    static async Task Main(string[] args)
    {
        var crawlerService = new CrawlerService();
        await crawlerService.CrawlAndStoreDataAsync();
    }
}