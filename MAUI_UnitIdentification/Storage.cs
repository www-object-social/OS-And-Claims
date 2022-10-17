namespace MAUI_UnitIdentification;
public class Storage : UnitIdentification.IStorage
{
    public Task<string> Read()
    {
        throw new NotImplementedException();
    }

    public Task Save(string Token, StandardInternal.unitIdentification.storage.Type Type)
    {
        throw new NotImplementedException();
    }

    public Task<StandardInternal.unitIdentification.storage.Type> Type()
    {
        throw new NotImplementedException();
    }
}