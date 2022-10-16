namespace PingPong;
public class Engine
{
    private readonly Product.Infomation PI;
    private readonly Unit.IInfomation UI;
    public readonly Progress.manager.Task PmT;
    public Engine(Product.Infomation PI, Unit.IInfomation UI, Progress.Manager PM) {
        this.PI = PI;
        (this.PmT = PM.Register).Install(); 
        (this.UI = UI).Change += UI_Change;
        this.UI_Change();
    }

    private void UI_Change()
    {
        Console.WriteLine("UI_Change");
        if (this.UI.Network is Unit.infomation.Network.Online)
        {

        }
        else this.PmT.Cancel(); 
    }
}