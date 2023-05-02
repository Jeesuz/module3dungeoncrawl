using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    GameObject player;
    Animator animator;
    AudioSource audioUse;
    public AudioClip open;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioUse = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject == player)
        {
            PlayerMovement playerMove = other.gameObject.GetComponent<PlayerMovement>();
            if (!playerMove.crossGet)
            {
                audioUse.PlayOneShot(open, 0.7f);
                playerMove.crossGet = true;
                animator.Play("chestopen");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
