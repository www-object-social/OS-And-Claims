namespace Progress.manager;
public class Task:IDisposable
{
    private Action ActionChange = null!;
    public event Action Change
    {
        add => ActionChange += value;
#pragma warning disable CS8601 // Possible null reference assignment.
        remove => ActionChange -= value;
#pragma warning restore CS8601 // Possible null reference assignment.
    }
    private Status _Status = Status.Done;
    public Status Status {
        get => _Status;
        private set {
            this.manager.Update(_Status = value);
            this.ActionChange?.Invoke();
        }
    }
    private readonly Manager manager;
    internal Task(Manager manager)=> (this.manager = manager).Tasks.Add(this);
    public void Install() => Status = Status.Install;
    public void Done() => Status = Status.Done;
    public void Download() => Status = Status.Download;
    public void Cancel() => Status = Status.Download;
    public void InProcess() => Status = Status.InProcess;
    public void Dispose()
    {
        this.Done();
        this.manager.Tasks.Remove(this);
    }
}
