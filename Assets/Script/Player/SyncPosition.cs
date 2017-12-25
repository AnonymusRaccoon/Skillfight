using UnityEngine.Networking;
using UnityEngine;

public class SyncPosition : NetworkBehaviour
{
    [SyncVar]
    public Vector3 playerPosition;

    private void FixedUpdate()
    {
        SetPosition ();
        if (!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * 15);
        }
    }

    [Command]
    void CmdServerPosition (Vector3 position)
    {
        playerPosition = position;
    }

    [ClientCallback]
    void SetPosition ()
    {
        if (isLocalPlayer)
            CmdServerPosition(transform.position);
    }
}
