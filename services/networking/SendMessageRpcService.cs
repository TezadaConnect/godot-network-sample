using System.Linq;
using Godot;

public partial class SendMessageRpcService : NetWorkingService { // This class will end up being singleton
	
	public void TrasnmitMessage(string message){
		Rpc("SendAmessage", message, Multiplayer.GetUniqueId().ToString());
	}

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void SendAmessage(string message, string playerID){
		ColorRect rectNode = GetNode<ColorRect>("/root/Node/ChatBackground");

		Node2D holder = null;
		foreach (Node2D item in rectNode.GetChildren().Cast<Node2D>()){
			holder = item;
		}

		ChatBoxText chatBoxText = ResourceLoader.Load<PackedScene>("res://resources/drawables/chat_box_text.tscn").Instantiate<ChatBoxText>();
		
		if(holder != null){
			chatBoxText.Position = new Vector2(0, 70 + holder.Position.Y);
		}

		chatBoxText.GetNode<Label>("Label").Text = message;
		chatBoxText.GetNode<Label>("PlayerName").Text = "Player ID: " + playerID;
		GetNode<ColorRect>("/root/Node/ChatBackground").AddChild(chatBoxText);
		GetNode<LineEdit>("/root/Node/ChatInput").Text = "";

		if(rectNode.GetChildCount() <= 6){
			return;
		}
		rectNode.GetChildren().RemoveAt(0);
		foreach (Node2D item in rectNode.GetChildren().Cast<Node2D>()){
			item.Position = new Vector2(0, item.Position.Y - 70);
		}
	}
}