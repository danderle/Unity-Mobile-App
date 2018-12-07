using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    /// <summary>
    /// The instance.
    /// </summary>
    private static GameManager mInstance;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static GameManager Instance { get { return mInstance; }}

    /// <summary>
    /// The index of the current skin.
    /// </summary>
    public int CurrentSkinIndex = 0;

    /// <summary>
    /// The currency.
    /// </summary>
    public int Currency = 0;

    /// <summary>
    /// The skin availability.
    /// </summary>
    public int SkinAvailability = 1;

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Awake()
    {

        mInstance = this;
        /// <summary>
        /// Update this instance.
        /// </summary>
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            //We had previous session
            CurrentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            Currency = PlayerPrefs.GetInt("Currency");
            SkinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        }
        else
        {
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", CurrentSkinIndex);
        PlayerPrefs.SetInt("Currency", Currency);
        PlayerPrefs.SetInt("SkinAvailability", SkinAvailability);
    }
}
