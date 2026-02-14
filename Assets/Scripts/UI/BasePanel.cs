using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected bool isShow = false;
    protected float alphaSpeed = 5f;
    protected UnityAction onHideComplete;
    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }
    protected virtual void Start()
    {
        Init();
    }
    public abstract void Init();
    public virtual void Show()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    public virtual void Hide(UnityAction unityAction)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        onHideComplete = unityAction;
    }
    void Update()
    {
        if(isShow&&canvasGroup.alpha<1)
        {
            canvasGroup.alpha += Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else
        {
            if(!isShow&&canvasGroup.alpha>0)
            {
                canvasGroup.alpha -= Time.deltaTime * alphaSpeed;
                if (canvasGroup.alpha <= 0)
                {
                    canvasGroup.alpha = 0;
                    onHideComplete?.Invoke();
                }       
            }
        }         
    }
}
