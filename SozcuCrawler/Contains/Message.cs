namespace SozcuCrawler.Contains;

public class Message
{
    public void StartMessage()
    {
       var message = "Veri çekme ve Elasticsearch'e kaydetme işlemi başlatılıyor...";
        Console.WriteLine(message);
    }

    public void TitleAndUrlSaveMessage()
    {
        var message = "Başlıklar ve URL'ler Elasticsearch'e kaydediliyor...";
        Console.WriteLine(message);
    }

    public void CompleteElasticSearchSaveMessage()
    {
        var message = "Elasticsearch kaydetme işlemi tamamlandı.";
        Console.WriteLine(message);
    }

    public void DataNotFoundMessage()
    {
        var message = "Hedeflenen öğeler bulunamadı!";
        Console.WriteLine(message);
    }

    public void TransactionCompletedMessage()
    {
        var message = "İşlem tamamlandı.";
        Console.WriteLine(message);
    }

    public void AccessFailedMessage()
    {
        var message = "Web sitesine erişim başarısız!";
        Console.WriteLine(message);
    }
}

