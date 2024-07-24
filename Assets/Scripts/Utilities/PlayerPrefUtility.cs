using UnityEngine;

public static class PlayerPrefsUtility
{
    private const string _gasPumpHighScore = "GasPumpHighScore";

    public static float GetGasPumpHighScore()
    {
        return PlayerPrefs.GetFloat(_gasPumpHighScore, 0);
    }

    public static void SetGasPumpHighScore(float highScore)
    {
        PlayerPrefs.SetFloat(_gasPumpHighScore, highScore);
    }
}
