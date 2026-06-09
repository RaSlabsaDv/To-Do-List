using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddCors(opt => opt.AddPolicy("angular", policy =>
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials())); 

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("angular");

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(err => err.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    var (status, message) = exception switch
    {
        NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
        ConflictException => (StatusCodes.Status409Conflict, exception.Message),
        _ => (StatusCodes.Status500InternalServerError, "Internal server error")
    };

    context.Response.StatusCode = status;
    await context.Response.WriteAsJsonAsync(new { error = message });
}));

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();