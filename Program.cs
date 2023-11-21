using PdfCompressionService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Set the application to listen on port 7777
// Configure Kestrel only for production
if (builder.Environment.IsProduction())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(7777); // Listen for HTTP requests on port 7777 in production
    });
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register PdfCompressionService
builder.Services.AddScoped<PdfCompressionService.Services.Interfaces.IPdfCompressionService, PdfCompressionService.Services.PdfCompressionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
