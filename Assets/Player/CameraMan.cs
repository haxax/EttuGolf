using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public static CameraMan Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform bcImg;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float scaleExtra = 1.2f;
    [SerializeField] private float minScale = 1f;
    [SerializeField] public List<Transform> targetForms = new List<Transform>();



    private Vector3 targetPoint = Vector3.zero;
    private Vector2 targetScale = Vector2.one;
    private float betterTargetScale = 1f;

    private Vector2 minXY = Vector2.zero;
    private Vector2 maxXY = Vector2.zero;

    private void OnValidate()
    {
        gameObject.ValidateComponent(ref cam);
    }

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else if (Instance != this)
        { Destroy(gameObject); }
    }

    void Update()
    {
        targetPoint = Vector2.zero;
        minXY = transform.position;
        maxXY = transform.position;
        foreach (Transform t in targetForms)
        {
            targetPoint += t.position;

            if (t.position.x < minXY.x) { minXY.x = t.position.x; }
            else if (t.position.x > maxXY.x) { maxXY.x = t.position.x; }
            if (t.position.y < minXY.y) { minXY.y = t.position.y; }
            else if (t.position.y > maxXY.y) { maxXY.y = t.position.y; }
        }

        targetPoint /= targetForms.Count;
        targetPoint.z = -10;

        transform.position += ((targetPoint - transform.position) * Time.deltaTime) * movementSpeed;


        targetScale = maxXY - minXY;
        targetScale.x /= cam.aspect;

        betterTargetScale = targetScale.x;
        if (targetScale.y > targetScale.x) { betterTargetScale = targetScale.y; }
        betterTargetScale /= 2f;
        if (betterTargetScale < minScale) { betterTargetScale = minScale; }
        betterTargetScale *= scaleExtra;

        cam.orthographicSize += ((betterTargetScale - cam.orthographicSize) * Time.deltaTime) * movementSpeed;

        bcImg.transform.localScale = Vector3.one * 4f * cam.orthographicSize;
    }
}
