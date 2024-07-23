using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GasPumpGaugeDialog : DialogBase
{
    [SerializeField] private Image indicatorImage;
    [SerializeField] private GaugeZone gaugeZonePrefab;
    [SerializeField] private Transform zoneHolder;

    private const float oscilationValue = 180f;
    private const float zoneWeightConstant = 2f;
    private const int radiantConstant = 360;

    private Sequence _oscilationSequence;

    public override void Init(ScriptableDialogData dialogData)
    {
        base.Init(dialogData);

        indicatorImage.transform.localRotation = Quaternion.Euler(Vector3.forward * oscilationValue / 2);

        GasPumpGaugeDialogData gasPumpGaugeDialogData = dialogData as GasPumpGaugeDialogData;

        _oscilationSequence = DOTween.Sequence();

        _oscilationSequence.SetLoops(-1);
        _oscilationSequence.Append(indicatorImage.transform.DOLocalRotate(Vector3.forward * -oscilationValue, gasPumpGaugeDialogData.oscalationTime / 2f, RotateMode.LocalAxisAdd));
        _oscilationSequence.Append(indicatorImage.transform.DOLocalRotate(Vector3.forward * oscilationValue, gasPumpGaugeDialogData.oscalationTime / 2f, RotateMode.LocalAxisAdd));

        var totalWeight = 0f;
        gasPumpGaugeDialogData.gasPumpGaugeContent.ForEach(x => totalWeight += x.weight);

        var rotationIndex = 0f;
        foreach (var gaugeContent in gasPumpGaugeDialogData.gasPumpGaugeContent)
        {
            var zone = Instantiate(gaugeZonePrefab, zoneHolder);
            var relativeWeight = (gaugeContent.weight / totalWeight) / zoneWeightConstant;
            zone.Init(relativeWeight, gaugeContent.areaColor, rotationIndex);
            rotationIndex -= relativeWeight * radiantConstant;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DialogHelper.Instance.RemoveDialog(this);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _oscilationSequence.Pause();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            _oscilationSequence.Play();
        }
    }
}
