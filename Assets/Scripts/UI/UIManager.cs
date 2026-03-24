using System.Collections.Generic;
using UnityEngine;
public class UIManager
{
    private Transform canvasTransform;
    //存储已经创建的面板，键为面板名称，值为面板脚本组件
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //单例模式
    private static UIManager instance=new UIManager();
    public static UIManager Instance => instance;
    private UIManager()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        //换场景时不会销毁
        GameObject.DontDestroyOnLoad(canvasTransform.gameObject);
    }
   

    //创建面板
    //约束泛型T必须是BasePanel的子类，后面代码中可以直接使用BasePanel的属性和方法,但是BasePanel不能直接当做T来使用
    public T NewPanel<T>() where T : BasePanel{
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        GameObject panelPrefab = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //setParent是transform下的函数，第二个参数为false表示保持预设的缩放和旋转
        panelPrefab.transform.SetParent(canvasTransform, false);
        //Dic中存储的是BasePanel类型的脚本，不是GameObject，所以需要GetComponent来获取脚本组件
        T panel = panelPrefab.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.Show();
        return panel;
    }
    //销毁面板
    //isFade表示是否淡出销毁，默认为true
    public void DestroyPanel<T>(bool isFade=true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if(isFade==true)
            {
                //使用lambda表达式，更简单
                panelDic[panelName].Hide(() =>
                {
                    //虽然脚本对象即将失效，但字典中仍然持有这个引用。如果你不调用 Remove，字典会继续保留一个指向已销毁对象的无效引用
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            };
        }
    }
    //获取面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
