using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject escapeMenuCanvas;

    bool paused = false;

    // Start is called before the first frame update

    private void Awake()
    {
        mainCanvas.SetActive(true);
        escapeMenuCanvas.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            print("Suuuuuka");

            if(!paused)
            {
                mainCanvas.SetActive(false);
                escapeMenuCanvas.SetActive(true);
                Time.timeScale = 0.0f;
                paused = true;

            }
            else
            {
                Time.timeScale = 1.0f;
                mainCanvas.SetActive(true);
                escapeMenuCanvas.SetActive(false);

                paused = false;
            }


        }
        
    }
}
