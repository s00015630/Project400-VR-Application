using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSprite : MonoBehaviour
{
    private float max = 1f;
    private float speed = 0.6f;
    public SpriteRenderer sprite;

    void Update()
    {
        sprite.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * speed, max));
    }

}
