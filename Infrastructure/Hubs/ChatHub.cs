using Application.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

//[Authorize]
public class ChatHub : Hub
{
    private readonly Dictionary<string, string> _connections = new();
    
    public async Task JoinChatRoom(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
    public async Task SendMessage(Guid chatId, string content)
    {
        await Clients.OthersInGroup(chatId.ToString()).SendAsync("ReceiveMessage", content);
    }
}