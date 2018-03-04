using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Login : MonoBehaviour {

    public string Username;
    public string Password;

    public GameObject AccountManager;

    public GameObject RegisterObject;

    [Space]
    public GameObject LoginInNotif;
    public GameObject ErrorButton;
    public GameObject UsernamePasswordMatch;
    public GameObject InvalidPasswordUsername;
    public GameObject ServerError;


    public void LoginButton ()
    {
        StartCoroutine(LoginAccount());
    }

    private IEnumerator LoginAccount ()
    {
        LoginInNotif.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("passwordPost", Password);

        WWW www = new WWW(SqlManager.url + "Login.php", form);
        yield return www;

        LoginInNotif.SetActive(false);
        if (www.text.Contains("Login Successful."))
        {
            AccountManager.GetComponent<SqlManager>().ID = int.Parse(www.text.Substring(www.text.IndexOf("||") + 2, www.text.Length - www.text.IndexOf("||") - 2));
            AccountManager.GetComponent<SqlManager>().Username = Username;
            AccountManager.GetComponent<SqlManager>().GroupID[0] = Username;
            SceneManager.LoadScene("Main Menu");
            yield break;
        }
        if (www.text == "Username and Password don't match")
        {
            UsernamePasswordMatch.SetActive(true);
            ErrorButton.SetActive(true);
            yield break;
        }
        if (www.text == "Invalid Username or Password")
        {
            InvalidPasswordUsername.SetActive(true);
            ErrorButton.SetActive(true);
            yield break;
        }
        else
        {
            ServerError.SetActive(true);
            ErrorButton.SetActive(true);
            yield break;
        }
    }

    public void UpdateUsername ()
    {
        Username = GameObject.Find("Username_Login").GetComponent<InputField>().text;
    }

    public void UpdatePassword()
    {
        Password = GameObject.Find("Password_Login").GetComponent<InputField>().text;
    }

    public void GoToRegister ()
    {
        RegisterObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
