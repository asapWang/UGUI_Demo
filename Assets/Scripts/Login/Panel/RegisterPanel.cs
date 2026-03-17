using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button cancelBtn;
    public Button confirmBtn;
    public InputField idInput;
    public InputField passwordInput;
    public override void Init()
    {
        cancelBtn.onClick.AddListener(() =>
        {
            //销毁当前面板，返回登录界面
            UIManager.Instance.DestroyPanel<RegisterPanel>();
            UIManager.Instance.NewPanel<LoginPanel>();
        });
        confirmBtn.onClick.AddListener(() =>
        {
            //验证输入是否合法
            if (idInput.text.Length < 6 || passwordInput.text.Length < 6)
            {
                //提示输入不合法
                TipPanel tipPanel = UIManager.Instance.NewPanel<TipPanel>();
                tipPanel.SetInfo("账号和密码必须都大于6位");
                //清空输入框                
                idInput.text = "";
                passwordInput.text = "";
                return;
            }
            //调用LoginMgr的注册方法，注册成功则返回登录界面，否则提示账号已存在
            if (LoginMgr.Instance.RegisterUser(idInput.text, passwordInput.text))
            {
                //返回登录界面,将账号密码传回登录界面
                LoginPanel loginPanel = UIManager.Instance.NewPanel<LoginPanel>();
                loginPanel.SetAccountInfo(idInput.text, passwordInput.text);
                //销毁当前面板
                UIManager.Instance.DestroyPanel<RegisterPanel>();
            }
            else
            {
                //提示账号已存在
                TipPanel tipPanel = UIManager.Instance.NewPanel<TipPanel>();
                tipPanel.SetInfo("账号已存在");
                //清空输入框                
                idInput.text = "";
                passwordInput.text = "";
            }
            
        });
    }
}
