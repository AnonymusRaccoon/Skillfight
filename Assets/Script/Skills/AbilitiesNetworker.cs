using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class AbilitiesNetworker : NetworkBehaviour {

    [Command]
    public void CmdUpdateSkin(string SkinString, GameObject parentObject)
    {
        GameObject Skin = Instantiate(Resources.Load(SkinString, typeof(GameObject))) as GameObject;
        NetworkServer.SpawnWithClientAuthority(Skin, parentObject);
        RpcUpdateSkin(SkinString, parentObject, Skin);
    }

    [ClientRpc]
    void RpcUpdateSkin(string SkinString, GameObject parentObject, GameObject Skin)
    {
        Skin.transform.SetParent(parentObject.transform);
        Skin.transform.localPosition = new Vector3(0, -0.992f, 0);

        string oldSkin = parentObject.GetComponent<SetupLocalPlayer>().OldPassif;
        if (oldSkin != "")
        {
            List<Component> skills = new List<Component>();
            foreach (Melee skill in parentObject.GetComponents(typeof(Melee)))
            {
                skills.Add(skill);
            }
            foreach (MidRange skill in parentObject.GetComponents(typeof(MidRange)))
            {
                skills.Add(skill);
            }
            foreach (LongRange skill in parentObject.GetComponents(typeof(LongRange)))
            {
                skills.Add(skill);
            }
            foreach (Component comp in skills)
            {
                Destroy(comp);
            }
            Destroy(GameObject.Find(oldSkin + "(Clone)"));
        }
        parentObject.GetComponent<SetupLocalPlayer>().OldPassif = SkinString;

        if (SkinString.Contains("Melee"))
        {
            parentObject.AddComponent(typeof(Melee));
        }

        if (SkinString.Contains("MidRange"))
        {
            parentObject.AddComponent(typeof(MidRange));
        }

        if (SkinString.Contains("LongRange"))
        {
            parentObject.AddComponent(typeof(LongRange));
        }
    }

    [Command]
    public void CmdAddScript(GameObject player, string newScript)
    {
        RpcAddScript(player, newScript);
    }

    [ClientRpc]
    void RpcAddScript(GameObject player, string newScript)
    {
        System.Type scriptType = System.Type.GetType(newScript);
        NetworkBehaviour script = player.GetComponent(scriptType) as NetworkBehaviour;
        script.enabled = true;
    }
}
