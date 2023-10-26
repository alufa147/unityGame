using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogController : MonoBehaviour
{
    // 親
    public Canvas parentCanvas;

    // ダイアログ本体
    public GameObject dialog;

    // メッセージの階層
    private int messagePos = 1;

    // キャンセルボタンの階層
    private int cancelButtonPos = 2;

    // OKボタンの階層
    private int okButtonPos = 3;

    // レベルアップviewの階層
    private int levelUpViewPos = 4;

    /// <summary>
    /// ダイアログを生成する
    /// </summary>
    /// <returns></returns>
    public GameObject instantiateDialog()
    {
        // ダイアログ生成
        GameObject dialogClone = Instantiate(dialog);

        // 親要素設定
        dialogClone.transform.SetParent(parentCanvas.transform);

        // 画面中央に表示
        dialogClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        return dialogClone;
    }

    /// <summary>
    /// メッセージを設定する
    /// </summary>
    /// <param name="view"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public GameObject setDialogMessage(GameObject view, string message)
    {
        view.transform.GetChild(messagePos).GetComponent<Text>().text = message;
        return view;
    }

    /// <summary>
    /// キャンセルボタンの処理を設定する
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setCancelButton(GameObject view, UnityEngine.Events.UnityAction e){

        view.transform.GetChild(cancelButtonPos).GetComponent<Button>().onClick.AddListener(e);
        return view;
    }

    /// <summary>
    /// OKボタンの処理を設定する
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setOkButton(GameObject view, UnityEngine.Events.UnityAction e)
    {

        view.transform.GetChild(okButtonPos).GetComponent<Button>().onClick.AddListener(e);
        return view;
    }

    /// <summary>
    /// レベルアップviewを表示する
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject levelUpViewEnable(GameObject view)
    {

        view.transform.GetChild(levelUpViewPos).gameObject.SetActive(true);
        return view;
    }

    /// <summary>
    /// キャンセルボタンを隠す
    /// </summary>
    /// <param name="view"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public GameObject setCancelButtonHidden(GameObject view)
    {

        view.transform.GetChild(cancelButtonPos).gameObject.SetActive(false);
        return view;
    }
}
