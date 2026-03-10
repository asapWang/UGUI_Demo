using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.NewPanel<TipPanel>();
        UIManager.Instance.GetPanel<TipPanel>().SetInfo("欢迎来到主界面");
    }
}
