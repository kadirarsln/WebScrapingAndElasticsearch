using HtmlAgilityPack;
using Nest;
using RestSharp;
using SozcuCrawler.Contains;
using SozcuCrawler.Entities;

namespace SozcuCrawler.Services;

public class CrawlerService
{
    public void CrawlerServiceMain()
    {
        Message message = new Message();
        message.StartMessage();

        // Elasticsearch istemcisi oluşturma
        var settings = new ConnectionSettings(new Uri("http://localhost:9200")) // Elasticsearch URL'i
            .DefaultIndex("sozcu_data");
        var client = new ElasticClient(settings);

        // Hedef URL
        string url = "https://www.sozcu.com.tr/";

        // RestSharp istemcisiyle sayfa içeriğini çekiyoruz
        var restClient = new RestClient(url);
        var request = new RestRequest(); // Yeni bir istek oluştur
        request.Method = Method.Get; // HTTP metodunu ayarla

        var response = restClient.Execute(request);

        if (response.IsSuccessful)
        {
            // HtmlAgilityPack ile HTML'yi parse et
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content);

            // Başlıkları seçmek için XPath veya CSS seçicileri kullan
            var nodes = htmlDoc.DocumentNode.SelectNodes("//header/a"); // Hedef XPath

            if (nodes != null)
            {
                message.TitleAndUrlSaveMessage();

                var dataList = new List<SozcuArticle>();

                foreach (var node in nodes)
                {
                    string title = node.InnerText.Trim();
                    string link = node.GetAttributeValue("href", "");

                    // Veriyi nesne olarak kaydet
                    var article = new SozcuArticle
                    {
                        Title = title,
                        Link = link,
                        Date = DateTime.Now
                    };

                    dataList.Add(article);

                    // Elasticsearch'e tek tek ekle
                    var indexResponse = client.IndexDocument(article);
                    if (indexResponse.IsValid)
                    {
                        Console.WriteLine($"Kaydedildi: {title}");
                    }
                    else
                    {
                        Console.WriteLine($"Hata: {indexResponse.OriginalException.Message}");
                    }
                }

                message.CompleteElasticSearchSaveMessage();
            }
            else
            {
                message.DataNotFoundMessage();
            }
        }
        else
        {
            message.AccessFailedMessage();
        }

        message.TransactionCompletedMessage();
    }
}


