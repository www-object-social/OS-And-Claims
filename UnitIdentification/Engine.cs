namespace UnitIdentification;
public class Engine
{
    private readonly PingPong.Engine PPE;
    private readonly IStorage S;
    public readonly Progress.manager.Task PmT;
    public Engine(Progress.Manager PM,PingPong.Engine PPE, IStorage S) {
        this.PPE = PPE;
        this.S = S;
        (this.PmT = PM.Register).Install();
        this.PPE.PmT.Change += PmT_Change;
    }

    private void PmT_Change()
    {
        if (PPE.PmT.Status is Progress.manager.Status.Done)
        {
            Console.WriteLine("Engine PmT_Change");
        }
        else this.PmT.Install();
    }
}
