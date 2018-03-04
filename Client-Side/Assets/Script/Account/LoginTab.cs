using UnityEngine.EventSystems;
using UnityEngine;

public class LoginTab : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Username_Login")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Password_Login"), null);
            else if (EventSystem.current.currentSelectedGameObject.name == "Password_Login")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Username_Login"), null);

            else if (EventSystem.current.currentSelectedGameObject.name == "Username_Register")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("eMail_Register"), null);
            else if (EventSystem.current.currentSelectedGameObject.name == "eMail_Register")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Password_Register"), null);
            else if (EventSystem.current.currentSelectedGameObject.name == "Password_Register")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("ConfirmPassword_Register"), null);
            else if (EventSystem.current.currentSelectedGameObject.name == "ConfirmPassword_Register")
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Username_Register"), null);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Username_Login" || EventSystem.current.currentSelectedGameObject.name == "Password_Login")
                GameObject.Find("Login").GetComponent<Login>().LoginButton();

            else if (EventSystem.current.currentSelectedGameObject.name == "Username_Register" || EventSystem.current.currentSelectedGameObject.name == "eMail_Register" || EventSystem.current.currentSelectedGameObject.name == "Password_Register" || EventSystem.current.currentSelectedGameObject.name == "ConfirmPassword_Register")
                GameObject.Find("Register").GetComponent<Register>().RegisterAccount();
        }
    }
}
