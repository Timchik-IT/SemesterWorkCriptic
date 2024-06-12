namespace Criptic.Core.Abstractions.Interfaces;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}