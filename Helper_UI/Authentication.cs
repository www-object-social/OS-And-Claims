using Helper_UI.authentication;
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
		this.ChangeToAuthentication();
	}
	private readonly UnitIdentification.Engine UIE;
	public Authentication(UnitIdentification.Engine UIE) {
		this.UIE = UIE;
	}
}