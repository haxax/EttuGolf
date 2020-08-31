using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private AnimationCurve xCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve yCurve = new AnimationCurve();

    private Vector2 direction;
    private Vector2 position;

    private float baseY = 0f;

    void Start()
    {
        baseY = transform.position.y;
        direction = new Vector2(Random.Range((int)0, 2), Random.Range((int)0, 2));
        if (direction.x == 0) { direction.x = -1; }
        if (direction.y == 0) { direction.y = -1; }
        position = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    void Update()
    {
        position += Time.deltaTime * speed * direction;
        if (position.x > 1f) { position.x = 1 - (position.x - 1); direction.x = -1f; }
        if (position.y > 1f) { position.y = 1 - (position.y - 1); direction.y = -1f; }

        if (position.x < 0f) { position.x *= -1f; direction.x = 1f; }
        if (position.y < 0f) { position.y *= -1f; direction.y = 1f; }

        transform.position = new Vector3(xCurve.Evaluate(position.x), baseY + yCurve.Evaluate(position.y), 0f);
    }
}
