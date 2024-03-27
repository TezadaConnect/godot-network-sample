using Godot;

public partial class ChatSceneController : Node{
	private SendMessageRpcService sendMessageRpcService;
	private LineEdit _inputField;

	public override void _Ready(){
		sendMessageRpcService = GetNode<SendMessageRpcService>("/root/SendMessageRpcService");
		InitializeButtonListeners();
		InitializeUiBindings();
	}

	public void InitializeButtonListeners(){
		GetNode<Button>("CreateServerButton").Pressed += sendMessageRpcService.CreateAServer;
		GetNode<Button>("JoinServerButton").Pressed += sendMessageRpcService.JoinAServer;
		GetNode<Button>("SendButton").Pressed += OnPressedSendMessage;
	}

	public void InitializeUiBindings(){
		_inputField = GetNode<LineEdit>("ChatInput");
	}

	public void OnPressedSendMessage(){
		if(!IsValidText(_inputField)){
			return;
		}
		string message = _inputField.Text;
		sendMessageRpcService.TrasnmitMessage(message);
		_inputField.Text = "";
	}

	public bool IsValidText(LineEdit lineEdit){
		if(lineEdit.Text.Equals("")){
			lineEdit.PlaceholderText = "This field can't be empty.";
			return false;
		}
		if(lineEdit.Text.Length > 40){
			lineEdit.PlaceholderText = "Character exceeded the max. number of 40.";
			return false;
		}
		return true;
	}
}
