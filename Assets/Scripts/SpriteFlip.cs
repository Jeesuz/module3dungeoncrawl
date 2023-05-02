using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpriteFlip : MonoBehaviour
{
    SpriteRenderer sr;
    AIPath aipath;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        aipath = transform.parent.GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipX = aipath.desiredVelocity.x < 0f;
    }
}
