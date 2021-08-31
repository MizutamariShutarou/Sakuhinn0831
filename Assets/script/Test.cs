using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TouchManager.Began += info => { Debug.Log("押された"); };
        //TouchManager.Ended += info => { Debug.Log("離れた"); };

        TouchManager.Began += (info) =>
        {
            Debug.Log("押されたスクリーン座標は" + info.screenPoint);
        };



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
