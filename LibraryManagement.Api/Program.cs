using LibraryManagement.Application;
using LibraryManagement.Application.DTO;
using LibraryManagement.Domain;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books/{id}", async (Guid id, [FromServices] IBookService service) =>
    {
        try
        {
            return Results.Ok(await service.GetBookByIdAsync(id));
        }
        catch (BookNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .WithOpenApi();

app.MapPost("/books", async (BookDto dto, [FromServices] IBookService service) =>
{
    try
    {
        await service.AddBookAsync(dto);
        return Results.Created();
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
}).WithOpenApi();


app.MapGet("/books", async ([FromServices] IBookService service) =>
{
    try
    {
        return Results.Ok(await service.GetAllBooksAsync());
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
}).WithOpenApi();

app.MapPut("/books/update", async ([FromBody] Book book, [FromServices] IBookService service) =>
    {
        try
        {
            await service.UpdateBookAsync(book);
            return Results.NoContent();
        }
        catch (BookNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .WithOpenApi();


app.MapDelete("/books/{id}/delete", async (Guid id, [FromServices] IBookService service) =>
    {
        try
        {
            await service.DeleteBookAsync(id);
            return Results.NoContent();
        }
        catch (BookNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .WithOpenApi();

app.Run();