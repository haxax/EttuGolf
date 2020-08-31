using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode moveLeftKey;
    [Space(20)]
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private LayerMask jumpRayMask;
    [SerializeField] private List<Transform> rayPoints = new List<Transform>();
    [SerializeField] private float rayDistance = 1f;
    [Space(20)]
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float maxMovementSpeed = 3f;
    [SerializeField] private List<Transform> rotateOnMove = new List<Transform>();
    [SerializeField] private AnimationCurve footAnimRotationCurve;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;

    private float animationStep = 0.5f;
    private float animationDir = 1f;

    private float prevDir = 1f;

    private void OnValidate()
    {
        gameObject.ValidateComponent(ref rb);
    }

    private void Start()
    {
        CameraMan.Instance.targetForms.Add(transform);
    }

    void Update()
    {
        if (Input.GetKey(moveLeftKey)) { Move(-1f); }
        if (Input.GetKey(moveRightKey)) { Move(1f); }
        if (Input.GetKeyDown(jumpKey)) { Jump(); }
        AnimationUpdate();
    }


    private void Jump()
    {
        for (int i = 0; i < rayPoints.Count; i++)
        {
            if (RayCheck(rayPoints[i]))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                break;
            }
        }
    }

    private bool RayCheck(Transform point)
    {
        bool result = false;

        RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.down, rayDistance, jumpRayMask);
        Debug.DrawRay(point.position, Vector2.down * rayDistance, Color.red, 1f);
        if (hit)
        {
            result = true;
        }
        return result;
    }


    private void Move(float direction)
    {
        if (prevDir != direction)
        {
            prevDir = direction;
            foreach (Transform t in rotateOnMove)
            {
                t.localScale = t.localScale.SetX(direction);
            }

        }



        rb.AddForce(Vector2.right * direction * moveForce * Time.deltaTime, ForceMode2D.Impulse);
        if (Mathf.Abs(rb.velocity.x) > maxMovementSpeed) { rb.velocity = new Vector2(maxMovementSpeed * (rb.velocity.x / Mathf.Abs(rb.velocity.x)), rb.velocity.y); }
    }

    private void AnimationUpdate()
    {
        animationStep += animationDir * (Mathf.Abs(rb.velocity.x) / maxMovementSpeed) * Time.deltaTime * 2f;
        if (animationStep > 1f) { animationStep = 1 - (animationStep - 1); animationDir = -1f; }
        if (animationStep < 0f) { animationStep *= -1f; animationDir = 1f; }
        rightFoot.localEulerAngles = rightFoot.localEulerAngles.SetZ(footAnimRotationCurve.Evaluate(animationStep));
        leftFoot.localEulerAngles = leftFoot.localEulerAngles.SetZ(footAnimRotationCurve.Evaluate(
            0.5f - (animationStep - 0.5f)
            ));
    }
}
