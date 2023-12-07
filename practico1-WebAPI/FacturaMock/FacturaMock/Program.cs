
using FacturaMock.Models;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(
    options => options.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
);
}

app.UseHttpsRedirection();

app.MapPost("/reporte", (RequestData requestData) =>
{
    // Lee los datos del cuerpo de la solicitud
    var htmlCode = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Template\\invoice1.html");

    var fechaInicio = requestData.FechaInicio.ToString();
    var fechaFin = requestData.FechaFin.ToString();
    var comisionesTotales = requestData.totalComisiones.ToString();

    DateTime dt1 = DateTime.Now;
    DateTime dt2 = dt1;
    var culture = CultureInfo.CreateSpecificCulture("es-UY");
    var styles = DateTimeStyles.None;
    styles |= DateTimeStyles.AssumeLocal;

    var number = float.PositiveInfinity;
    bool fechaValida = DateTime.TryParse(fechaInicio, culture, styles, out dt1)
                           && DateTime.TryParse(fechaFin, culture, styles, out dt2)
                           && float.TryParse(comisionesTotales,out number);

    if (fechaValida)
    {
        htmlCode = htmlCode.Replace("{{FechaInicio}}", fechaInicio);
        htmlCode = htmlCode.Replace("{{FechaFin}}", fechaFin);
        htmlCode = htmlCode.Replace("{{ComisionesTotales}}", comisionesTotales);
    } else
    {
        var errorResponse = "Los datos proporcionados son inválidos. Asegúrate de que 'fechaInicio' y 'FechaFin' sean fechas válidas y 'ComisionesTotales' sea un número válido.";

        // Devuelve el mensaje JSON de error
        return errorResponse;
    }

    //htmlCode = htmlCode.Replace("{{FechaInicio}}", fechaInicio.ToString("yyyy-MM-dd"));
    //htmlCode = htmlCode.Replace("{{FechaFin}}", fechaFin.ToString("yyyy-MM-dd"));
    //htmlCode = htmlCode.Replace("{{ComisionesTotales}}", comisionesTotales.ToString("C"));

    SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
    SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlCode);
    byte[] data = doc.Save();
    var result = Convert.ToBase64String(data);
    doc.Close();

    return result;
});

app.Run();

