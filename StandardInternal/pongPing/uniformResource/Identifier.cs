namespace StandardInternal.pongPing.uniformResource;
public class Identifier : IIdentifier
{
    public string Path { get; set; } = null!;
    public int LimitOfConnection { get; set; }
    public DateTime Created { get; set; }
    public DateTime Use { get; set; }
    public Guid ID { get; set; }
    public int UsedConnections { get; set; }

}
