using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class ChooseServerPanel : BasePanel
{
    public Button lastServerBtn;
    public Text lastServerText;
    public Image lastServerStateImg;
    public Text serverRangeText;

    public ScrollRect leftScrollRect;
    public ScrollRect rightScrollRect;

    public override void Init()
    {
        //左侧生成服务器列表
        List<ServerInfo> serverData = LoginMgr.Instance.ServerData;
        int count=serverData.Count/5+(serverData.Count%5==0?0:1);
        for (int i = 0; i < count; i++)
        {
            GameObject leftBtnPrefab = Instantiate(Resources.Load<GameObject>("UI/ServerLeftBtn"));
            leftBtnPrefab.transform.SetParent(leftScrollRect.content, false);
            ServerLeftBtn serverLeftBtn = leftBtnPrefab.GetComponent<ServerLeftBtn>();
            serverLeftBtn.InitInfo(i * 5 + 1, serverData.Count < (i + 1) * 5 ? serverData.Count : (i + 1) * 5);
        }
    }

    public override void Show()
    {
        base.Show();
        //显示上次登录的服务器信息
        LoginData loginData = LoginMgr.Instance.LoginData;
        if (loginData.serverID != -1)
        {
            // 如果上次选择过服务器，根据服务器ID获取服务器信息并显示
            ServerInfo lastServerInfo = LoginMgr.Instance.ServerData[loginData.serverID - 1 ];
            lastServerText.text = lastServerInfo.id + "区  " + lastServerInfo.name;
            SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Login");
            switch (lastServerInfo.state)
            {
                case 0://流畅
                    lastServerStateImg.sprite = atlas.GetSprite("ui_DL_liuchang_01");
                    break;
                case 1://无
                    lastServerStateImg.gameObject.SetActive(false);
                    break;
                case 2://火爆
                    lastServerStateImg.sprite = atlas.GetSprite("ui_DL_huobao_01");
                    break;
                case 3://繁忙
                    lastServerStateImg .sprite = atlas.GetSprite("ui_DL_fanhua_01");
                    break;
                case 4://维护
                    lastServerStateImg.sprite = atlas.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        else
        {
            lastServerText.text = "暂无服务器记录";
            lastServerStateImg.gameObject.SetActive(false);
        }

        UpdateList(1, 5>LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
        
    }

    //右侧生成服务器列表
    public void UpdateList(int begin,int end)
    {
        //更新服务器范围文本
        serverRangeText.text = "服务器 " + begin + "-" + end;
        //清除右侧服务器列表
        foreach (Transform child in rightScrollRect.content){
            Destroy(child.gameObject);
        }
        //动态生成右侧服务器列表
        for (int i = begin-1; i < end; i++)
        {
            List<ServerInfo> serverData = LoginMgr.Instance.ServerData;
            GameObject rightBtnPrefab = Instantiate(Resources.Load<GameObject>("UI/ServerRightBtn"));
            rightBtnPrefab.transform.SetParent(rightScrollRect.content, false);
            ServerRightBtn serverRightBtn = rightBtnPrefab.GetComponent<ServerRightBtn>();
            serverRightBtn.InitInfo(serverData[i]);
        }
    }
}
