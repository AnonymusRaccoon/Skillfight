using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
{

	public int Page = 1;
	public int SelectedAbilities = 1;
	public Text TimerText;

	public GameObject Fond;
	public Sprite FondPassif;
	public Sprite FondNormal;
	public Sprite FondLock;
	public Sprite FondAlreadyLock;

	public Sprite Melee;
	public Sprite MidRange;
	public Sprite LongRange;

	public GameObject Skills;
	public GameObject SkillsPassif;

    public GameObject AllActive;
    public GameObject SkillSlider;


    public GameObject Button1;
	public GameObject Button2;
	public GameObject Button3;
	public GameObject Button4;
	public GameObject ButtonLock;
	public Slider SliderObject;
	
	public GameObject SelectedPassif;
	public GameObject SelectedAbilities1;
	public GameObject SelectedAbilities2;
    public GameObject SelectedAbilities3;

    public GameObject HealthSlider;

	public Button[] ButtonList;
	public Sprite[] AbilitiesList;

	private GameObject LocalPlayer;
	
	public GameObject SkillObject;

    [SerializeField]
    private GameObject SkillPassif;
    [SerializeField]
    private GameObject Skill1;
    [SerializeField]
    private GameObject Skill2;
    [SerializeField]
    private GameObject Skill3;

    [SerializeField]
    private GameObject InGamePassif;
    [SerializeField]
    private GameObject InGameSkill1;
    [SerializeField]
    private GameObject InGameSkill2;
    [SerializeField]
    private GameObject InGameSkill3;

    Hp Health;

    private void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }

    public void ChangePage () {

		if (SliderObject.normalizedValue >= 0 && SliderObject.normalizedValue < 0.20f) {

			ButtonList [0].GetComponent<Image> ().sprite = AbilitiesList [0];
			ButtonList [1].GetComponent<Image> ().sprite = AbilitiesList [1];
			ButtonList [2].GetComponent<Image> ().sprite = AbilitiesList [2];
			ButtonList [3].GetComponent<Image> ().sprite = AbilitiesList [3];
			ButtonList [4].GetComponent<Image> ().sprite = AbilitiesList [4];

		} if (SliderObject.normalizedValue >= 0.20f && SliderObject.normalizedValue < 0.40f) {

			ButtonList [0].GetComponent<Image> ().sprite = AbilitiesList [5];
			ButtonList [1].GetComponent<Image> ().sprite = AbilitiesList [6];
			ButtonList [2].GetComponent<Image> ().sprite = AbilitiesList [7];
			ButtonList [3].GetComponent<Image> ().sprite = AbilitiesList [8];
			ButtonList [4].GetComponent<Image> ().sprite = AbilitiesList [9];

		} if (SliderObject.normalizedValue >= 0.40f && SliderObject.normalizedValue < 0.60f) {

			ButtonList [0].GetComponent<Image> ().sprite = AbilitiesList [10];
			ButtonList [1].GetComponent<Image> ().sprite = AbilitiesList [11];
			ButtonList [2].GetComponent<Image> ().sprite = AbilitiesList [12];
			ButtonList [3].GetComponent<Image> ().sprite = AbilitiesList [13];
			ButtonList [4].GetComponent<Image> ().sprite = AbilitiesList [14];

		} if (SliderObject.normalizedValue >= 0.60f && SliderObject.normalizedValue < 0.80f) {

			ButtonList [0].GetComponent<Image> ().sprite = AbilitiesList [15];
			ButtonList [1].GetComponent<Image> ().sprite = AbilitiesList [16];
			ButtonList [2].GetComponent<Image> ().sprite = AbilitiesList [17];
			ButtonList [3].GetComponent<Image> ().sprite = AbilitiesList [18];
			ButtonList [4].GetComponent<Image> ().sprite = AbilitiesList [19];

		} if (SliderObject.normalizedValue >= 0.80f && SliderObject.normalizedValue < 1) {

			ButtonList [0].GetComponent<Image> ().sprite = AbilitiesList [20];
			ButtonList [1].GetComponent<Image> ().sprite = AbilitiesList [21];
			ButtonList [2].GetComponent<Image> ().sprite = AbilitiesList [22];
			ButtonList [3].GetComponent<Image> ().sprite = AbilitiesList [23];
			ButtonList [4].GetComponent<Image> ().sprite = AbilitiesList [24];

		}

	}

	public void AbilitiesButton1 () {

		SelectedAbilities = 1;
		Fond.GetComponent<Image> ().sprite = FondPassif;
		Skills.SetActive (false);
		SkillsPassif.SetActive (true);
		// SelectedPassif.SetActive (true);
		// SelectedAbilities1.SetActive (false);
		// SelectedAbilities2.SetActive (false);
		// SelectedAbilities3.SetActive (false);

	}

	public void AbilitiesButton2 () {

		SelectedAbilities = 2;
		Fond.GetComponent<Image> ().sprite = FondNormal;
		Skills.SetActive (true);
        SkillsPassif.SetActive (false);
        AllActive.SetActive(true);
        SkillSlider.SetActive(true);
        ButtonLock.SetActive(false);
        // SelectedPassif.SetActive (false);
        // SelectedAbilities1.SetActive (true);
        // SelectedAbilities2.SetActive (false);
        // SelectedAbilities3.SetActive (false);


    }

	public void AbilitiesButton3 () {

		SelectedAbilities = 3;
		Fond.GetComponent<Image> ().sprite = FondNormal;
		Skills.SetActive (true);
		SkillsPassif.SetActive (false);
        AllActive.SetActive(true);
        SkillSlider.SetActive(true);
        ButtonLock.SetActive(false);
        // SelectedPassif.SetActive (false);
        // SelectedAbilities1.SetActive (false);
        // SelectedAbilities2.SetActive (true);
        // SelectedAbilities3.SetActive (false);

    }

	public void AbilitiesButton4 () {

		SelectedAbilities = 4;
		Fond.GetComponent<Image> ().sprite = FondNormal;
		Skills.SetActive (true);
		SkillsPassif.SetActive (false);
        AllActive.SetActive(true);
        SkillSlider.SetActive(true);
        ButtonLock.SetActive(false);
        // SelectedPassif.SetActive (false);
        // SelectedAbilities1.SetActive (false);
        // SelectedAbilities2.SetActive (false);
        // SelectedAbilities3.SetActive (true);

    }
	
	public void AbilitiesSelectButton ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject;
        SetupLocalPlayer SetupLocalPlayerScript = player.GetComponent<SetupLocalPlayer> ();
		if (SelectedAbilities == 1)
        {
            Health = player.GetComponent<Hp> ();
			Button1.GetComponent<Image> ().enabled = true;
			if (EventSystem.current.currentSelectedGameObject.name == "Melee")
            {
				Button1.GetComponent<Image>().sprite = Melee;
                SkillPassif.GetComponent<Image>().sprite = Melee;
                InGamePassif.GetComponent<Image>().sprite = Melee;
                UpdateSkin(player, "Melee");
                SetupLocalPlayerScript.Passif = "Melee";
                Health.Health = 1300;
                Health.maxHealth = 1300;
				Health.HealthSlider.maxValue = 1300;
				Health.HealthSlider.value = 1300;
			}
            if (EventSystem.current.currentSelectedGameObject.name == "MidRange")
            {
				Button1.GetComponent<Image>().sprite = MidRange;
                SkillPassif.GetComponent<Image>().sprite = MidRange;
                InGamePassif.GetComponent<Image>().sprite = MidRange;
                UpdateSkin(player, "MidRange");
                SetupLocalPlayerScript.Passif = "MidRange";
                Health.Health = 1000;
                Health.maxHealth = 1000;
                Health.HealthSlider.maxValue = 1000;
				Health.HealthSlider.value = 1000;
			}
            if (EventSystem.current.currentSelectedGameObject.name == "LongRange")
            {
				Button1.GetComponent<Image>().sprite = LongRange;
                SkillPassif.GetComponent<Image>().sprite = LongRange;
                InGamePassif.GetComponent<Image>().sprite = LongRange;
                SetupLocalPlayerScript.Passif = "LongRange";
                UpdateSkin(player, "LongRange");
				Health.Health = 900;
                Health.maxHealth = 900;
                Health.HealthSlider.maxValue = 900;
				Health.HealthSlider.value = 900;
			}
            AbilitiesButton2();
			return;
		} if (SelectedAbilities == 2 && EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name != SetupLocalPlayerScript.Abilities2 && EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name != SetupLocalPlayerScript.Abilities3) {

            foreach (Component comp in player.GetComponents (typeof(Component)))
            {
                if (comp.name == SetupLocalPlayerScript.Abilities1)
                {
                    Destroy(comp);
                }
            }
            Button2.GetComponent<Image> ().enabled = true;
			Button2.GetComponent<Image> ().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite;
            Skill1.GetComponent<Image> ().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            InGameSkill1.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            SetupLocalPlayerScript.Abilities1 = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name;
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject.GetComponent<AbilitiesNetworker>().CmdAddScript(GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject, EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name);
            AbilitiesButton3();
			return;
			
		} if (SelectedAbilities == 3 && EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name != SetupLocalPlayerScript.Abilities1 && EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name != SetupLocalPlayerScript.Abilities3) {

            foreach (Component comp in player.GetComponents(typeof(Component)))
            {
                if (comp.name == SetupLocalPlayerScript.Abilities2)
                {
                    Destroy(comp);
                }
            }
            Button3.GetComponent<Image> ().enabled = true;
			Button3.GetComponent<Image> ().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite;
            Skill2.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            InGameSkill2.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            SetupLocalPlayerScript.Abilities2 = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name;
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject.GetComponent<AbilitiesNetworker>().CmdAddScript(GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject, EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name);
            AbilitiesButton4();
			return;

		} if (SelectedAbilities == 4 && EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name != SetupLocalPlayerScript.Abilities1 && EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name != SetupLocalPlayerScript.Abilities2) {

            foreach (Component comp in player.GetComponents(typeof(Component)))
            {
                if (comp.name == SetupLocalPlayerScript.Abilities3)
                {
                    Destroy(comp);
                }
            }
            Button4.GetComponent<Image> ().enabled = true;
			Button4.GetComponent<Image> ().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite;
            Skill3.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            InGameSkill3.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            SetupLocalPlayerScript.Abilities3 = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name;
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject.GetComponent<AbilitiesNetworker>().CmdAddScript(GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject, EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name);
            AreYouReady ();
        }

    }

    void UpdateSkin(GameObject parentObject, string SkinString)
    {
        parentObject.GetComponent<AbilitiesNetworker>().CmdUpdateSkin(SkinString, parentObject);

        StartCoroutine(SpawnSkinCallback(SkinString, parentObject));

        if (SkinString.Contains("Melee"))
            GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition = new Vector3(-0.034f, 1.4f, 0.25f);
        if (SkinString.Contains("MidRange"))
            GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition = new Vector3(-0.034f, 1.4f, 0.25f);
        if (SkinString.Contains("LongRange"))
            GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition = new Vector3(-0.034f, 1.4f, 0.25f);
    }

    private IEnumerator SpawnSkinCallback(string SkinString, GameObject parentObject)
    {
        yield return new WaitForSeconds(1.5f);
        parentObject.GetComponent<FirstPersonController>().anim = parentObject.transform.Find(SkinString + "(Clone)").GetComponent<Animator>();
    }



    void AreYouReady () 
	{
		Fond.GetComponent<Image> ().sprite = FondLock;
		ButtonLock.SetActive(true);
        AllActive.SetActive(false);
        SkillSlider.SetActive(false);
	}

	public void Ready () 
	{
		Fond.GetComponent<Image> ().sprite = FondAlreadyLock;
		ButtonLock.SetActive(false);
		Button1.GetComponent<Button> ().enabled = false;
		Button2.GetComponent<Button> ().enabled = false;
		Button3.GetComponent<Button> ().enabled = false;
		Button4.GetComponent<Button> ().enabled = false;
        GameObject.Find("Main Camera").transform.parent.GetComponent<SetupLocalPlayer>().CmdReady(GameObject.Find("Main Camera").transform.parent.name);
	}
}
