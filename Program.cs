<<<<<<< HEAD
=======
//using monitoramento_ambiental_mongodb.Data;
//using monitoramento_ambiental_mongodb.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Configurações do MongoDB
//builder.Services.Configure<DataBaseSettings>(
//    builder.Configuration.GetSection("Database"));

//// Adiciona o contexto do MongoDB
//builder.Services.AddScoped<MongoDBContext>();

//// Adiciona os serviços
//builder.Services.AddScoped<SensorService>();
//builder.Services.AddScoped<LeituraService>();
//builder.Services.AddScoped<PrevisaoChuvaService>();
//builder.Services.AddScoped<AlertaService>();
//builder.Services.AddScoped<ControleIrrigacaoService>();


//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();

>>>>>>> 8bdfd3e (Update VariÃ¡veis no Azure)
using DotNetEnv;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Services;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente
Env.Load();

// Configurações do MongoDB usando variáveis de ambiente
builder.Services.Configure<DataBaseSettings>(options =>
{
    options.ConnectionURI = Environment.GetEnvironmentVariable("MONGODB_URI")!;
    options.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
    options.SensorCollectionName = Environment.GetEnvironmentVariable("SENSOR_COLLECTION_NAME")!;
    options.LeituraCollectionName = Environment.GetEnvironmentVariable("LEITURA_COLLECTION_NAME")!;
    options.ControleIrrigacaoCollectionName = Environment.GetEnvironmentVariable("CONTROLE_IRRIGACAO_COLLECTION_NAME")!;
    options.AlertaCollectionName = Environment.GetEnvironmentVariable("ALERTA_COLLECTION_NAME")!;
    options.PrevisaoChuvaCollectionName = Environment.GetEnvironmentVariable("PREVISAO_CHUVA_COLLECTION_NAME")!;
});
// Adiciona o contexto do MongoDB
builder.Services.AddScoped<MongoDBContext>();

// Adiciona os serviços
builder.Services.AddScoped<SensorService>();
builder.Services.AddScoped<LeituraService>();
builder.Services.AddScoped<PrevisaoChuvaService>();
builder.Services.AddScoped<AlertaService>();
builder.Services.AddScoped<ControleIrrigacaoService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

