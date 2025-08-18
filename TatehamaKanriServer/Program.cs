using TatehamaKanriServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

// CORSポリシーを追加
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
                      builder =>
                      {
                          // スマートフォンアプリやWebからの接続を許可する
                          builder.SetIsOriginAllowed(origin => true)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.MapHub<ConnectionHub>("/connectionHub");

app.Run();
