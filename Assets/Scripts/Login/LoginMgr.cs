using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance=new LoginMgr();
    public static LoginMgr Instance=>instance;
    private LoginData loginData;
    public LoginData LoginData => loginData;
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;
    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
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
