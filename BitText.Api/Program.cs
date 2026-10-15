var builder = WebApplication.CreateBuilder(args);

// --- 1. PREPARAÇÃO (A COZINHA) ---
builder.Services.AddOpenApi();     // O que já estava no seu código
builder.Services.AddControllers(); // Ensina a API a ler a sua pasta Controllers
builder.Services.AddMemoryCache(); // A memória para guardar a letra (ANTES DO BUILD!)

// --- 2. INAUGURAÇÃO ---
var app = builder.Build();

// --- 3. REGRAS DO APP ---
app.MapControllers(); // Cria o mapa para achar o seu GameController

app.Run(); // Liga o servidor