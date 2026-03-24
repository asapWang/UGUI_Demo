using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightBtn : MonoBehaviour
{
    public Button rightBtn;
    public Text serverName;
    public Image stateImg;
    public Image newImg;
    private ServerInfo serverInfo;
    private void Start()
    {
        //点击右侧按钮
        rightBtn.onClick.AddListener(() =>
        {
            //记下选择的服务器ID，但未真正保存登录数据，直到点击进入游戏才保存
            LoginData loginData = LoginMgr.Instance.LoginData;
            loginData.serverID = serverInfo.id;
            //销毁当前面板
            UIManager.Instance.DestroyPanel<ChooseServerPanel>();
            //显示服务器面板
            UIManager.Instance.NewPanel<ServerPanel>();
            
        });
    }
    //初始化按钮信息
    public void InitInfo(ServerInfo info)
    {
        serverInfo = info;
        serverName.text = info.id + "区  " + info.name;
        newImg.gameObject.SetActive(serverInfo.isNew);
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Login");
        switch (serverInfo.state)
        {
            case 0://流畅
                stateImg.sprite = atlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 1://无
                stateImg.gameObject.SetActive(false);
                break;
            case 2://火爆
                stateImg.sprite = atlas.GetSprite("ui_DL_huobao_01");
                break;
            case 3://繁忙
                stateImg.sprite = atlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 4://维护
                stateImg.sprite = atlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }

}
