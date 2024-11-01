using Microsoft.AspNetCore.SignalR;
using WebApplication1.hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback) // Allows any localhost
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Register CustomUserIdProvider
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Apply CORS globally
app.UseCors("AllowLocalHost");

app.UseAuthorization();
app.MapRazorPages();
app.MapHub<CommunicationHub>("/communicationHub");

app.Run();
