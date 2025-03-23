using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Repository;
using ToDoList.Service;
using ToDoList.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Register Service
builder.Services.AddScoped<ITaskService, TaskService>();

// Register Validation
builder.Services.AddScoped<ITaskValidation, TaskValidation>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();