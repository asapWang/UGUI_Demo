using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button registerBtn;
    public Button confirmBtn;
    public Toggle autoLoginToggle;
    public Toggle rememberPWToggle;
    public InputField idInput;
    public InputField passwordInput;
    public override void Init()
    {
        //点击确定，验证输入的账号密码是否正确
        confirmBtn.onClick.AddListener(() =>
        {
            
        });
        //点击注册，销毁当前面板，打开注册面板
        registerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.DestroyPanel<LoginPanel>();
            //展示注册面板
        });

        //取消记住密码就取消自动登录
        rememberPWToggle.onValueChanged.AddListener((isOn) =>
        {
            if (!isOn)
            {
                autoLoginToggle.isOn = false;
            }
        });
        //自动登录就记住密码
        autoLoginToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                rememberPWToggle.isOn = true;
            }
        });
    }
    public override void Show()
    {
        base.Show();
        //加载登录数据
        LoginData loginData=LoginMgr.Instance.LoginData;
        idInput.text = loginData.id;
        autoLoginToggle.isOn = loginData.autoLogin;
        rememberPWToggle.isOn = loginData.rememberPW;
        if (loginData.rememberPW)
        {
            //如果记住密码，就把密码显示在输入框里
            passwordInput.text = loginData.password;
        }
        if(loginData.autoLogin)
        {
            //自动登录

        }
    }
}
