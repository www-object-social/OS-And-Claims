namespace Product;
public class Infomation
{
    public infomation.Software Software { get; init; }
    public StandardInternal.product.infomation.Name Name { get; init; }
    public string[] ISO639_1s => new[] { "EN", "DA" };
    public bool ISDeveloper { get; set; } = false;
}