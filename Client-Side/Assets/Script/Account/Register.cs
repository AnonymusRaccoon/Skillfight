using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Register : MonoBehaviour
{
    public string Username;
    public string Password;
    public string confirmPassword;
    public string eMail;
    public int iconID;
    public Sprite[] Icon;
    public GameObject IconImage;

    public GameObject AccountManager;

    public GameObject Login;

    [Space]
    public GameObject LoginInNotif;
    public GameObject ErrorButton;
    public GameObject UsernameTooShort;
    public GameObject UsernameTooLong;
    public GameObject PasswordTooShort;
    public GameObject PasswordsMatch;
    public GameObject AvalibleEMail;
    public GameObject UsernameAlreadyUse;


    public void RegisterAccount ()
    {
        if (Username.Length < 4)
        {
            UsernameTooShort.SetActive(true);
            ErrorButton.SetActive(true);
            return;
        }
        if (Username.Length > 10)
        {
            UsernameTooLong.SetActive(true);
            ErrorButton.SetActive(true);
            return;
        }
        if (Password.Length < 6)
        {
            PasswordTooShort.SetActive(true);
            ErrorButton.SetActive(true);
            return;
        }
        if (Password != confirmPassword)
        {
            PasswordsMatch.SetActive(true);
            ErrorButton.SetActive(true);
            return;
        }
        if (eMail.Length > 8 && !eMail.Contains ("@"))
        {
            AvalibleEMail.SetActive(true);
            ErrorButton.SetActive(true);
            return;
        }
        StartCoroutine(CreateAccount());
    }

    private IEnumerator CreateAccount ()
    {
        LoginInNotif.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("eMailPost", eMail);
        form.AddField("passwordPost", Password);
        form.AddField("iconIDPost", iconID);

        WWW www = new WWW(SqlManager.url + "Register.php", form);
        yield return www;
        LoginInNotif.SetActive(false);

        if (www.text.Contains("Error: INSERT INTO"))
        {
            UsernameAlreadyUse.SetActive(true);
            ErrorButton.SetActive(true);
            yield break;
        }
        if (www.text.Contains("Successfully"))
        {
            AccountManager.GetComponent<SqlManager>().ID = int.Parse(www.text.Substring(www.text.IndexOf("||") + 2, www.text.Length - www.text.IndexOf("||") - 2));
            AccountManager.GetComponent<SqlManager>().Username = Username;
            AccountManager.GetComponent<SqlManager>().GroupID[0] = Username;
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void UpdateUsername ()
    {
        Username = GameObject.Find("Username_Register").GetComponent<InputField>().text;
    }

    public void UpdateEMail()
    {
        eMail = GameObject.Find("eMail_Register").GetComponent<InputField>().text;
    }

    public void UpdatePassword()
    {
        Password = GameObject.Find("Password_Register").GetComponent<InputField>().text;
    }

    public void UpdateConfirmPassword()
    {
        confirmPassword = GameObject.Find("ConfirmPassword_Register").GetComponent<InputField>().text;
    }

    public void PreviousIcon()
    {
        if (iconID > 0)
            iconID--;
        else
            iconID = Icon.Length;
        IconImage.GetComponent<Image>().sprite = Icon[iconID];
    }

    public void NextIcon()
    {
        if (iconID + 1 > Icon.Length)
            iconID = 0;
        else
            iconID ++;
        IconImage.GetComponent<Image>().sprite = Icon[iconID];
    }

    public void GoToLogin()
    {
        Login.SetActive(true);
        gameObject.SetActive(false);
    }
}
