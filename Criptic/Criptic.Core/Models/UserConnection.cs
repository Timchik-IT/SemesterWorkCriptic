namespace Criptic.Core.Models;

public class UserConnection
{
    public UserConnection(string userName, string chatRoom)
    {
        UserName = userName;
        ChatRoom = chatRoom;
    }
    
    public string UserName { get; }

    public string ChatRoom { get; }
}