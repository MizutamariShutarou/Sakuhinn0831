using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    //鳥のプレハブ格納配列
    public GameObject[] BirdPrefabs;

    //連鎖判定用の鳥の処理
    const float BirdDistance = 3.5f;

    //クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();
    const int MinChain = 3;

    public GameObject LineObj;
    List<GameObject> LineBirdList = new List<GameObject>();

    public GameObject timeUp;


    // Start is called before the first frame update
    void Start()
    {
        
        TouchManager.Began += (info) =>
        {
            //クリック地点でヒットしているオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                //ヒットしたオブジェクトのタグがBirdだったら
                if (hitObj.tag == "Bird")
                {

                    firstBird = hitObj;
                    lastBird = hitObj;
                    currentName = hitObj.name;
                    removableBirdList = new List<GameObject>();
                    pushToBirdList(hitObj);

                }
            }
        };

        TouchManager.Moved += (info) =>
        {
            if (firstBird == null)
            {
                return;
            }
            //クリック地点でヒットしているオブジェクトを取得   
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);

            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                //ヒットしたオブジェクトのタグがBirdかつ、名前が一緒
                //最後にヒットしたオブジェクトと違う、リストには入ってない
                if (hitObj.tag == "Bird" && hitObj.name == currentName && lastBird != hitObj && 0 > removableBirdList.IndexOf(hitObj))
                {
                    float Distance = Vector2.Distance(hitObj.transform.position,lastBird.transform.position);
                    if(Distance>BirdDistance)
                    {
                        return;
                    }
                    //PushToLineList(hitObj, lastBird);
                    lastBird = hitObj;
                    pushToBirdList(hitObj);


                }
            }
        };
        TouchManager.Ended += (info) =>
        {
            int count = removableBirdList.Count;

            if(count >= MinChain)
            {
                //リストに格納されている鳥を消去
                foreach(GameObject Obj in removableBirdList)
                {
                    Destroy(Obj);
                }
                StartCoroutine(DropBird(count));
            }
            

            foreach (GameObject Obj in removableBirdList)
            {
                ChangeColor(Obj, 1.0f);
            }
            removableBirdList = new List<GameObject>();
            LineBirdList = new List<GameObject>();
            firstBird = null;
            lastBird = null;
        };



        StartCoroutine(DropBird(50));
    }

    private void PushToLineList(GameObject lastObj, GameObject hitObj)
    {
        GameObject Line = (GameObject)Instantiate(LineObj);
        LineRenderer renderer = Line.GetComponent<LineRenderer>();
        //線の太さ
        renderer.startWidth = 0.1f;
        renderer.endWidth = 0.1f;
        //頂点の数
        renderer.positionCount = 2;
        //頂点を設定
        renderer.SetPosition(0, new Vector3(lastObj.transform.position.x,lastObj.transform.position.y, -1.0f));
        renderer.SetPosition(1, new Vector3(hitObj.transform.position.x, hitObj.transform.position.y, -1.0f));
        LineBirdList.Add(Line);
    }

    private void pushToBirdList(GameObject Obj)
    {
        removableBirdList.Add(Obj);
        ChangeColor(Obj, 0.5f);
    }
   

    private void ChangeColor(GameObject obj, float tranceparency )
    {
        SpriteRenderer BirdTexture = obj.GetComponent<SpriteRenderer>();
        BirdTexture.color = new Color(BirdTexture.color.r, BirdTexture.color.g, BirdTexture.color.b, tranceparency);
    }



    // Update is called once per frame
   

    //指定された個数分鳥を発生させる

    IEnumerator　DropBird(int count)
    {
        for(int i = 0; i < count; i++)
        {
            //ランダムで出現位置を作成
            Vector2 pos = new Vector2(Random.Range(-5.4f, 5.4f),15.3f);
            //出現する鳥のIDを作成
            int id = Random.Range(0, BirdPrefabs.Length);

            //鳥を発生させる
            GameObject bird = (GameObject)Instantiate(BirdPrefabs[id],
                pos,
                Quaternion.AngleAxis(Random.Range(-40,40), Vector3.forward));
            //作成した鳥の名前をIDを使ってつけなおす
            bird.name = "Bird" + id;

            //0.05秒待って次の処理へ
            yield return new WaitForSeconds(0.05f);



        }
    }
    //private void Update()
    //{
        //if(timeUp.GetComponent<>)
    //}


}
