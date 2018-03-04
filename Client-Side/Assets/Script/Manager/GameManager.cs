using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    [SerializeField]
    private GameObject DartPrefab;
    [SerializeField]
    private float DartSpeed = 15;

    [Space]
    [SerializeField]
    private GameObject LinePrefab;

    [Command]
    public void CmdMidShot(string shooterID)
    {
        RpcMidShot(shooterID);
    }

    [ClientRpc]
    public void RpcMidShot(string shooterID)
    {
        GameObject player = GameObject.Find(shooterID);
		Transform castPosition = player.transform.Find("Cast");
        GameObject Dart = Instantiate(DartPrefab, castPosition.position, player.transform.rotation);
        Dart.transform.eulerAngles = player.transform.eulerAngles;
        Dart.transform.localEulerAngles = new Vector3(Dart.transform.rotation.x, Dart.transform.rotation.y - 90, Dart.transform.rotation.z);
        //Rigidbody DartRb = Dart.GetComponent<Rigidbody>();
        //DartRb.AddForce(Dart.transform.forward * DartSpeed);
        Dart.transform.name = "Dart" + shooterID;
        StartCoroutine(RemoveDart(Dart));
    }

    private IEnumerator RemoveDart(GameObject Dart)
    {
        yield return new WaitForSeconds(7);
        if (Dart != null)
            Destroy(Dart);
    }

    [Command]
    public void CmdLongShot(string shooterID)
    {
        RpcLongShot(shooterID);
    }

    [ClientRpc]
    public void RpcLongShot(string shooterID)
    {
        GameObject player = GameObject.Find(shooterID);
		Transform castPosition = player.transform.Find("Cast");
        GameObject Line = Instantiate(LinePrefab, castPosition.position, player.transform.rotation);
        LineRenderer lineRenderer = Line.GetComponent<LineRenderer>();
        RaycastHit hit;
        if (Physics.Raycast(castPosition.position, player.transform.forward, out hit, 100))
        {
            Vector3[] posLineRenderer = new Vector3[] { player.transform.forward, hit.transform.position };
            lineRenderer.SetPositions(posLineRenderer);
            if (hit.collider.transform.parent.gameObject.tag == "Player")
            {
                gameObject.GetComponent<Hp>().CmdDamage(350, hit.collider.transform.parent.name, shooterID);
            }
        }
        //else
        //{
        //    Vector3[] posLineRenderer = new Vector3[] { castPosition.forward, forward };
        //    lineRenderer.SetPositions(posLineRenderer);
        //}
        StartCoroutine(RemoveLine(Line));
    }

    private IEnumerator RemoveLine (GameObject Line)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(Line);
    }
	
	[Command]
	public void CmdDash (string playerID)
	{
		RpcDash (playerID);
	}
	
	[ClientRpc]
	public void RpcDash (string playerID)
	{
		GameObject player = GameObject.Find(playerID);
		player.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 15, ForceMode.Impulse);
		StartCoroutine (stopDash(playerID));
	}

	public IEnumerator stopDash (string playerID)
	{
		yield return new WaitForSeconds (0.8f);
		GameObject.Find(playerID).GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
	}
	
	[Command]
	public void CmdTpShortRange (string playerID)
	{
		RpcTpShortRange (playerID);
	}
	
	[ClientRpc]
	public void RpcTpShortRange (string playerID)
	{
		StartCoroutine (startTp (playerID));
	}
	
	public IEnumerator startTp (string playerID)
	{
		yield return new WaitForSeconds (3);
		GameObject player = GameObject.Find(playerID);
		Transform castPosition = player.transform.Find("Cast");
		RaycastHit hit;
        if (Physics.Raycast(castPosition.position, player.transform.forward, out hit, 15))
		{
			player.transform.position = hit.transform.position;
		}
	}
	
	[Command]
	public void CmdInvisibility (string playerID, bool invisibility)
	{
		RpcInvisbility(playerID, invisibility);
	}
	
	[ClientRpc]
	public void RpcInvisbility (string playerID, bool invisibility)
	{
		GameObject player = GameObject.Find(playerID);
		GameObject mesh = player.transform.Find("Mesh").gameObject;
        CapsuleCollider collider = player.GetComponentInChildren<CapsuleCollider>();
		if (invisibility)
		{
			mesh.SetActive(false);
			collider.enabled = false;
			StartCoroutine(noInvisibility (playerID));
		}
		else
		{
			mesh.SetActive(true);
			collider.enabled = true;
			//Mute
		}
	}
	
	public IEnumerator noInvisibility (string playerID)
	{
		yield return new WaitForSeconds (5);
		CmdInvisibility(playerID, false);
	}
}
