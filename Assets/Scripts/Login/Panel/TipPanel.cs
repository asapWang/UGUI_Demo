using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Text infoText;
    public override void Init()
    {
        //初始化
        confirmBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.DestroyPanel<TipPanel>();
        });
    }

    public void SetInfo(string info)
    {
        infoText.text = info;
    }
}
