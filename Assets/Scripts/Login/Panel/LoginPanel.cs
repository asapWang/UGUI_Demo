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
            if(LoginMgr.Instance.CheckInfo(idInput.text, passwordInput.text))
            {
                //登录成功，保存登录数据
                LoginData loginData = LoginMgr.Instance.LoginData;
                loginData.id = idInput.text;
                loginData.password = passwordInput.text;
                loginData.rememberPW = rememberPWToggle.isOn;
                loginData.autoLogin = autoLoginToggle.isOn;
                LoginMgr.Instance.SaveLoginData();
                //提示登录成功
                TipPanel tipPanel = UIManager.Instance.NewPanel<TipPanel>();
                tipPanel.SetInfo("登录成功");
                //销毁当前面板，进入服务器面板
                UIManager.Instance.DestroyPanel<LoginPanel>();
            }
            else
            {
                //提示账号或密码错误
                TipPanel tipPanel = UIManager.Instance.NewPanel<TipPanel>();
                tipPanel.SetInfo("账号或密码错误");
                //清空密码输入框
                idInput.text = "";
                passwordInput.text = "";
            }
        });
        //点击注册，销毁当前面板，打开注册面板
        registerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.DestroyPanel<LoginPanel>();
            //展示注册面板
            UIManager.Instance.NewPanel<RegisterPanel>();
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
            //自动登录，销毁当前面板
            TipPanel tipPanel = UIManager.Instance.NewPanel<TipPanel>();
            tipPanel.SetInfo("自动登录成功");
            //UIManager.Instance.DestroyPanel<LoginPanel>();
            //进入服务器面板

        }
    }
    public void SetAccountInfo(string id, string password)
    {
        idInput.text = id;
        passwordInput.text = password;
    }
}
