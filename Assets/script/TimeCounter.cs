using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class TimeCounter : MonoBehaviour
{
    
    //カウントダウン
    public float time = 10.0f;
    public Text timerText;
    public bool isTimeUp;
    //時間を表示するText型の変数
    public Text timeText;

    // Update is called once per frame
    private void Start()
    {
        isTimeUp = false;
    }
    void Update()
    {
        if (0 < time)
        {
            //時間をカウントダウンする
            time -= Time.deltaTime;
            //時間を表示する
            timeText.text = time.ToString("f1") + "秒";

        }
       

        //countdownが0以下になったとき
        else if (time <= 0)
        {
            isTimeUp = true;

            //timeText.text = "Game Over";
            SceneManager.LoadScene("Scenes/Game Over");
        }
        
    }
    //public void ChangeScene()
    //{
    //    if (time <= 0)
    //    {
    //        SceneManager.LoadScene("Scenes/Game Over");
    //    }
    //}
}
