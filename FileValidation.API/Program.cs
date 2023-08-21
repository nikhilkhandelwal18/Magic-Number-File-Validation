var builder = WebApplication.CreateBuilder(args);

#region "Add services to the container"

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseSwaggerUI(options =>
    //{
    //    // build a swagger endpoint for each discovered API version
    //    foreach (var description in provider.ApiVersionDescriptions)
    //    {
    //        options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
    //            description.GroupName.ToUpperInvariant());
    //    }
    //});

    //app.UseSwaggerUI(options =>
    // {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
    //     options.RoutePrefix = string.Empty;
    // });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
