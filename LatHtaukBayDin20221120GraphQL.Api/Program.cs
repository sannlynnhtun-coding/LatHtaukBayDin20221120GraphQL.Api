using Hangfire;
using LatHtaukBayDin20221120GraphQL.Api.Controllers;
using LatHtaukBayDin20221120GraphQL.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
//builder.Services.AddGraphQLServer()
//    .AddQueryType<MinTheinKhaQuery>()
//    .AddQueryType<CronQuery>();
builder.Services.AddGraphQLServer().AddQueryType(q => q.Name("Query"))
    .AddType<MinTheinKhaQuery>()
    .AddType<CronQuery>()
    .AddType<BlogQuery>()
    .AddType<waiwaikhaingQuery>()
    .AddType<PagingQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHangfireDashboard("/cron");

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
