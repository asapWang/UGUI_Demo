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
            UIManager.Instance.DestroyPanel<ServerPanel>();
            UIManager.Instance.NewPanel<LoginPanel>();
        });
        //点击选服按钮，进入选服面板
        changeBtn.onClick.AddListener(() =>
        {
            
        });
        //点击进入按钮，进入游戏
        enterBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.DestroyPanel<ServerPanel>();
            //切场景
            SceneManager.LoadScene("GameScene"); 
        });
    }
    public override void Show()
    {
        base.Show();
        //显示服务器信息
        LoginData loginData = LoginMgr.Instance.LoginData;
        
    }
}