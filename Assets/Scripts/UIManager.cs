using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _leftText;
    [SerializeField] TMP_Text _winText;

    public static UIManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Duplicate UIManager", this);
            Destroy(this);
        }

        Assert.IsNotNull(_scoreText);
        Assert.IsNotNull(_leftText);
        Assert.IsNotNull(_winText);
    }

    public void SetScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    public void SetLeft(int left)
    {
        _leftText.text = $"Left: {left}";
    }

    internal void DisplayWinText()
    {
        _winText.enabled = true;
    }
}
