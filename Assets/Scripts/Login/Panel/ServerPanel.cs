using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ServerPanel : BasePanel
{
    public Button backBtn;
    public Button enterBtn;
    public Button changeBtn;
    public Text serverText;
    public override void Init()
    {
        //点击返回按钮，返回登录界面
        backBtn.onClick.AddListener(() =>
        {
            if(LoginMgr.Instance.LoginData.autoLogin)
            {
                //如果自动登录勾选上了，取消自动登录，否则永远回不到登录界面了
                LoginMgr.Instance.LoginData.autoLogin = false;
            }
            UIManager.Instance.DestroyPanel<ServerPanel>();
            UIManager.Instance.NewPanel<LoginPanel>();
        });
        //点击选服按钮，进入选服面板
        changeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.DestroyPanel<ServerPanel>();
            UIManager.Instance.NewPanel<ChooseServerPanel>();
        });
        //点击进入按钮，进入游戏
        enterBtn.onClick.AddListener(() =>
        {
            //此刻才真正保存服务器id
            LoginMgr.Instance.SaveLoginData();
            //销毁服务器面板和背景面板
            UIManager.Instance.DestroyPanel<ServerPanel>();
            UIManager.Instance.DestroyPanel<BKPanel>();
            //切场景
            SceneManager.LoadScene("GameScene"); 
        });
    }
    public override void Show()
    {
        base.Show();
        //显示服务器信息
        LoginData loginData = LoginMgr.Instance.LoginData;
        if (loginData.serverID != -1)
        {
            ServerInfo serverInfo = LoginMgr.Instance.ServerData[loginData.serverID - 1];
            serverText.text = serverInfo.id + "区  " + serverInfo.name;
        }
        else
        {
            serverText.text = "无服务器记录";
        }
    }
}