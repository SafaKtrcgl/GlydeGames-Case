using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GasPumpGaugeDialog : DialogBase
{
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Button gaugeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GaugeZone gaugeZonePrefab;
    [SerializeField] private Transform zoneHolder;
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private const float oscilationValue = 180f;
    private const float zoneWeightConstant = 2f;
    private const int radiantConstant = 360;

    private const string currentScore = "Current Score: ";
    private const string highestScore = "Highest Score: ";

    private Sequence _oscilationSequence;
    private Coroutine _countdownRoutine;
    private GasPumpGaugeDialogData _gasPumpGaugeDialogData;
    private float _totalWeight;
    private float _currentScore = 1f;
    private bool _isBussy = false;

    private void UpdateCurrentScoreText() => currentScoreText.text = currentScore + _currentScore;
    private void UpdateHighScoreText() => highestScoreText.text = highestScore + PlayerPrefsUtility.GetGasPumpHighScore();

    public override void Init(ScriptableDialogData dialogData)
    {
        base.Init(dialogData);

        indicatorImage.transform.localRotation = Quaternion.Euler(Vector3.forward * oscilationValue / 2);

        gaugeButton.onClick.AddListener(() =>
        {
            if (_isBussy) return;
            StartCoroutine(OnGaugeClicked());
        });
        exitButton.onClick.AddListener(OnCloseButtonClick);

        _gasPumpGaugeDialogData = dialogData as GasPumpGaugeDialogData;

        _oscilationSequence = DOTween.Sequence();
        _oscilationSequence.SetLoops(-1);
        _oscilationSequence.Append(indicatorImage.transform.DOLocalRotate(Vector3.forward * -oscilationValue, _gasPumpGaugeDialogData.oscalationTime / 2f, RotateMode.LocalAxisAdd));
        _oscilationSequence.Append(indicatorImage.transform.DOLocalRotate(Vector3.forward * oscilationValue, _gasPumpGaugeDialogData.oscalationTime / 2f, RotateMode.LocalAxisAdd));

        _totalWeight = 0f;
        _gasPumpGaugeDialogData.gasPumpGaugeContent.ForEach(x => _totalWeight += x.weight);

        var rotationIndex = 0f;
        foreach (var gaugeContent in _gasPumpGaugeDialogData.gasPumpGaugeContent)
        {
            var zone = Instantiate(gaugeZonePrefab, zoneHolder);
            var relativeWeight = (gaugeContent.weight / _totalWeight) / zoneWeightConstant;
            zone.Init(relativeWeight, gaugeContent.areaColor, rotationIndex);
            rotationIndex -= relativeWeight * radiantConstant;
        }

        UpdateCurrentScoreText();
        UpdateHighScoreText();

        _countdownRoutine = StartCoroutine(TimeCountdown(_gasPumpGaugeDialogData.timeLimit));
    }

    public void OnCloseButtonClick()
    {
        Close();
    }

    public IEnumerator OnGaugeClicked()
    {
        _isBussy = true;

        float GetInspectorRotationValue(float eulerAngle)
        {
            if (eulerAngle > 180f) return eulerAngle - 360f;

            return eulerAngle;
        }

        _oscilationSequence.Pause();
        var indicatorRelevantProgress = GetInspectorRotationValue((indicatorImage.transform.localEulerAngles.z) - (oscilationValue / 2f)) / -oscilationValue;
        RewardPlayer(GetCurrentZone(indicatorRelevantProgress));

        yield return new WaitForSeconds(.25f);

        _oscilationSequence.Play();

        _isBussy = false;
    }

    private void RewardPlayer(GasPumpGaugeAreaColor hitAreaColor)
    {
        switch (hitAreaColor)
        {
            case GasPumpGaugeAreaColor.White:
                _currentScore = 1f;
                break;
            case GasPumpGaugeAreaColor.Green:
                _currentScore *= 2f;
                currentScoreText.transform.DOPunchScale(Vector2.one, .15f);
                break;
            case GasPumpGaugeAreaColor.Yellow:
                _currentScore *= 1.5f;
                break;
            case GasPumpGaugeAreaColor.Red:
                _currentScore *= .5f;
                break;
        }

        UpdateCurrentScoreText();
    }

    private GasPumpGaugeAreaColor GetCurrentZone(float indicatorRelevantProgress)
    {
        var weightProgress = 0f;
        foreach (var gaugeContent in _gasPumpGaugeDialogData.gasPumpGaugeContent)
        {
            weightProgress += gaugeContent.weight / _totalWeight;
            if (weightProgress > indicatorRelevantProgress)
            {
                return gaugeContent.areaColor;
            }
        }

        return _gasPumpGaugeDialogData.gasPumpGaugeContent[^1].areaColor;
    }

    private IEnumerator TimeCountdown(float time)
    {
        yield return new WaitForSeconds(time);

        OnTimeOut();
    }

    private void OnTimeOut()
    {
        if (_currentScore > PlayerPrefsUtility.GetGasPumpHighScore())
        {
            PlayerPrefsUtility.SetGasPumpHighScore(_currentScore);
        }

        Close();
    }

    public override void Close()
    {
        _oscilationSequence?.Kill();
        if (_countdownRoutine != null)
            StopCoroutine(_countdownRoutine);

        gaugeButton.onClick.RemoveListener(() => StartCoroutine(OnGaugeClicked()));
        exitButton.onClick.RemoveListener(OnCloseButtonClick);

        base.Close();
    }
}
