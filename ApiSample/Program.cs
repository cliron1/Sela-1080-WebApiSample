using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container... and added some test here



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidIssuer = "Sela",
            ValidateIssuer = true,

            ValidAudience = "Website",
            ValidateAudience = false,

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:Secret"]))
        };
        options.Events = new JwtBearerEvents {
            OnAuthenticationFailed = context => {
                return Task.CompletedTask;
            }
        };
    });
/* ============================================ */

builder.Services.AddCors(options => {
    options.AddPolicy(name: "mycors",
        policy => {
            policy
            .WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader();
            //.WithHeaders("content-type", "authorization");
            //.AllowAnyOrigin()
            //.AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Please enter a JWT token..."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            new string[] {}
        }
    });

    c.IncludeXmlComments(
        Path.Combine(AppContext.BaseDirectory, "ApiSample.xml")
    );
});


/* ================================================= */

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.DocumentTitle = "Sela - Class 1080 - API Docs";
    });
}

app.UseHttpsRedirection();

//app.UseStaticFiles(); // wwwroot
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory),
    RequestPath = new PathString("")
});


app.UseCors("mycors");

app.UseAuthentication(); // analogy: Hotel check in
app.UseAuthorization(); // analogy: Only your Room

app.MapControllers()
    .RequireAuthorization();

app.Run();
