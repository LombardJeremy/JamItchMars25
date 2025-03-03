using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;

    [Header("SoundList")] 
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip rotateSound;
    [SerializeField] private AudioClip landSound;

    private void Start()
    {
        PlayerController.PuyoMove += MoveSound;
        PlayerController.PuyoRotate += RotateSound;
    }

    private void OnDestroy()
    {
        PlayerController.PuyoMove -= MoveSound;
        PlayerController.PuyoRotate -= RotateSound;
    }

    private void MoveSound()
    {
        sfxAudioSource.PlayOneShot(moveSound);
    }
    
    private void RotateSound()
    {
        sfxAudioSource.PlayOneShot(rotateSound);
    }
    
    private void LandSound()
    {
        sfxAudioSource.PlayOneShot(landSound);
    }
}
