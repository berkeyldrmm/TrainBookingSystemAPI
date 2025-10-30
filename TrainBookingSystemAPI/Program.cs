using Microsoft.AspNetCore.Http.HttpResults;
using TrainBookingSystemAPI.Abstracts.Rezervasyonlar;
using TrainBookingSystemAPI.Dtos.Rezervasyonlar.YerlesimAyrinti;
using TrainBookingSystemAPI.Services.Rezervasyonlar;
using TrainBookingSystemAPI.Wrappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IRezervasyonService, RezervasyonService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/rezervasyon-kontrol", (RezervasyonKontrolRequest request, IRezervasyonService _rezervasyonService) =>
{
    if (!_rezervasyonService.RezervastonYapilabilirlik(request))
    {
        return Results.Ok(new RezervasyonKontrolResponse(false, Array.Empty<YerlesimAyrintiDto>()));
    }

    return Results.Ok(new RezervasyonKontrolResponse(true, _rezervasyonService.YerlesimAyrintiOlustur(request)));
});

app.Run();
