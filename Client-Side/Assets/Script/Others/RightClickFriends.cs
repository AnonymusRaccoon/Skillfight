using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightClickFriends : MonoBehaviour, IPointerClickHandler
{
    private GameObject CanvasObject;
    private GameObject FriendManager;
    private GameObject GroupManager;

    private void OnEnable()
    {
        CanvasObject = GameObject.Find("Imortal Panel");
        FriendManager = GameObject.Find("FriendManager");
        GroupManager = GameObject.Find("GroupManager");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            string friend = gameObject.GetComponentInChildren<Text>().text;

            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1);

            GameObject rightClick = Instantiate(FriendManager.GetComponent<Friends>().rightClickPrefab);
            rightClick.transform.SetParent(CanvasObject.transform);
            rightClick.transform.position = pos;

            rightClick.transform.Find("Invite").GetComponent<Button>().onClick.AddListener(delegate { GroupManager.GetComponent<Group>().InviteFriendToGroupButton(friend); });
            rightClick.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(delegate { FriendManager.GetComponent<Friends>().RemoveFriendButton(friend); }) ;
        }
    }
}
