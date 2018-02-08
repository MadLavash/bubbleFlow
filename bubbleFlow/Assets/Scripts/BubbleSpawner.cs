using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    private float gameTime;
    [SerializeField]
    private float startBaseSpeed;
    [SerializeField]
    private float endBaseSpeed;
    [SerializeField]
    private GameObject bubbleFab;

    private float bubbleBaseRadius;
    private Transform bubbleParentTransform;
    private Vector2 minValuesOfScreenInWorld;
    private Vector2 maxValuesOfScreenInWorld;
    public static float gameTimeLeft { get; private set; }
    private IEnumerator spawnBubbleRoutine;

    private const float BUBBLE_Z_POSITION = -7f;
    private const float TIME_BETWEEN_SPAWNING_BUBBLES = 0.4f;

    public delegate void BubbleExploded(float speedSizeCoeff);
    public delegate void SimpleBool(bool isTrue);

    public static event SimpleBool isGameStarted;

    private void Start()
    {
        bubbleParentTransform = transform;
        gameTimeLeft = gameTime;
        CalculateSizeOfScreenInUnits();
        GetBubbleBaseRadius();

        spawnBubbleRoutine = SpawnBubbleRoutine();
        StartCoroutine(spawnBubbleRoutine);

        if(isGameStarted != null)
        {
            isGameStarted(true);
        }
    }

    private void CalculateSizeOfScreenInUnits()
    {
        minValuesOfScreenInWorld = Camera.main.ScreenToWorldPoint(Camera.main.pixelRect.min);          
        maxValuesOfScreenInWorld = Camera.main.ScreenToWorldPoint(Camera.main.pixelRect.max);
    }

    private void GetBubbleBaseRadius()
    {
        bubbleBaseRadius = bubbleFab.GetComponent<SphereCollider>().radius * bubbleFab.transform.localScale.x;
    }

    IEnumerator SpawnBubbleRoutine()
    {
        while(gameTimeLeft > 0)
        {
            if (SpawnBubble())
            {
                yield return new WaitForSeconds(TIME_BETWEEN_SPAWNING_BUBBLES);
            }
            else
            {
                yield return null;
            }
        }
    }

    bool SpawnBubble()
    {
        float speedSizeCoeff = UnityEngine.Random.Range(0.5f, 1.5f);
        Vector3 bubblePosition = GeneratePosition();

        if (!Physics.CheckSphere(bubblePosition, bubbleBaseRadius * speedSizeCoeff))
        {
            BubbleSettings settings = new BubbleSettings(startBaseSpeed, endBaseSpeed, speedSizeCoeff, bubblePosition, gameTime,
                gameTimeLeft, (maxValuesOfScreenInWorld.y + bubbleBaseRadius * speedSizeCoeff));

            BubbleBehaviour bubble = Instantiate(bubbleFab, bubbleParentTransform).GetComponent<BubbleBehaviour>();
            bubble.Initialize(settings);
            return true;
        }

        return false;
    }

    Vector3 GeneratePosition()
    {
        float x, y, z;

        x = UnityEngine.Random.Range(minValuesOfScreenInWorld.x - bubbleBaseRadius, maxValuesOfScreenInWorld.x + bubbleBaseRadius);
        y = minValuesOfScreenInWorld.y + bubbleBaseRadius;
        z = UnityEngine.Random.Range(BUBBLE_Z_POSITION, BUBBLE_Z_POSITION * 3);

        return new Vector3(x, y, z);
    }

    private void Update()
    {       
        if (gameTimeLeft <= 0)
        {
            return;
        }

        gameTimeLeft -= Time.deltaTime;

        if(gameTimeLeft <= 0)
        {
            if (isGameStarted != null)
            {
                isGameStarted(false);
            }
        }
    }



}
