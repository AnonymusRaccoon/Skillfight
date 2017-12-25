using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour {

	private GameObject AccountManager;
	public string SelectedFriend;

	void OnEnable()
	{
		AccountManager = GameObject.Find("AccountManager");
		StartCoroutine(ListMessage());
	}

	public void AddToChatButton (string friend, string message)
	{
		StartCoroutine(AddToChat(friend, message));
	}

	private IEnumerator AddToChat (string friend, string message)
	{
		WWWForm form = new WWWForm();
		form.AddField ("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
		form.AddField ("FriendPost", friend);
		form.AddField ("MessagePost", message);

		WWW www = new WWW(SqlManager.url + "Chat.php", form);

        yield return www;

		if (www.text.Contains("<!DOCTYPE HTML"))
		{
			StartCoroutine(AddToChat(friend, message));
		}
	}

	private IEnumerator ListMessage ()
	{
		WWWForm form = new WWWForm();
		form.AddField ("PlayerPost", AccountManager.GetComponent<SqlManager>().Username);
		form.AddField ("FriendPost", SelectedFriend);

		WWW www = new WWW(SqlManager.url + "ListChat.php", form);

        yield return www;

		string [] messages = www.text.Split("//".ToCharArray());

		foreach (string message in messages)
		{

            //GameObject text = Instantiate (MessagePrefab);
			
		}

		if (www.text.Contains("<!DOCTYPE HTML"))
		{
			StartCoroutine(ListMessage());
		}

		yield return new WaitForSeconds (1);
		StartCoroutine(ListMessage());
	}
}
