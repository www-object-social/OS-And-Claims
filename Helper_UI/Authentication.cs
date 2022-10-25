using Helper_UI.authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Product.infomation;

namespace Helper_UI;
public class Authentication
{
	private Action ChangeAction = null!;
	public event Action Change {
		add => ChangeAction += value;
		remove => ChangeAction -= value;
	}
	private Do _Do = Do.Authentication;
	public Do Do {
		get => _Do;
		private set {
			if (value == _Do) return;
			_Do = value;  
			this.ChangeAction?.Invoke();
		}
	}
	public void ChangeToPrefix() => Do = Do.Prefix;
	public void ChangeToAuthentication() {
		Email = "";
		Mobile = "";
		this.CancelToAuthentication();
	}
	public void CancelToAuthentication()=> Do = Do.Authentication;
	private string _Email = "";
	private string _Mobile = "";
	public string Email {
		get => _Email;
		set { 
			if(_Email == value) return;
			_Email = value;
			this.ChangeAction?.Invoke();
		}
	}
	public string Mobile
	{
		get => _Mobile;
		set
		{
			if (_Mobile == value) return;
			_Mobile = value;
			this.ChangeAction?.Invoke();
		}
	}
	public void SetPrefix(string ISO3166) {
		this.UIE.ISO3166 = ISO3166;
		this.CancelToAuthentication();
	}
	public async void CreateAnAccount() {
		this.PmT.Install();
		await this.PPE.Hub.InvokeAsync("A_C");
		this.PmT.Done();
	}
	private readonly UnitIdentification.Engine UIE;
	private readonly PingPong.Engine PPE;
	private readonly Progress.manager.Task PmT;
	public Authentication(UnitIdentification.Engine UIE,PingPong.Engine PPE,Progress.Manager PM) {
		this.UIE = UIE;
		this.PPE = PPE;
		this.PmT = PM.Register;
	}
}