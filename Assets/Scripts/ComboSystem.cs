using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboSystem : MonoBehaviour
{
    public int comboCount;
    public float expireTime=2f;
    private float countdown;
    public TextMeshProUGUI comboText;
    public Color fadeColor;
    private Color originalColor;
    public float fadeTime;
    public int scoreMultiplier=1;
    public int multiplierThreshold = 5;

    private void Start()
    {
        originalColor = comboText.color;
        comboCount = 0;
        countdown = expireTime;
    }

    private void Update()
    {
        if (comboCount >= multiplierThreshold)
            scoreMultiplier = 2;
        if(comboCount == 0)
            comboText.SetText("");
        else
            comboText.SetText("x " + comboCount.ToString());
        if(countdown <= 0)
        {
            ResetCombo();
        }
        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        if(countdown < expireTime/2)
        {
            comboText.color = Color.Lerp(comboText.color, fadeColor, fadeTime * Time.deltaTime);
        }
    }

    public void ScorePoints(int points)
    {
        comboCount = comboCount + points;
        countdown = expireTime;
        comboText.color = originalColor;
    }

    public void ResetCombo()
    {
        comboCount = 0;
        scoreMultiplier = 1;
    }
}
