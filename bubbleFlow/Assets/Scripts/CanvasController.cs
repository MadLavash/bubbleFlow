using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text finalScore;

    private bool isGameStarted = false;
    private float currentScore = 0;
    private float baseScore = 1;

    private void Awake()
    {
        BubbleSpawner.isGameStarted += HandleGameStatus;
        BubbleBehaviour.bubbleExploded += UpdateScore;
    }

    private void HandleGameStatus(bool isStarted)
    {
        isGameStarted = isStarted;

        if (!isStarted)
        {
            finalScore.text = scoreText.text;
            finalScore.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            return;
        }

        UpdateTime();
    }

    private void UpdateTime()
    {
        timeText.text = string.Format("Time: {0:00}", Mathf.FloorToInt(BubbleSpawner.gameTimeLeft) );
    }

    private void UpdateScore(float speedSizeCoeff)
    {
        currentScore += baseScore / speedSizeCoeff;

        scoreText.text = "Score: " + currentScore.ToString("F2");
    }

    private void OnDestroy()
    {
        BubbleSpawner.isGameStarted -= HandleGameStatus;
        BubbleBehaviour.bubbleExploded -= UpdateScore;
    }

}
