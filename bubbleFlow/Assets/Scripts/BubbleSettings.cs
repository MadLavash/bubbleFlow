using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSettings 
{
    public float startBaseSpeed;
    public float endBaseSpeed;
    public float speedSizeCoeff;
    public Vector3 startPosition;
    public float gameTime;
    public float gameTimeLeft;
    public float maxFlyHeight;

    public BubbleSettings(float startBaseSpeed, float endBaseSpeed, float speedSizeCoeff, Vector3 startPosition, 
        float gameTime, float gameTimeLeft, float maxFlyHeight)
    {
        this.startBaseSpeed = startBaseSpeed;
        this.endBaseSpeed = endBaseSpeed;
        this.speedSizeCoeff = speedSizeCoeff;
        this.startPosition = startPosition;
        this.gameTime = gameTime;
        this.gameTimeLeft = gameTimeLeft;
        this.maxFlyHeight = maxFlyHeight;
    }
	
}
