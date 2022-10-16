namespace Progress;
public class Manager
{
    private Action ActionChange = null!;
    public event Action Change
    {
        add => ActionChange += value;
        remove => ActionChange -= value;
    }
    public manager.Status Status { get; private set; } = manager.Status.Startup;

    internal async void Update(manager.Status status) {
        if (status == this.Status||(this.Status is manager.Status.Startup&&Tasks.Any(x=>x.Status is manager.Status.Install or manager.Status.Download or manager.Status.Cancel))||(this.Status is manager.Status.Install&& status is manager.Status.Download))return;
        await Task.Delay(20);
        this.Status = Tasks.Any(x => x.Status is manager.Status.Install or manager.Status.Cancel) ? manager.Status.Install : Tasks.Any(x => x.Status is manager.Status.Download) ? manager.Status.Download : manager.Status.Done;
        this.ActionChange?.Invoke();
    }
    public Manager(Unit.IInfomation UI) {
        UI.Change += () => {
            foreach (var T in Tasks.Where(x => x.Status == manager.Status.Download))
                T.Cancel();
        };
    }
    internal List<manager.Task> Tasks = new();
    public manager.Task Register => new(this);
}