using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Public variables

    /// <summary>
    /// The level button prefab.
    /// </summary>
    public GameObject levelButtonPrefab;

    /// <summary>
    /// The level button container.
    /// </summary>
    public GameObject levelButtonContainer;

    /// <summary>
    /// The shop button prefab.
    /// </summary>
    public GameObject shopButtonPrefab;

    /// <summary>
    /// The shop button container.
    /// </summary>
    public GameObject shopButtonContainer;

    /// <summary>
    /// The currency text.
    /// </summary>
    public Text CurrencyText;

    /// <summary>
    /// The player material.
    /// </summary>
    public Material PlayerMaterial;

    #endregion

    #region Private Variables

    /// <summary>
    /// The camera transform.
    /// </summary>
    private Transform mCameraTransform;

    /// <summary>
    /// The camera desired look at.
    /// </summary>
    private Transform mCameraDesiredLookAt;

    #endregion

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {
        PlayerPrefs.DeleteAll();

        //Set the first skin for player
        ChangePlayerSkin(GameManager.Instance.CurrentSkinIndex);
        //set camera on start
        mCameraTransform = Camera.main.transform;
        //Set current currency
        CurrencyText.text = "Currency : " + GameManager.Instance.Currency.ToString();

        //loads all the levels
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");

        //for each level image load a gameobject button and set the image to the button
        foreach(var thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);

            string sceneName = thumbnail.name;

            //adds a listener so when the button is clicked the level is loaded
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }

        //the texture index
        int textureIndex = 0;
        //loads all the player skins from the images
        Sprite[] textures = Resources.LoadAll<Sprite>("Player");

        //for each skin image a gameobject button is created
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopButtonContainer.transform, false);

            int index = textureIndex;
            //adds a listener so when the skin is clicked the player changes to this skin
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));

            //true when the skin is unlocked
            if((GameManager.Instance.SkinAvailability & 1 << index) == 1 << index)
                container.transform.GetChild(0).gameObject.SetActive(false);

            textureIndex++;
        }
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update()
    {
        if(mCameraDesiredLookAt != null)
        {
            mCameraTransform.rotation = Quaternion.Slerp(mCameraTransform.rotation, mCameraDesiredLookAt.rotation, 3 * Time.deltaTime);
        }
    }

    /// <summary>
    /// Loads the images into the Buttons
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Looks at menu.
    /// </summary>
    /// <param name="menuTransform">Menu transform.</param>
    public void LookAtMenu(Transform menuTransform)
    {
        mCameraDesiredLookAt = menuTransform;
    }

    /// <summary>
    /// Changes the player skin.
    /// </summary>
    /// <param name="index">Index.</param>
    private void ChangePlayerSkin(int index)
    {
        if ((GameManager.Instance.SkinAvailability & 1 << index) == 1 << index)
        {

            float x = (index % 4) * 0.25f;
            float y = ((int)index % 4) / 0.25f;

            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.5f;
            else if (y == 0.5f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0.0f;

            PlayerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
            GameManager.Instance.CurrentSkinIndex = index;
            GameManager.Instance.Save();
        }
        else
        {
            //You do not have the skin, do you want to buy it --- cost
            float cost = 500;

            if(GameManager.Instance.Currency >= cost)
            {
                GameManager.Instance.Currency -= (int)cost;
                GameManager.Instance.SkinAvailability += 1 << index;
                GameManager.Instance.Save();
                CurrencyText.text = "Currency : " + GameManager.Instance.Currency.ToString();
                shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                ChangePlayerSkin(index);
            }
        }
    }
}
