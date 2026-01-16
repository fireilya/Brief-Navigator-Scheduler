using UnityEngine;

public class RiskInfluenceMark
{
    public bool IsHappened;
    public float InfluenceCoeff;
    public string RiskMessage;

    public RiskInfluenceMark() => Reset();

    public void Reset()
    {
        IsHappened = false;
        InfluenceCoeff = 1.0f;
        RiskMessage = string.Empty;
    }
    
}