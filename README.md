
<body>

<h1>Direct Messaging in RealTimeChatHub</h1>

<p>
    In this project, <strong>RealTimeChatHub</strong>, the chat functionality is specifically designed for <strong>direct messaging</strong> between users. Direct messaging allows users to send private messages to each other in real-time.
</p>

<h2>What is Direct Messaging?</h2>
<p>
    Direct messaging (DM) is a private communication channel where messages are sent directly from one user to another. In this project, each user has a unique <code>userId</code> which allows the server to route messages specifically to the intended recipient. Only the recipient with the matching <code>userId</code> will receive the message, ensuring privacy and exclusivity of the conversation.
</p>

<h2>How Direct Messaging Works in RealTimeChatHub</h2>

<ol>
    <li>
        <strong>Unique User Identification:</strong>
        <p>
            Each user connects to the <code>CommunicationHub</code> with a unique <code>userId</code>, passed as a query parameter in the connection URL. The <code>CustomUserIdProvider</code> extracts this <code>userId</code> and associates it with the user’s connection in SignalR. This enables the server to recognize each user individually.
        </p>
    </li>
    <li>
        <strong>Sending Direct Messages:</strong>
        <p>
            The <code>SendDirectMessage</code> method in <code>CommunicationHub</code> allows one user to send a message specifically to another user by targeting their <code>userId</code>. When a user sends a message, they specify the <code>recipientUserId</code> (the target user) along with their own <code>senderUserId</code>, <code>senderName</code>, <code>message</code>, and <code>timestamp</code>.
        </p>
        <p>
            SignalR’s <code>Clients.User(recipientUserId)</code> method sends the message only to the user with the specified <code>recipientUserId</code>.
        </p>
    </li>
    <li>
        <strong>Receiving Messages:</strong>
        <p>
            The targeted recipient receives the message through the <code>ReceiveDirectMessage</code> method on the client-side, displaying it in real-time without any delay. This method also works for sending files, where the <code>SendFileMessage</code> function uses the same approach to deliver files directly from one user to another.
        </p>
    </li>
    <li>
        <strong>Ensuring Privacy:</strong>
        <p>
            Since messages are directed to a specific <code>userId</code>, no other users connected to the <code>CommunicationHub</code> can see or receive these messages. This setup ensures that all communications remain private between the sender and recipient.
        </p>
    </li>
</ol>

<h2>Example Code Snippet for Direct Messaging</h2>

<pre>
<code>
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
</code>
</pre>

<h2>Summary of Direct Messaging in RealTimeChatHub</h2>
<ul>
    <li><strong>Private Communication:</strong> Messages are routed to specific users based on their <code>userId</code>, ensuring that only the intended recipient can see the message.</li>
    <li><strong>Real-Time Delivery:</strong> Leveraging SignalR, messages are delivered instantly to the recipient, creating a real-time chat experience.</li>
    <li><strong>User-Specific Routing:</strong> The <code>CustomUserIdProvider</code> and <code>Clients.User()</code> method ensure messages are sent directly and privately.</li>
</ul>

</body>
<h2>develpoed by Sifiso Vinjwa</h2>
</html>
