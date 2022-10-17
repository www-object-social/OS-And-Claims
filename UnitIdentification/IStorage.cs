namespace UnitIdentification;
public interface IStorage
{
    public Task<StandardInternal.unitIdentification.storage.Type> Type();
    public Task Save(string Token, StandardInternal.unitIdentification.storage.Type Type);
    public Task<string> Read();
}