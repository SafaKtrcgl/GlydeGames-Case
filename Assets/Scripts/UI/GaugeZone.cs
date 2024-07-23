using UnityEngine;
using UnityEngine.UI;

public class GaugeZone : MonoBehaviour
{
    [SerializeField] private Image zoneImage;

    public void Init(float weightPercent, GasPumpGaugeAreaColor gaugeColor, float rotationIndex)
    {
        zoneImage.fillAmount = weightPercent;

        transform.localRotation = Quaternion.Euler(Vector3.forward * rotationIndex);

        switch (gaugeColor)
        {
            case GasPumpGaugeAreaColor.Green:
                zoneImage.color = Color.green;
                break;
            case GasPumpGaugeAreaColor.Yellow:
                zoneImage.color = Color.yellow;
                break;
            case GasPumpGaugeAreaColor.Red:
                zoneImage.color = Color.red;
                break;
            default:
                zoneImage.color = Color.white;
                break;
        }
    }
}
