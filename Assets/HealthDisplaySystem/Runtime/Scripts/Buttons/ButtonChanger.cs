using UnityEngine;

public abstract class ButtonChanger : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected float Amount = 10f;

    protected virtual void Start()
    {
        if (Health == null)
        {
            enabled = false;
        }
    }

    public abstract void OnClick();
}
