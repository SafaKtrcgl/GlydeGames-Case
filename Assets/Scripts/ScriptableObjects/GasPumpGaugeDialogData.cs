using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GasPumpGaugeDialogData : ScriptableDialogData
{
    public List<GasPumpGaugeAreaData> gasPumpGaugeContent;
    public float timeLimit;
    public float oscalationTime;
}
