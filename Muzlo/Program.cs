var builder = WebApplication.CreateBuilder(args);



Starter.RegisterServices(builder);

var app = builder.Build();


Starter.Configure(app);


app.Run();