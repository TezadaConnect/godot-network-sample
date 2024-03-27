using Godot;

public partial class ChatSceneController : Node{
	private SendMessageRpcService sendMessageRpcService;

	public override void _Ready(){
		sendMessageRpcService = GetNode<SendMessageRpcService>("/root/SendMessageRpcService");
		InitializeButtonListeners();
	}

	public void InitializeButtonListeners(){
		GetNode<Button>("CreateServerButton").Pressed += sendMessageRpcService.CreateAServer;
		GetNode<Button>("JoinServerButton").Pressed += sendMessageRpcService.JoinAServer;
		GetNode<Button>("SendButton").Pressed += OnPressedSendMessage;
	}

	public void OnPressedSendMessage(){
		LineEdit inputField = GetNode<LineEdit>("ChatInput");
		if(!IsValidText(inputField)){
			inputField.PlaceholderText = "This field can't be empty";
			return;
		}
		string message = inputField.Text;
		sendMessageRpcService.TrasnmitMessage(message);
		inputField.Text = "";
	}

	public bool IsValidText(LineEdit lineEdit){
		if(lineEdit.Text.Equals("")){
			lineEdit.PlaceholderText = "This field can't be empty";
			return false;
		}
		if(lineEdit.Text.Length > 40){
			lineEdit.PlaceholderText = "Character exceeded the max. number of 45";
			return false;
		}
		return true;
	}
}
