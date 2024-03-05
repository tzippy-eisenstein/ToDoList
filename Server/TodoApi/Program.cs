using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//cors
builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder =>
    {
      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }
));
builder.Services.AddSwaggerGen();
var app = builder.Build();

//swagger
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//get all list
app.MapGet("/", async (ToDoDbContext db) =>
{
  var d = await db.Items.ToListAsync();
  return Results.Ok(d);
});

//add item
app.MapPost("/{name}", async (string name, ToDoDbContext db) =>
{
  Item temp = new Item();
  temp.Name = name;
  temp.IsComplete = false;
  db.Items.Add(temp);
  await db.SaveChangesAsync();
});

//set item to complete
app.MapPut("/{id}/{isComplete}", async (int id, bool IsComplete, ToDoDbContext db) =>
{
  var todo = await db.Items.FindAsync(id);
  if (todo is null) return Results.NotFound();
  else
  {
    todo.IsComplete = IsComplete;
  }

  await db.SaveChangesAsync();
  return Results.NoContent();
});

//delete item
app.MapDelete("/{id}", async (int id, ToDoDbContext db) =>
{
  if (await db.Items.FindAsync(id) is Item item)
  {
    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
  }
  return Results.NotFound();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.Run();