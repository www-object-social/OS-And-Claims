namespace StandardInternal.pongPing.uniformResource;
public interface IIdentifier
{
    DateTime Created { get; }
    Guid ID { get; }
    int LimitOfConnection { get; }
    string Path { get; }
    DateTime Use { get; }
    int UsedConnections { get; }
}