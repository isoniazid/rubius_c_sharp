public static class Starter
{
    public static readonly byte[] ticketHashKey = Encoding.UTF8.GetBytes("Это ключ для хеширования билетов");
    public static readonly byte[] passwordHashKey = Encoding.UTF8.GetBytes("Это ключ для хеширования паролей");
    public static readonly byte[] RefreshHashKey = Encoding.UTF8.GetBytes("Это ключ для хеширования рефреш токенов");

    public static readonly TimeSpan AccessTokenTime = new TimeSpan(0, 30, 0);
    public static readonly TimeSpan RefreshTokenTime = new TimeSpan(30, 0, 0, 0);
    public static readonly TimeSpan RefreshTokenThreshold = new TimeSpan(5, 0, 0, 0);

    public static void AddAuthInSwagger(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions setup)
    {
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
            { jwtSecurityScheme, Array.Empty<string>() }
                });

        }
    }

    public static void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(ApplicationProfile));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup => AddAuthInSwagger(setup));

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
        });

        builder.Services.AddScoped<IHallService, HallService>();
        builder.Services.AddScoped<IBandService, BandService>();
        builder.Services.AddScoped<IConcertService, ConcertService>();
        builder.Services.AddScoped<IViewerService, ViewerService>();
        /* 
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();*/
        //builder.Services.AddSingleton<ITokenService> (new TokenService()); 

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = ConfigureJwtBearer(builder));




        builder.Services.AddCors();
    }


    public static TokenValidationParameters ConfigureJwtBearer(WebApplicationBuilder builder)
    {
        return new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new Exception("Отсутствует ключ!")))
        };
    }

    public static void Configure(WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }


        app.UseCors(builder =>
        {
            builder.WithOrigins("http://localhost:3000");
            builder.AllowCredentials();
            builder.AllowAnyHeader();
        });

        app.MapControllers();
    }

}