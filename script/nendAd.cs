using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NendUnityPlugin.AD.Video;

public class nendAd : MonoBehaviour
{
    private int fullboardSpotId = 485520;
    private string fullboardApiKey = "a88c0bcaa2646c4ef8b2b656fd38d6785762f2ff";
    private int videoAdSpotId = 802559;
    private string videoAdApiKey = "e9527a2ac8d1f39a667dfe0f7c169513b090ad44";

    private NendAdInterstitialVideo videoAd;

    private bool loadNow = false;
    private bool loadFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        //OSに応じてIDを設定する必要がある
        startVideoAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadNow)
        {
            Debug.Log("広告の読み込みを開始します。");
            videoAd.Load();
            loadNow = true;
        }

        if (loadFlag)
        {
            Debug.Log("広告を表示します。");
            loadFlag = false;
            videoAd.Show();
        }
    }

    public void startVideoAd()
    {
        videoAd = NendAdInterstitialVideo.NewVideoAd(videoAdSpotId, videoAdApiKey);
        videoAd.AddFallbackFullboard(fullboardSpotId, fullboardApiKey);
        videoAd.IsMuteStartPlaying = false;

        //広告ロード成功のコールバック
        videoAd.AdLoaded += (instance) =>
        {
            Debug.Log("広告のロードに成功しました。");
            loadFlag = true;
        };

        //広告ロード失敗のコールバック
        videoAd.AdFailedToLoad += (instance, errorCode) =>
        {
            Debug.Log("広告のロードに失敗しました。");
            loadFlag = false;
            loadNow = false;
        };

        //再生失敗のコールバック
        videoAd.AdFailedToPlay += (instance) => {
            Debug.Log("広告の再生に失敗しました。");
            loadNow = false;
        };

        //広告表示のコールバック
        videoAd.AdShown += (instance) => {
            Debug.Log("広告を表示しました。");
        };

        //再生開始のコールバック
        videoAd.AdStarted += (instance) => {
            Debug.Log("広告の再生を開始しました。");
        };

        //再生中断のコールバック
        videoAd.AdStopped += (instance) => {
            Debug.Log("広告の再生を中断しました。");
        };

        //再生完了のコールバック
        videoAd.AdCompleted += (instance) => {
            Debug.Log("広告の再生が完了しました。");
        };

        //広告クリックのコールバック
        videoAd.AdClicked += (instance) => {
            Debug.Log("広告がクリックされました。");
        };

        //オプトアウトクリックのコールバック
        videoAd.InformationClicked += (instance) => {
            Debug.Log("広告の情報が開かれました。");
        };

        //広告クローズのコールバック
        videoAd.AdClosed += (instance) => {
            Debug.Log("広告が閉じられました。");
            loadNow = false;
        };


    }

    //広告オブジェクトが削除されたときに実行
    void OnDestroy()
    {
        Debug.Log("OnDestroy()");
        videoAd.Release();
    }

}
