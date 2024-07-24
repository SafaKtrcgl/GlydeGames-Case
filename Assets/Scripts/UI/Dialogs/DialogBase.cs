using UnityEngine;

public abstract class DialogBase : MonoBehaviour
{
    virtual public void Init(ScriptableDialogData dialogData)
    {

    }

    virtual public void Close()
    {
        DialogHelper.Instance.RemoveDialog(this);
    }
}
