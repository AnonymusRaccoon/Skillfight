// using System.Collections;
// using UnityEngine.Networking;
// using UnityEngine;

// public class HostMigration : NetworkBehaviour {

	// [SerializeField]
	// private GameObject prefab;

	// public void OnServerHostShutdown ()
	// {
		// Network.Player[] netPlayers = Network.connections;
		// float minPing = 5000;
		// Network.Player newHost = null;
		// foreach(Network.Player netPlayer in netPlayers)
		// {
			// if (Network.GetAveragePing(netPlayer) < minPing)
			// {
				// minPing = Network.GetAveragePing(netPlayer);
				// newHost = netPlayer;
			// }
		// }
		// if (netPlayer.isLocalPlayer)
		// {
			// Network.InitializeServer (8, 7777, true);
		// }
		// else
		// {
			// Network.Connect(newHost.ipAddress, 7777);
			// CmdAddPlayer (netPlayer);
		// }
	// }
	// [Command]
	// private void CmdAddPlayer (NetworkPlayer netPlayer)
	// {
		// GameObject player = GameObject.Instantiate(player);
		// NetworkServer.AddPlayerForConnection(nePlayer, player);
	// }
// }
