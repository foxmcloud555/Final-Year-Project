using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [Tooltip("How many seconds to wait before loading a level")]
    public float loadDelay = 1;

    [Tooltip("The name of the level to be loaded")]
    public string levelName = "";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel()
    {
        Time.timeScale = 1;

        
       

        // Execute the function after a delay
        Invoke("ExecuteLoadLevel", loadDelay);
        
    }

    void ExecuteLoadLevel()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}

