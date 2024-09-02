using Microsoft.OpenApi.Models;
using PizzaStore.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza Santi API", Description = "Keep a documentation for our API's", Version = "v1" });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Santi API V1");
    });
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

app.MapPost("delay/{ms}", (int ms) => PizzaDB.Delay = ms);
app.MapGet("delay", () => PizzaDB.Delay);

app.MapPost("exception/{onOff}", (bool onOff) => PizzaDB.Error());

app.MapGet("/heartbeat", () => Heartbeat.GetHeartbeat());


var eventMonitor = new EventMonitor();


// Simula la recepción de un evento
app.MapPost("/event/{id}", (int id) =>
{
    eventMonitor.AddOrUpdateEvent(id);
    return Results.Ok($"Event {id} received.");
});

// Verifica el estado del evento
app.MapGet("/event/{id}/status", (int id) =>
{
    int waitTimeInSeconds = 15 ;
    bool isAlive = eventMonitor.IsEventStillAlive(id, waitTimeInSeconds);
    return Results.Ok(isAlive ? $"Event {id} is alive." : $"Event {id} has expired.");
});

app.Run("http://localhost:3000");
