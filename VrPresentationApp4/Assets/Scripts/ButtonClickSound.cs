using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{

    public AudioClip audio;
    private Button button
    {
        get
        {
            return GetComponent<Button>();
        }
    }
    private AudioSource source { get{ return GetComponent<AudioSource>(); } }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = audio;
        source.playOnAwake = false;
        button.onClick.AddListener(() => PlayButtonClick());
    }

    void PlayButtonClick()
    {
        source.PlayOneShot(audio);
    }
}
