using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using SozcuWebApp.Models;

namespace SozcuWebApp.Pages;
public class IndexModel(IElasticClient elasticClient) : PageModel
{
    public List<SozcuArticle> Articles { get; set; } = new();

    public void OnGet(string query)
    {
        // Eðer kullanýcý bir sorgu girdiyse Elasticsearch'te arama yap
        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchResponse = elasticClient.Search<SozcuArticle>(s => s
                .Query(q => q.Match(m => m
                        .Field(f => f.Title)
                        .Query(query) // Kullanýcýdan gelen arama sorgusu
                ))
                .Size(50)); // Sonuçlarý sýnýrla

            if (searchResponse.IsValid)
            {
                Articles = searchResponse.Documents.ToList();
            }
        }
        else
        {
            // Kullanýcý sorgu girmediyse tüm verileri getir
            var searchResponse = elasticClient.Search<SozcuArticle>(s => s
                .Query(q => q.MatchAll())
                .Size(50)); // Ýlk 50 kaydý getir

            if (searchResponse.IsValid)
            {
                Articles = searchResponse.Documents.ToList();
            }
        }
    }
}

