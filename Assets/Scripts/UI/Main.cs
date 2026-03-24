using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.NewPanel<BKPanel>();
        UIManager.Instance.NewPanel<LoginPanel>();
    }
}
