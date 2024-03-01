using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Enable CORS
app.UseCors(builder => builder.WithOrigins(
	"http://localhost:3000/"
	).AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

//loads .env file (by default it tries to read from 'appsettings.json')
Env.Load();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
