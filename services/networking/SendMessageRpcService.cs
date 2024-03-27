using System.Linq;
using Godot;

public partial class SendMessageRpcService : NetWorkingService {
	private ColorRect _chatBackground;
    public override void _Ready(){
        base._Ready();
		// Chat scene chat background
		_chatBackground = GetNode<ColorRect>("/root/Node/ChatBackground");
    }
    public void TrasnmitMessage(string message){
		Rpc(nameof(SendAmessage), message, GetUserID());
	}

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void SendAmessage(string message, string playerID){
		ChatContainer chatContainer = ResourceLoader.Load<PackedScene>("res://resources/drawables/chat_container.tscn").Instantiate<ChatContainer>();
		Label chatMessage = chatContainer.GetNode<Label>("Label");
		Label playerName = chatContainer.GetNode<Label>("PlayerName");
		
		Node2D holder = null;
		foreach (Node2D item in _chatBackground.GetChildren().Cast<Node2D>()){
			holder = item;
		}
		
		if(holder != null){
			chatContainer.Position = new Vector2(0, 70 + holder.Position.Y);
		}

		chatMessage.Text = message;
		playerName.Text = "Player ID: " + playerID;

		int senderID = Multiplayer.GetRemoteSenderId();

		if(senderID == int.Parse(GetUserID())){
			Color color = new("#772345");
			chatContainer.GetNode<ColorRect>("Background").Color = color;
			playerName.Text = "Player ID: Me";
		}

		_chatBackground.AddChild(chatContainer);

		if(_chatBackground.GetChildCount() <= 6){
			return;
		}

		ChatContainer containerForDelete = (ChatContainer)_chatBackground.GetChildren().ElementAt(0);
		containerForDelete.Free();

		foreach (Node2D item in _chatBackground.GetChildren().Cast<Node2D>()){
			item.Position = new Vector2(0, item.Position.Y - 70);
		}
	}
}