using HtmlAgilityPack;
using Nest;
using RestSharp;
using SozcuCrawler.Entities;
using SozcuCrawler.Services;

namespace SozcuCrawler;

class Program
{
    static void Main(string[] args)
    {
        CrawlerService crawlerService = new CrawlerService();
        crawlerService.CrawlerServiceMain();
    }
}

