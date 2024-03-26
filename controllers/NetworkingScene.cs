using System.Linq;
using Godot;

public partial class NetworkingScene : Node{
	// private const int PORT = 8080;
	// private const string SERVER_ADDRESS = "127.0.0.1";
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
		string message = GetNode<LineEdit>("ChatInput").Text;
		Rpc("SendAmessage", message, Multiplayer.GetUniqueId());
	}
}
