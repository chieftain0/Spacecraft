using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelPicker : MonoBehaviour
{
    // Start is called before the first frame update

    public string[] scenes;
    public string SelectedScene = null;

    public TMP_Text Text;

    public int choice;
    void Start()
    {
        choice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "";
        for (int i = 0; i < scenes.Length; i++)
        {
            if (i == choice)
            {
                Text.text += scenes[i] + " < \n";
            }
            else
            {
                Text.text += scenes[i] + "\n";
            }   
        }

        if(Input.GetButtonDown("Y_BUTTON"))
        {
            choice--;
        }
        if (Input.GetButtonDown("A_BUTTON"))
        {
            choice++;
        }

        if (choice > scenes.Length - 1 || choice < 0)
        {
            choice = 0;
        }

        SelectedScene = scenes[choice];

        if(Input.GetButtonDown("X_BUTTON"))
        {
            SceneManager.LoadScene("Moon" + (choice + 1).ToString());
        }
        
    }
}
