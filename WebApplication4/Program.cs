using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(OP =>OP.UseSqlServer(builder.Configuration.GetConnectionString("Constr")));

builder.Services.AddControllers();

var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);//????? Key ?????
builder.Services.AddAuthentication(b =>
{
    b.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// ????? ????? ?? ??? ????? ????
    b.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;// ????? ??? ?? ?? ????? ????? ????
}).AddJwtBearer(op => op.TokenValidationParameters = new TokenValidationParameters//????? token f?????
{
    ValidateIssuerSigningKey = true,//????? ??? ????? ???? ?? json ??
    IssuerSigningKey = new SymmetricSecurityKey(Key),//????? ???? ????? ????
    ValidateIssuer = true,//?? ???? ??? ?? ?? Issure
    ValidIssuer = builder.Configuration["Jwt:Issuer"],//
    ValidateAudience = true,
    ValidAudience = builder.Configuration["Jwt:Audience"],//?? ???? ??? ?? ?? Audience
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
