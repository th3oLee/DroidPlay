using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;  
using UnityEngine.UI;

public class TCPTestServer : MonoBehaviour {  	
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener; 
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;  	
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient; 	
	#endregion 	
    public string ip = "127.0.0.1";
    public int port = 42422;

	public RemoteServerSide[] remotes = new RemoteServerSide[4];
	int nbOfPlayer = 0;
	
	public int MaxPlayers = 4;

	//PRISE EN COMPTE DE LA MANETTE
	public static TCPTestServer instance;
	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(this);
	}
	
	// Use this for initialization
	void Start () { 		
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start();

		for(int i = 0; i < MaxPlayers; i++)
		{
			remotes[i] = new RemoteServerSide();
		}

	}

	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// Create listener on localhost port 8052. 			
			tcpListener = new TcpListener(IPAddress.Parse(ip), port); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  			
			while (true) { 				
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
						int length; 						
						// Read incomming stream into byte arrary. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 							
							var incommingData = new byte[length]; 							
							Array.Copy(bytes, 0, incommingData, 0, length);  							
							// Convert byte array to string message. 							
							string clientMessage = Encoding.ASCII.GetString(incommingData); 							
							//Debug.Log("client message received as: " + clientMessage); 
							processMessage(clientMessage);						
						} 					
					} 				
				} 			
			} 		
		} 		
		catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}  	
	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	private void SendMessage() { 		
		if (connectedTcpClient == null) {             
			return;         
		}  		
		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = connectedTcpClient.GetStream(); 			
			if (stream.CanWrite) {                 
				string serverMessage = "This is a message from your server."; 			
				// Convert string message to byte array.                 
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage); 				
				// Write byte array to socketConnection stream.               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				Debug.Log("Server sent his message - should be received by client");           
			}       
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		} 	
	} 

	private void processMessage(string message)
	{
		if(message.Contains("200"))
		{
			// Add Player
			//Debug.Log(message);
			string[] words = message.Split(';');
			addRemote(words[0]); 
		}
		if(message.Contains("201"))
		{
			// Last input
			Debug.Log(message);
			string[] words = message.Split(';');
			int PlayerId = findPlayerByName(words[0]);
			remotes[PlayerId].lastInput = words[2];
			remotes[PlayerId].isConsumed = false;
		}
		if(message.Contains("202"))
		{
			// Accelerometter
			//Debug.Log(message);
			string[] words = message.Split(';');

			Vector3 vec = new Vector3();
			vec.x = float.Parse(words[2]);
			vec.y = float.Parse(words[3]);
			vec.z = float.Parse(words[4]);


			int PlayerId = findPlayerByName(words[0]);
			remotes[PlayerId].accelerometter = vec;

		}
		if(message.Contains("203"))
		{
			//Debug.Log(message);
			string[] words = message.Split(';');
			Vector3 vec2 = new Vector3();
			vec2.x = float.Parse(words[2]);
			vec2.y = float.Parse(words[3]);
			vec2.z = float.Parse(words[4]);
			int PlayerId = findPlayerByName(words[0]);
			remotes[PlayerId].gravity = vec2;

		}
		if(message.Contains("204"))
		{
			//Debug.Log(message);
			string[] words = message.Split(';');
			Quaternion att = new Quaternion();
			att.x = float.Parse(words[2]);
			att.y = float.Parse(words[3]);
			att.z = float.Parse(words[4]);
			att.w = 0.5f;//float.Parse(words[5]);

			int PlayerId = findPlayerByName(words[0]);
			remotes[PlayerId].attitude = att;

		}

	}

	private void addRemote(string playerName)
	{
		RemoteServerSide remote = new RemoteServerSide();
		remote.name = playerName;
		remote.isConnected = true;
		remotes[nbOfPlayer] = remote;

		//PRISE EN COMPTE DE LA MANETTE QUI JOUE
		GameControl.instance.RegisterRemote(remote);

		nbOfPlayer++;
	}

	private int findPlayerByName(string name)
	{
		for(int i = 0; i < MaxPlayers; i++)
		{
			if(remotes[i].name == name)
			{
				return i;
			}
		}
		return 0;
	}
}