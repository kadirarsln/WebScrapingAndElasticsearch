using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using SozcuWebApp.Models;

namespace SozcuWebApp.Pages;
public class IndexModel(IElasticClient elasticClient) : PageModel
{
    public List<SozcuArticle> Articles { get; set; } = new();

    public void OnGet(string query)
    {
        // E�er kullan�c� bir sorgu girdiyse Elasticsearch'te arama yap
        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchResponse = elasticClient.Search<SozcuArticle>(s => s
                .Query(q => q.Match(m => m
                        .Field(f => f.Title)
                        .Query(query) // Kullan�c�dan gelen arama sorgusu
                ))
                .Size(50)); // Sonu�lar� s�n�rla

            if (searchResponse.IsValid)
            {
                Articles = searchResponse.Documents.ToList();
            }
        }
        else
        {
            // Kullan�c� sorgu girmediyse t�m verileri getir
            var searchResponse = elasticClient.Search<SozcuArticle>(s => s
                .Query(q => q.MatchAll())
                .Size(50)); // �lk 50 kayd� getir

            if (searchResponse.IsValid)
            {
                Articles = searchResponse.Documents.ToList();
            }
        }
    }
}

