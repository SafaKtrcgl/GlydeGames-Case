using Singleton;

public class DialogHelper : MonoSingleton<DialogHelper>
{
    public T DisplayDialog<T>(T dialogPrefab, ScriptableDialogData dialogData) where T : DialogBase
    {
        InputHelper.Instance.OnDialogDisplay();
        var dialog = Instantiate(dialogPrefab, transform);
        dialog.Init(dialogData);

        return dialog;
    }

    public void RemoveDialog<T>(T dialogInstance) where T : DialogBase
    {
        Destroy(dialogInstance.gameObject);
        InputHelper.Instance.OnDialogHide();
    }
}
