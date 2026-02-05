using CamelRegistry;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//SETTING UP DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"))
   .ExcludeFromDescription();

//CRUD OPERATIONS
//READ ALL
app.MapGet("/camels", async (AppDbContext ctx) => 
	await ctx.Camels.ToListAsync())
	.WithTags("Read Camels");

//READ
app.MapGet("/camels/{id}", async (AppDbContext ctx, int id) =>
	await ctx.Camels.FindAsync(id)
		is Camel camel
		? Results.Ok(camel)
		: Results.NotFound()).WithTags("Read Camel");

//CREATE
app.MapPost("/camels", async (AppDbContext ctx, CreateCamelDto camelDto) =>
{
	if (camelDto.HumpCount >= 1 && camelDto.HumpCount <= 2)
	{
		Camel newCamel = new Camel()
		{
			Name = camelDto.Name,
			Color = camelDto.Color,
			HumpCount = camelDto.HumpCount,
			LastFed = camelDto.LastFed
		};
		ctx.Camels.Add(newCamel);
		await ctx.SaveChangesAsync();
		return Results.Created($"/camels/{newCamel.Id}", newCamel);
	}
	return Results.BadRequest();
}).WithTags("Create Camel");

//UPDATE
app.MapPut("/camels/{id}", async (AppDbContext ctx, int id, Camel updatedCamel) =>
{
	Camel oldCamel = await ctx.Camels.FindAsync(id);
	if (oldCamel is null) return Results.NotFound();

	foreach (var prop in typeof(Camel).GetProperties())
	{
		if (!prop.Name.Equals("Id"))
		{
			var newValue = prop.GetValue(updatedCamel);
			prop.SetValue(oldCamel, newValue);
		}
	}

	await ctx.SaveChangesAsync();
	return Results.Ok();
}).WithTags("Update Camel");

//DELETE
app.MapDelete("/camels/{id}", async (AppDbContext ctx, int id) =>
{
	if (await ctx.Camels.FindAsync(id) is Camel camel)
	{
		ctx.Camels.Remove(camel);
		await ctx.SaveChangesAsync();
		return Results.NoContent();
	}
	return Results.NotFound();
}).WithTags("Delete Camel");

app.Run();