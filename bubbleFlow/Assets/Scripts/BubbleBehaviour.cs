using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    private float currentSpeed
    {
        get
        {
            return GetCurrentBaseSpeed() / settings.speedSizeCoeff;
        }
    }
    public Transform thisTransform { get; protected set; }
    private bool isInitialized = false;
    private BubbleSettings settings;

    public static BubbleSpawner.BubbleExploded bubbleExploded;

    public void Initialize(BubbleSettings settings)
    {
        if (isInitialized)
        {
            return;
        }
        else
        {
            isInitialized = true;
        }

        this.settings = settings;
        BubbleSpawner.isGameStarted += HandleGameStatus;

        thisTransform = transform;

        thisTransform.localScale *= settings.speedSizeCoeff;
        thisTransform.position = settings.startPosition;

        GetComponent<Renderer>().material.SetColor("_Color", RandromizeColor());
    }

    private static Color RandromizeColor()
    {
        float r, g, b;
        r = UnityEngine.Random.Range(0, 1f);
        g = UnityEngine.Random.Range(0, 1f);
        b = UnityEngine.Random.Range(0, 1f);

        return new Color(r, g, b);
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        thisTransform.Translate(Vector3.up * Time.deltaTime * currentSpeed);

        if(thisTransform.position.y >= settings.maxFlyHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (bubbleExploded != null)
        {
            bubbleExploded(settings.speedSizeCoeff);
        }

        Destroy(gameObject);
    }
    
    private void HandleGameStatus(bool isStarted)
    {
        if (!isStarted)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private float GetCurrentBaseSpeed()
    {
        return Mathf.Lerp(settings.startBaseSpeed, settings.endBaseSpeed, (1 - settings.gameTimeLeft / settings.gameTime));
    }

    private void OnDestroy()
    {
        BubbleSpawner.isGameStarted -= HandleGameStatus;
    }



}
