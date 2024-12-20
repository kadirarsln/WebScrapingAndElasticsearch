using Nest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("sozcu_data"); // Elasticsearch'deki indeks
    return new ElasticClient(settings);
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
