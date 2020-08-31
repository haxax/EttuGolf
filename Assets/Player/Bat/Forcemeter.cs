using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcemeter : MonoBehaviour
{
    [SerializeField] private GolfClub target;
    [SerializeField] private Transform pivot;
    [SerializeField] private Vector2 maxScale = Vector2.one;

    void LateUpdate()
    {
        if (!target.IsHeld && pivot.gameObject.activeInHierarchy) { pivot.gameObject.SetActive(false); return; }
        else if (target.IsHeld && !pivot.gameObject.activeInHierarchy) { pivot.gameObject.SetActive(true); }
        else if (!pivot.gameObject.activeInHierarchy) { return; }

        pivot.localScale = maxScale * target.CurrentForce;
        pivot.localEulerAngles = new Vector3(0,0,target.CurrentSwingDirection.ToAngle360(Vector2.up));
    }
}
