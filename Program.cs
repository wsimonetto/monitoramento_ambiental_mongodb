<<<<<<< HEAD
=======
//using monitoramento_ambiental_mongodb.Data;
//using monitoramento_ambiental_mongodb.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Configura��es do MongoDB
//builder.Services.Configure<DataBaseSettings>(
//    builder.Configuration.GetSection("Database"));

//// Adiciona o contexto do MongoDB
//builder.Services.AddScoped<MongoDBContext>();

//// Adiciona os servi�os
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

>>>>>>> 8bdfd3e (Update Variáveis no Azure)
using DotNetEnv;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Services;

var builder = WebApplication.CreateBuilder(args);

// Carregar vari�veis de ambiente
Env.Load();

// Configura��es do MongoDB usando vari�veis de ambiente
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

// Adiciona os servi�os
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

