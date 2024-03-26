using Godot;

public partial class NetworkingScene : Node{
	private const int PORT = 8080;
	private const string SERVER_ADDRESS = "127.0.0.1";

	public override void _Ready(){
		// Connect the function to the signal
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerConnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
		Multiplayer.ServerDisconnected += ServerDisconnected;
	}

	/*
	* *********************************
	*  JOINING AND CREATING SERVER
	* *********************************
	*/
	public void CreateAServer(){
		ENetMultiplayerPeer peer = new();
		peer.CreateServer(PORT);
		Multiplayer.MultiplayerPeer = peer;
	}

	public void JoinAServer(){
		ENetMultiplayerPeer peer = new();
		peer.CreateClient(SERVER_ADDRESS, PORT);
		Multiplayer.MultiplayerPeer = peer;
	}

	// TO TERMINATE NETWORKING JUST SHIT NULL THE Multiplayer.MultiplayerPeer

	/*
	* **********************************************
	*  NETWORK CONNECTION RESPONSE TO SERVER
	* **********************************************
	*/
	public void PeerConnected(long id){
		string userID = id.ToString();
		GD.Print("User: " + userID + " connected to the game");
	}

	public void PeerDisconnected(long id){
		string userID = id.ToString();
		GD.Print("User: " + userID + " disconnected to the game");
	}

	/*
	* **********************************************
	*  NETWORK CONNECTION RESPONSE TO CLIENT
	* **********************************************
	*/
	public void ConnectedToServer(){
		GD.Print("Connected to the server.");
	}

	public void ConnectionFailed(){
		GD.Print("Failed to connect to the server.");
	}

	public void ServerDisconnected(){
		GD.Print("Server has disconnected,");
	}

	/*
	* **********************************************
	*  FOR GETTING USERID OF THE APPLICATION
	* **********************************************
	*/
	public long GetUserID(){
		return Multiplayer.GetUniqueId();
	}

	/*
	* **********************************************
	*  TO KNOW IF THE APPLICATION HOST A SERVER
	* **********************************************
	*/
	public bool GetIsServer(){
		return Multiplayer.IsServer();
	}



}
