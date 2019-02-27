using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSprite : MonoBehaviour
{
    public float fadeOutTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (FadeOuSpritet (GetComponent<SpriteRenderer>()));
    }

    IEnumerator FadeOuSpritet(SpriteRenderer _sprite)
    {
        Color color = _sprite.color;
        while (color.a > 0f)
        {
            color.a = Time.deltaTime / fadeOutTime;
            _sprite.color = color;

            if (color.a <= 0f)
            {
                color.a = 0.0f;
            }
            yield return null;
        }
        _sprite.color = color;
    }
}
