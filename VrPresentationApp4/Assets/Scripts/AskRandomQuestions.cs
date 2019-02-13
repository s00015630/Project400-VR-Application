using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskRandomQuestions : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    public void GetRandomQuestion()
    {
        audioSource.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }

    
}
