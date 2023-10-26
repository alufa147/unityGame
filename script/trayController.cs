using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trayController : MonoBehaviour
{
    common com;
    public GameObject trayObject;

    Vector3 tray1Position = new Vector3(-14.09f, 1.25f, -36.2f);
    Vector3 tray2Position = new Vector3(-14.09f, 1.25f, -35.01f);
    Vector3 tray3Position = new Vector3(-14.09f, 1.25f, -33.87f);

    GameObject tray1;
    GameObject tray2;
    GameObject tray3;

    //新体制
    int tray = 1;
    bool tray1State = false;
    bool tray2State = false;
    bool tray3State = false;

    //トレー切り替えボタン
    public Text selectButtonText;

    //トレー完成ボタン
    public Button successButton1;
    public Button successButton2;
    public Button successButton3;

    //トレーアイテム格納用オブジェクト子番号
    private int itemObjNum = 3;

    //トレー完成ボタン内のトレーイメージ
    Image trayImage1;
    Image trayImage2;
    Image trayImage3;

    Image checkImage1;
    Image checkImage2;
    Image checkImage3;

    Color disableColor = new Color(0.68f, 0.68f, 0.68f);
    Color enableColor = new Color(1f,1f,1f);

    //未確定アイテムを保持して置く用
    string unsettledTrayItem1 = "";
    string unsettledTrayItem2 = "";
    string unsettledTrayItem3 = "";

    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
        trayImage1 = successButton1.transform.GetChild(1).GetComponent<Image>();
        trayImage2 = successButton2.transform.GetChild(1).GetComponent<Image>();
        trayImage3 = successButton3.transform.GetChild(1).GetComponent<Image>();
        checkImage1 = successButton1.transform.GetChild(2).GetComponent<Image>();
        checkImage2 = successButton2.transform.GetChild(2).GetComponent<Image>();
        checkImage3 = successButton3.transform.GetChild(2).GetComponent<Image>();
        checkImage1.enabled = false;
        checkImage2.enabled = false;
        checkImage3.enabled = false;

        if (com.trayStrage2.activeInHierarchy)
        {
            com.traySelectText.text = "1";
        }
    }

    public void instantiateTray(int num)
    {
        //店員がトレーを生成したときに実行される。
        if(num == 1)
        {
            tray1 = Instantiate(trayObject);
            tray1.transform.position = tray1Position;
        } else if(num == 2)
        {
            tray2 = Instantiate(trayObject);
            tray2.transform.position = tray2Position;
        } else if(num == 3)
        {
            tray3 = Instantiate(trayObject);
            tray3.transform.position = tray3Position;
        }
      //Debug.Log("トレー (" + trayManage[customerName].name + ") が生成されました。");
      
    }

    //トレーの位置を返す
    public Vector3 getTrayPosition(int num)
    {
        if(num == 1)
        {
            return tray1Position;
        } else if(num == 2)
        {
            return tray2Position;
        } else
        {
            return tray3Position;
        }
    }

    //店員にトレーオブジェクトを渡す
    public GameObject getTray(int num)
    {
        if(num == 1)
        {
            return tray1;
        } else if(num == 2)
        {
            return tray2;
        } else
        {
            return tray3;
        }
    }

    //渡されたオブジェクトをトレー上に生成
    public bool instantiateItem(int num, GameObject item, string foodName)
    {
        //Debug.Log("instantiateItem" + customerName + "  itemName " + item.name);

        GameObject[] trayArr = { tray1, tray2, tray3 };

        int trayTransform = getTransform(num);

        // トレー上のアイテムが6だったらエラーを返す
        if(trayTransform >= trayArr[num -1].transform.childCount)
        {
            return false;
        }

        GameObject instantiateItem = Instantiate(item);
        instantiateItem.transform.SetParent(trayArr[num-1].transform.GetChild(trayTransform));
        instantiateItem.transform.localPosition = new Vector3(0, 0, 0);
        trayArr[num-1].GetComponent<tray>().setItem(foodName);
        return true;

    }


    //トレー上のオブジェクト用の位置
    public int getTransform(int num)
    {
        //トレイオブジェクトの中のポジションの中に入っている食べ物の数をカウント
        GameObject t;
        if(num == 1)
        {
            t = tray1;
        } else if(num == 2)
        {
            t = tray2;
        } else
        {
            t = tray3;
        }

        int count = 0;
        for (int i = 0; i < t.transform.childCount; i++)
        {
            if (t.transform.GetChild(i).transform.childCount != 0)
            {
                count++;
            }
        }
        return count;
    }

    //選択状態のトレーを返す
    public int getSelectedTray()
    {
        return tray;
    }


    //トレー切り替えボタン押下
    public void selectTrayButton()
    {
        if(tray == 1 && com.trayStrage2.activeInHierarchy)
        {
            tray = 2;
            selectButtonText.text = "トレー3";
        } else if(tray == 2 && com.trayStrage3.activeInHierarchy)
        {
            tray = 3;
            selectButtonText.text = "トレー2";
        } else
        {
            tray = 1;
            selectButtonText.text = "トレー1";
        }
    }

    //トレーを完成状態にする
    public void successTray1()
    {
        // 店員が選択されていない場合は終了
        if (com.selectStaff == null) return;

        // トレー上に何もアイテムがない状態は終了
        Image im = successButton1.transform.GetChild(itemObjNum).GetChild(0).GetComponent<Image>();
        if (im.color != enableColor || !im.gameObject.activeInHierarchy) return;

        // スタッフに格納ジョブを設定する できなければ終了
        if (!com.selectStaff.GetComponent<playercontroller>().setJobSlot(AppConst.JOB_MOVETRAY, 2, "1")) return;

        tray1State = true;
        successButton1.interactable = false;
        trayImage1.color = disableColor;
        checkImage1.enabled = true;
        
    }

    public void successTray2()
    {
        // 店員が選択されていない場合は終了
        if (com.selectStaff == null) return;

        // トレー上に何もアイテムがない状態は終了
        Image im = successButton2.transform.GetChild(itemObjNum).GetChild(0).GetComponent<Image>();
        if (im.color != enableColor || !im.gameObject.activeInHierarchy) return;

        // スタッフに格納ジョブを設定する できなければ終了
        if (!com.selectStaff.GetComponent<playercontroller>().setJobSlot(AppConst.JOB_MOVETRAY, 2, "2")) return;

        tray2State = true;
        successButton2.interactable = false;
        trayImage2.color = disableColor;
        checkImage2.enabled = true;
    }

    public void successTray3()
    {
        // 店員が選択されていない場合は終了
        if (com.selectStaff == null) return;

        // トレー上に何もアイテムがない状態は終了
        Image im = successButton3.transform.GetChild(itemObjNum).GetChild(0).GetComponent<Image>();
        if (im.color != enableColor || !im.gameObject.activeInHierarchy) return;

        // スタッフに格納ジョブを設定する できなければ終了
        if (!com.selectStaff.GetComponent<playercontroller>().setJobSlot(AppConst.JOB_MOVETRAY, 2, "3")) return;

        tray3State = true;
        successButton3.interactable = false;
        trayImage3.color = disableColor;
        checkImage3.enabled = true;
    }

    //トレーを未完成状態る
    public void unSuccessTray(int num)
    {
        GameObject successButton;
        if(num == 1)
        {
            successButton1.interactable = true;
            trayImage1.color = enableColor;
            tray1State = false;
            tray1 = null;
            checkImage1.enabled = false;
            successButton = successButton1.gameObject;
            
        } else if(num == 2)
        {
            successButton2.interactable = false;
            trayImage2.color = enableColor;
            tray2State = false;
            tray2 = null;
            checkImage2.enabled = false;
            successButton = successButton2.gameObject;
        } else
        {
            successButton3.interactable = false;
            trayImage3.color = enableColor;
            tray3State = false;
            tray3 = null;
            checkImage3.enabled = false;
            successButton = successButton3.gameObject;
        }

        // トレー完成ボタンのアイテムを空にする
        setTrayItemClear(num);

        // 空にする前に未完成状態のアイテムは残す
        setUnsettledItem(num);

        // リサイズ
        setItemViewSize(successButton);
    }

    //トレーに画像を仮セット
    public void setTrayItemImage(int num, string jobName)
    {

        if(jobName == AppConst.JOB_REJI)
        {
            return;
        }

        if(num == 1)
        {

            for(int i = 0; i < 6; i++)
            {
                GameObject item = successButton1.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (!item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + jobName);
                    im.color = disableColor;
                    item.SetActive(true);
                    setItemViewSize(successButton1.gameObject);
                    return;
                }
            }
        } else if(num == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject item = successButton2.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (!item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + jobName);
                    im.color = disableColor;
                    item.SetActive(true);
                    setItemViewSize(successButton2.gameObject);
                    return;
                }
            }
        } else if(num == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject item = successButton3.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (!item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + jobName);
                    im.color = disableColor;
                    item.SetActive(true);
                    setItemViewSize(successButton3.gameObject);
                    return;
                }
            }
        }
    }

    //トレーの画像を確定
    public void setTrayItemImageConfirm(int num, string jobName)
    {
        if(num == 1)
        {
            for(int i = 0; i < 6; i++)
            {
                GameObject item = successButton1.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    if(im.color == enableColor)
                    {
                        continue;
                    }

                    Debug.Log("imagename : " + im.sprite.name + "  jobname : " + jobName);
                    if(im.sprite.name == jobName)
                    {
                        im.color = enableColor;
                        return;
                    }
                }
            }
        } else if(num == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject item = successButton2.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    if (im.color == enableColor)
                    {
                        continue;
                    }
                    if (im.sprite.name == jobName)
                    {
                        im.color = enableColor;
                        return;
                    }
                }
            }
        } else if(num == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject item = successButton3.transform.GetChild(itemObjNum).GetChild(i).gameObject;
                if (item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    if (im.color == enableColor)
                    {
                        continue;
                    }
                    if (im.sprite.name == jobName)
                    {
                        im.color = enableColor;
                        return;
                    }
                }
            }
        }
    }


    //トレー内のアイテム画像を初期化
    private void setTrayItemClear(int num)
    {
        GameObject successButton;
        GameObject parent;
        if(num == 1)
        {
            successButton = successButton1.gameObject;
            
        } else if(num == 2)
        {
            successButton = successButton2.gameObject;
        } else
        {
            successButton = successButton3.gameObject;
        }

        parent = successButton.transform.GetChild(itemObjNum).gameObject;

        // アイテムが1の時
        if (!parent.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            GameObject item = parent.transform.GetChild(0).gameObject;
            item.GetComponent<Image>().sprite = null;
            item.SetActive(false);
        } else
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject item = parent.transform.GetChild(i).gameObject;
                if (item.activeInHierarchy)
                {
                    Image im = item.GetComponent<Image>();
                    if (im.color == disableColor)
                    {
                        unsettledTrayItem1 += im.sprite.name + ",";
                    }
                    im.sprite = null;
                    im.color = disableColor;
                    item.SetActive(false);
                }
            }
        }

        setItemViewSize(successButton.gameObject);
    }

    //トレーのアイテム数に応じてviewのサイズを変更
    private void setItemViewSize(GameObject view)
    {
        int cnt = 0;
        for(int i = 0; i < 6; i++)
        {
            GameObject item = view.transform.GetChild(itemObjNum).GetChild(i).gameObject;
            if (item.activeInHierarchy)
            {
                cnt++;
            }
        }

        view.GetComponent<RectTransform>().sizeDelta = new Vector2(270 + (cnt - 1) * 60, 90);
    }

    //保持してある未確定アイテムを表示する 0 = 未確定
    private void setUnsettledItem(int trayNum)
    {
        if(trayNum == 1)
        {
            int cnt = 0;
            string[] itemArr = unsettledTrayItem1.Split(',');
            
            for(int i = 0; i < itemArr.Length-1; i++)
            {
                if(itemArr[i] != "")
                {
                    Image im = successButton1.transform.GetChild(itemObjNum).GetChild(cnt).GetComponent<Image>();
                    im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + itemArr[i]);
                    im.color = disableColor;
                    im.gameObject.SetActive(true);
                    cnt++;
                }
            }

            // 保持用を初期化
            unsettledTrayItem1 = "";

        } else if(trayNum == 2)
        {

        } else if(trayNum == 3)
        {

        }
    }
}
