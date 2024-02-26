using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{

    public string LevelToLoad = "";

    public float Yboundary = 200f;


    // Start is called before the first frame update
    void Start()
    {
        string CurrentScene = SceneManager.GetActiveScene().name.ToString();
        LevelToLoad = "Level" + CurrentScene[CurrentScene.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Yboundary)
        {
            SceneManager.LoadScene(LevelToLoad);
        }
    }
}
