using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance=new LoginMgr();
    public static LoginMgr Instance=>instance;
    private LoginData loginData;
    public LoginData LoginData => loginData;
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;
    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }

    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    //注册账号
    public bool RegisterUser(string id,string password)
    {
        if (registerData.accountDic.ContainsKey(id))
        {
            return false;
        }
        registerData.accountDic.Add(id, password);
        SaveRegisterData();
        return true;
    }
    //清除登录数据
    public void ClearLoginData()
    {
        loginData.rememberPW = false;
        loginData.autoLogin = false;
        loginData.serverID = -1;
    }
    //验证账号密码是否正确
    public bool CheckInfo(string id,string password)
    {
        if (registerData.accountDic.ContainsKey(id))
        {
            if (registerData.accountDic[id] == password)
                return true;   
        }
        return false;
    }
}
