using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        //if (DontDestroyEnabled == false)
        //{
        //    // Sceneを遷移してもオブジェクトが消えないようにする
        //    Destroy(this);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Scenes/title");
    }
}
