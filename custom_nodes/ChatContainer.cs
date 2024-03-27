using Godot;

public partial class ChatContainer : Node2D{
	public override void _Ready(){
		GetNode<AnimationPlayer>("Popup").Play("popup");
	}
}
