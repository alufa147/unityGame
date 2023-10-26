using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terraceController : MonoBehaviour
{
    public GameObject chair1;
    public GameObject chair2;
    public GameObject chair3;
    public GameObject chair4;
    public GameObject chair5;
    public GameObject chair6;
    public GameObject chair7;
    public GameObject chair8;
    public GameObject chair9;
    public GameObject chair10;
    public GameObject chair11;
    public GameObject chair12;
    public GameObject chair13;
    public GameObject chair14;
    public GameObject chair15;
    public GameObject chair16;
    public GameObject chair17;
    public GameObject chair18;
    public GameObject chair19;
    public GameObject chair20;
    public GameObject chair21;
    public GameObject chair22;

    private bool[] chairs = new bool[22];

    //空き席を調べて、確保し、返す。
    public GameObject getFreeChair()
    {
        GameObject[] c = { chair1, chair2, chair3, chair4, chair5, chair6, chair7, chair8, chair9, chair10, chair11, chair12, chair13, chair14, chair15, chair16, chair17, chair18, chair19, chair20, chair21, chair22 };

        for(int i = 0; i < c.Length; i++)
        {
            //表示されている状態の椅子が空き状態だったらその椅子情報を渡す。
            if (c[i].activeInHierarchy && !chairs[i])
            {
                chairs[i] = true;
                return c[i];
            }
        }

        //どこも空いていないければnullを返す。
        return null;
    }

    //椅子の空き状態に戻す
    public void setChairStatus(GameObject usedChair)
    {
        GameObject[] c = { chair1, chair2, chair3, chair4, chair5, chair6, chair7, chair8, chair9, chair10, chair11, chair12, chair13, chair14, chair15, chair16, chair17, chair18, chair19, chair20, chair21, chair22 };
        for(int i = 0; i < c.Length; i++)
        {
            if(usedChair.transform.position == c[i].transform.position)
            {
                chairs[i] = false;
                return;
            }
        }
    }
    
}
