using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] private GameObject swingPFX;
    [SerializeField] private GameObject randomAudio;
    [SerializeField] private AudioClip hitAudio;

    private void Start()
    {
        CameraMan.Instance.targetForms.Add(transform);
    }

    public void PlaySwingHit(Vector2 point) {
        Instantiate(swingPFX, point, Quaternion.identity);
        AudioSource source = Instantiate(randomAudio, point, Quaternion.identity).GetComponent<AudioSource>();
        source.clip = hitAudio;
    }
}
