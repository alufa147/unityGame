using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using System;
using System.Threading;

public class heartController : MonoBehaviour
{
    DBManager db;

    private int heart = 0;

    SynchronizationContext context;

    private void Start()
    {
        _= loadHeart();
        context = SynchronizationContext.Current;
    }

    //サーバからハートをロードしてハートの数を返す
    public async Task loadHeart()
    {
        
        db = new DBManager();
        if(db.getUserId() == "")
        {
            registerUser();
            
        } else
        {
             await signIn();
             await FirebaseDatabase.DefaultInstance.GetReference("users/" + db.getUserId()+ "/heart").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("失敗");
                }
                else
                {
                    DataSnapshot snapshot = task.Result;
                    string json = snapshot.GetRawJsonValue();
                    heart = int.Parse(json);
                    Debug.Log(getHeart());
                    context.Post(_ => {
                        try {
                            GameObject.FindWithTag("viewController").GetComponent<viewController>().updateHeartView(getHeart());
                        } catch (Exception e) { Debug.Log(e); }
                    }, null);
                    
                }
                
            });
            await signOut();
        } 
    }


    public int getHeart()
    {
        return heart;
    }

    //サーバにハートの数を登録する
    public async void setHeart(int heart)
    {
        await signIn();

        DatabaseReference root = FirebaseDatabase.DefaultInstance.RootReference;

        DatabaseReference reference = root.Child("users").Child(db.getUserId());
        Dictionary<string, object> s = new Dictionary<string, object>();
        s.Add("heart", this.heart + heart);
        await reference.UpdateChildrenAsync(s);

        await signOut();
        await loadHeart();
    }

    //ユーザIDを登録
    public async void registerUser()
    {

        //サーバDBに登録

        await signIn();
        Debug.Log("ユーザを登録します。");

        string userId = (UnityEngine.Random.Range(1, 10000) * UnityEngine.Random.Range(1, 10000) * UnityEngine.Random.Range(1, 10000)).ToString();  

        DatabaseReference root = FirebaseDatabase.DefaultInstance.RootReference;

        DatabaseReference reference = root.Child("users").Child(userId);
        Dictionary<string, object> t = new Dictionary<string, object>();
        t.Add("heart", 0);
        t.Add("registerDate", DateTime.Now.ToString());
        await reference.UpdateChildrenAsync(t);

        await signOut();


        //ローカルに登録
        db.setUserId(userId);
    }

    //サインイン
    public async Task signIn()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        //匿名認証
        await auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("承認キャンセル");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("承認失敗" + task.Exception);
                return;
            }
        });
        Debug.Log("承認成功");
    }

    //サインアウト
    public async Task signOut()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        string userId = null;

        //匿名認証していたら解放
        if(auth.CurrentUser != null)
        {
            userId = auth.CurrentUser.UserId;

            //匿名ユーザでいっぱいにならないように消す
            await auth.CurrentUser.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("削除に失敗しました。");
                    return;
                }
            });
            auth.SignOut();
        }
        if(userId != null)
        {
            Debug.Log("サインアウトに成功 ：" + userId);
        }
    }




}
