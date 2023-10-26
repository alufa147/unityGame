using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeDialogController : MonoBehaviour
{
    // 親
    public Canvas parent;

    // ダイアログ本体
    public GameObject dialog;

    // メッセージの位置
    private int messagePos = 1;

    // キャンセルボタンの位置
    private int cancelButtonPos = 2;

    // moneyボタンの位置
    private int moneyButtonPos = 3;

    // heartボタンの位置
    private int heartButtonPos = 4;

    // ボタンテキストの位置
    private int buttonTextPos = 0;

    /// <summary>
    /// ダイアログの生成
    /// </summary>
    /// <returns></returns>
    public GameObject instantiateUpgradeDialog()
    {
        // ダイアログの生成
        GameObject dialogClone = Instantiate(dialog);

        // ダイアログ位置設定
        dialogClone.transform.SetParent(parent.transform);
        dialogClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        return dialogClone;
    }

    /// <summary>
    /// メッセージ設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public GameObject setMessage(GameObject view, string message)
    {
        view.transform.GetChild(messagePos).GetComponent<Text>().text = message;
        return view;
    }

    /// <summary>
    /// キャンセルボタンの設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setCancelButton(GameObject view, UnityEngine.Events.UnityAction e)
    {
        view.transform.GetChild(cancelButtonPos).GetComponent<Button>().onClick.AddListener(e);
        return view;
    }

    /// <summary>
    /// キャンセルボタンを非表示にする
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public GameObject setCancelHidden(GameObject view)
    {
        view.transform.GetChild(cancelButtonPos).gameObject.SetActive(false);
        return view;
    }

    /// <summary>
    /// moneyボタンの設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setMoneyButton(GameObject view, UnityEngine.Events.UnityAction e)
    {
        view.transform.GetChild(moneyButtonPos).GetComponent<Button>().onClick.AddListener(e);
        return view;
    }

    /// <summary>
    /// moneyボタンのテキストを設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public GameObject setMoneyButtonText(GameObject view, string text)
    {
        view.transform.GetChild(moneyButtonPos).GetChild(buttonTextPos).GetComponent<Text>().text = text;
        return view;
    }

    /// <summary>
    /// heartボタンの設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setHeartButton(GameObject view, UnityEngine.Events.UnityAction e)
    {
        view.transform.GetChild(heartButtonPos).GetComponent<Button>().onClick.AddListener(e);
        return view;
    }

    /// <summary>
    /// heartボタンのテキストを設定
    /// </summary>
    /// <param name="view"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public GameObject setHeartButtonText(GameObject view, string text)
    {
        view.transform.GetChild(heartButtonPos).GetChild(buttonTextPos).GetComponent<Text>().text = text;
        return view;
    }
}
