using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfCLubCOllider : MonoBehaviour
{
    [SerializeField] private GolfClub club;
    [SerializeField] private LayerMask hitableMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform headPoint;

    private List<GameObject> collisions = new List<GameObject>();

    private Vector2 pointPFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject go in collisions)
        { if (go == collision.gameObject) { return; } }

        collisions.Add(collision.gameObject);

        pointPFX = collision.ClosestPoint(headPoint.position);

        if (collision.gameObject.IsOnLayerMask(hitableMask))
        {
            collision.attachedRigidbody.AddForce(club.ActiveSwingForce * club.ActiveSwingDirection, ForceMode2D.Impulse);

            collision.gameObject.GetComponent<GolfBall>().PlaySwingHit(pointPFX);
        }
        else
        {
            collision.gameObject.GetComponent<Obstacle>().PlaySwingHit(pointPFX);
        }
    }

    public void Clear()
    {
        collisions.Clear();
    }

}
