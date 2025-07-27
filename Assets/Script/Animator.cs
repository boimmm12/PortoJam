using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Animator : MonoBehaviour
{

    [SerializeField] List<Sprite> walkSprites;
    SpriteAnimator currentAnim;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentAnim = new SpriteAnimator(walkSprites, spriteRenderer);
        currentAnim.Start();
    }

    // Update is called once per frame
    void Update()
    {
        currentAnim.HandleUpdate();
        return;
    }
}
