using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class CommunicationHub : Hub
{
    // Method for sending direct messages from one user to another
    public async Task SendDirectMessage(string recipientUserId, string senderUserId, string senderName, string message, string timestamp)
    {
        if (string.IsNullOrEmpty(recipientUserId))
        {
            Console.WriteLine("Error: recipientUserId is null or empty");
            return;
        }

        await Clients.User(recipientUserId).SendAsync("ReceiveDirectMessage", senderUserId, senderName, message, timestamp);
        Console.WriteLine($"Message sent from {senderUserId} to {recipientUserId}: {message}");
    }

    // Method to handle acknowledgments received from a client after receiving a message
    public Task SendReceivedAcknowledgment(string senderUserId, string receiverUserId, string acknowledgmentMessage)
    {
        Console.WriteLine($"Acknowledgment from {receiverUserId} to {senderUserId}: {acknowledgmentMessage}");
        return Task.CompletedTask;
    }

    // Method for sending files from one user to another
    public async Task SendFileMessage(string recipientUserId, string senderUserId, string senderName, string base64File, string fileType, string timestamp)
    {
        if (string.IsNullOrEmpty(recipientUserId))
        {
            Console.WriteLine("Error: recipientUserId is null or empty");
            return;
        }

        await Clients.User(recipientUserId).SendAsync("ReceiveFileMessage", senderUserId, senderName, base64File, fileType, timestamp);
        Console.WriteLine($"File sent from {senderUserId} to {recipientUserId}: {fileType}");
    }

    // Override method called when a user connects
    public override async Task OnConnectedAsync()
    {
        string userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        if (!string.IsNullOrEmpty(userId))
        {
            Console.WriteLine($"User connected with UserIdentifier: {userId}");
        }
        else
        {
            Console.WriteLine("User connected without a specific UserIdentifier.");
        }

        await Clients.Caller.SendAsync("ReceiveDirectMessage", "Server", "Server", "Welcome to the chat!", DateTime.Now.ToString("hh:mm:ss tt"));
        Console.WriteLine($"Client connected with ConnectionId: {Context.ConnectionId} and received a welcome message.");

        await base.OnConnectedAsync();
    }
}
