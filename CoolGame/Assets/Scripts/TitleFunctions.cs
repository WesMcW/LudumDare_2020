using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleFunctions : MonoBehaviour
{
    public TMP_InputField field;
    public Button playBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            field.text = PlayerPrefs.GetString("username");
            playBtn.interactable = true;
        }
    }

    public void Play()
    {
        
    }

    public void ValidateName()
    {
        string name = field.text;
        if (name.Length > 0)
        {
            name = name.Replace(" ", string.Empty);

            if (name.Length > 18)
            {
                name = name.Remove(18);
            }

            field.text = name;
            PlayerPrefs.SetString("username", name);
            playBtn.interactable = true;
        }
        else
        {
            if (PlayerPrefs.HasKey("username"))
            {
                field.text = PlayerPrefs.GetString("username");
            }
            else playBtn.interactable = false;
        }
    }
}
