using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorOpening : MonoBehaviour
{
    AudioSource audioUse;
    public AudioClip open;
    public AudioClip close;

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            audioUse.PlayOneShot(open, 0.5f);
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
            gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
            gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        audioUse.PlayOneShot(close, 0.5f);
        gameObject.GetComponent<TilemapRenderer>().enabled = true;
        gameObject.GetComponent<TilemapCollider2D>().isTrigger = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioUse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
