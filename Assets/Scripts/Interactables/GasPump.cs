using UnityEngine;

public class GasPump : MonoBehaviour, IInteractable
{
    [SerializeField] GasPumpGaugeDialog gasPumpGaugeDialog;
    [SerializeField] GasPumpGaugeDialogData gasPumpGaugeDialogData;
    public string Name { get => "Gas Pump"; }

    public void OnPlayerInteract()
    {
        DialogHelper.Instance.DisplayDialog(gasPumpGaugeDialog, gasPumpGaugeDialogData);
    }
}
