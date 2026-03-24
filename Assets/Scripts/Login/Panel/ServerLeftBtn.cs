using UnityEngine;
using UnityEngine.UI;

public class ServerLeftBtn : MonoBehaviour
{
    public Button leftBtn;
    public Text serverRangeText;
    private int beginIndex;
    private int endIndex;
    private void Start()
    {
        //点击左侧按钮显示对应服务器列表
        leftBtn.onClick.AddListener(()=>
        {
            UIManager.Instance.GetPanel<ChooseServerPanel>().UpdateList(beginIndex,endIndex);
        });
    }

    public void InitInfo(int begin,int end)
    {
        beginIndex = begin;
        endIndex = end;
        serverRangeText.text = beginIndex + " - " + endIndex + "区";
    }

}
