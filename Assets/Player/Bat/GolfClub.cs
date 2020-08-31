using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GolfClub : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    [SerializeField] private float maxForce = 10f;
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private Vector2 swingDurationScale = new Vector2();
    [SerializeField] private GolfCLubCOllider colliderMaster;

    private Vector2 currentSwingDirection = Vector2.zero;
    public Vector2 CurrentSwingDirection => currentSwingDirection;
    private float currentForce = 0.1f;
    public float CurrentForce => currentForce;
    private float holdDirection = 1f;

    //
    private float activeSwingForce = 0f;
    private float activeSwingDuration = 1f;
    private Vector2 activeSwingDirection = Vector2.zero;
    //

    private bool isHeld = false;
    public bool IsHeld => isHeld;

    public float ActiveSwingForce { get => activeSwingForce; }
    public Vector2 ActiveSwingDirection { get => activeSwingDirection; }

    private bool isSwingging = false;
    private float swingProgress = 0.0f;

    private void Update()
    {
        InputUpdate();
        SwingUpdate();
    }

    protected abstract void InputUpdate();

    protected void Hold(Vector2 point)
    {
        isHeld = true;
        currentForce += holdDirection * (Time.deltaTime / holdDuration);
        currentSwingDirection = new Vector2(point.x - pivot.position.x, point.y - pivot.position.y);

        if (currentForce < 0f) { currentForce *= -1; holdDirection = 1f; }
        else if (currentForce > 1f) { currentForce = 1f - (currentForce - 1f); holdDirection = -1f; }
    }

    protected void Release(Vector2 point)
    {
        isHeld = false;
        activeSwingForce = currentForce * maxForce;
        activeSwingDuration = swingDurationScale.x + ((1.0f - currentForce) * (swingDurationScale.y - swingDurationScale.x));
        activeSwingDirection = new Vector2(point.x - pivot.position.x, point.y - pivot.position.y);

        colliderMaster.Clear();
        isSwingging = true;
        colliderMaster.gameObject.SetActive(true);
        swingProgress = 0.0f;
        currentForce = 0.1f;
        holdDirection = 1f;
    }

    private void SwingUpdate()
    {
        if (!isSwingging) { return; }
        swingProgress += Time.deltaTime / activeSwingDuration;
        pivot.localEulerAngles = new Vector3(0, 0, swingProgress * 360f);
        if (swingProgress > 1f)
        {
            isSwingging = false;
            colliderMaster.gameObject.SetActive(false);

            swingProgress = 0.0f;
        }
    }
}