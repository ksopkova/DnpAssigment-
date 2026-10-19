using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Now, a Controller can request any of the I*Repository interfaces, as needed, and the Dependency Injection functionality will handle creation for us
builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();
builder.Services.AddScoped<IUserRepository, UserFileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger"));
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
