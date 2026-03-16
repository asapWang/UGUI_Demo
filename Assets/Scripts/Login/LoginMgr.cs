using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance=new LoginMgr();
    public static LoginMgr Instance=>instance;
    private LoginData loginData;
    public LoginData LoginData => loginData;
    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
    }

    public void SaveData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
}
