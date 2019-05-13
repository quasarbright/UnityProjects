using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSelector : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public Sprite[] sprites = {null};
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.sprite = sprite;
    }

    void OnEnable()
    {
        Start();
    }
}
