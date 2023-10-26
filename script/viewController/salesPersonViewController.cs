using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class salesPersonViewController : MonoBehaviour
{
    public GameObject salesPersonParentView;

    //表示するView
    public GameObject salesPersonView;


    Color enableColor = new Color(1f, 1f, 1f, 1f);
    Color disableColor = new Color(0f, 0f, 0f, 0f);

    Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();

    common com;

    //店員に指示を投げたときに空きスロットにimageを入れる
    public void inputJobImage(string name, string jobName, int position)
    {
        Image image = views[name].transform.GetChild(0).GetChild(position + 1).GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + jobName);
        image.color = enableColor;
    }

    //ジョブの割り込み
    public void interruptJobImage(string name, string jobName)
    {
        Debug.Log(jobName);
        for(int i = 5; i > 1; i--)
        {
            Image image1 = views[name].transform.GetChild(0).GetChild(i).GetComponent<Image>();
            Image image2 = views[name].transform.GetChild(0).GetChild(i - 1).GetComponent<Image>();
            image1.sprite = image2.sprite;
            if(image1.sprite == null)
            {
                image1.color = disableColor;
            } else
            {
                image1.color = enableColor;
            }
            
        }
        views[name].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + jobName);

    }

    //ジョブの削除
    public void deleteJobImage(string name)
    {
        for(int i = 1; i < 5; i++)
        {
            Image image1 = views[name].transform.GetChild(0).GetChild(i).GetComponent<Image>();
            Image image2 = views[name].transform.GetChild(0).GetChild(i+1).GetComponent<Image>();
            image1.sprite = image2.sprite;
            if(image1.sprite == null)
            {
                image1.color = disableColor;
            } else
            {
                image1.color = enableColor;
            }

            if(image2.sprite == null)
            {
                image2.color = disableColor;
            } else
            {
                image2.color = enableColor;
            }
        }
    }

    //現在設定されているjobのイメージを返す
    public Sprite getSpinnerImage(string name)
    {
        return views[name].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
    }
    

    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
    }

    //店員のviewを選択状態を変化させる
    public void selectStaff(string name)
    {
        Debug.Log(name);
        Image image = views[name].GetComponent<Image>();
        if(image.color == Color.yellow)
        {
            image.color = disableColor;
        } else
        {
            image.color = Color.yellow;
            foreach (string key in views.Keys)
            {
                if (key != name)
                {
                    views[key].GetComponent<Image>().color = disableColor;
                }
            }
        }
        
    }

    //すべての店員のviewを非選択状態にする
    public void unSelectStaff()
    {
        foreach(string key in views.Keys)
        {
            views[key].GetComponent<Image>().color = disableColor;
        }
    }

    //DBに登録されている店員をViewとして表示する
    public void setView(GameObject[] salesPersons)
    {

        foreach (GameObject sp in salesPersons)
        {
            if (sp == null) return;

            // すでに表示している場合はスキップ
            if (views.ContainsKey(sp.name))
            {
                continue;
            }

            GameObject view = Instantiate(salesPersonView);
            view.transform.SetParent(salesPersonParentView.transform);

            //viewのポジションを設定
            view.GetComponent<RectTransform>().anchoredPosition = new Vector2(210f * (salesPersonParentView.transform.childCount-1), 0);

            //viewの写真を設定
            view.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(numberOfResouceName(sp.GetComponent<playercontroller>().skinNumber));

            //グレーアウトするスロット数を設定
            for(int i = 0; i < sp.GetComponent<playercontroller>().slotNumber+  2; i++)
            {
                GameObject v = view.transform.GetChild(0).GetChild(i + 1).gameObject;
                v.SetActive(true);
                v.GetComponent<Image>().color = disableColor;
            }

            //ボタンの設定
            view.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {

                com.hoc.salesPersonSetSelect(sp.name);

            });

            views.Add(sp.name, view);
        }
    }


    //番号に紐づくイメージ
    public string numberOfResouceName(int num)
    {
        if (num == 1)
        {
            return "woman1";
        }
        else if (num == 2)
        {
            return "man2";
        }
        else if (num == 3)
        {
            return "woman2";
        }
        else
        {
            return "man1";
        }
    }


}
