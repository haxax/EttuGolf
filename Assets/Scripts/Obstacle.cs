using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject swingPFX;
    [SerializeField] private GameObject ballPFX;
    [SerializeField] private GameObject randomAudio;
    [SerializeField] private AudioClip hitAudio;

    public void PlaySwingHit(Vector2 point)
    {
        Instantiate(swingPFX, point, Quaternion.identity);
       AudioSource source = Instantiate(randomAudio, point, Quaternion.identity).GetComponent<AudioSource>();
        source.clip = hitAudio;
    }

    public void PlayBallHit(Vector2 point)
    {
        Instantiate(ballPFX, point, Quaternion.identity);
        AudioSource source = Instantiate(randomAudio, point, Quaternion.identity).GetComponent<AudioSource>();
        source.clip = hitAudio;
    }
}
