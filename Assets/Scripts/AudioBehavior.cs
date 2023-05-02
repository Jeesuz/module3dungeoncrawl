using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    AudioSource audioUse;
    public AudioClip levelMusic;
    bool playLevelmusic = true;
    public AudioClip bossMusic;
    bool playBossmusic = false;
    public AudioClip lowHealth;
    bool playDying = false;
    PlayerMovement playerScript;
    BossBehavior bossScript;
    GameObject boss;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        audioUse = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossScript = boss.GetComponent<BossBehavior>();
    }

    void StopTheMusic()
    {
        if (playLevelmusic == false && playBossmusic == false && playDying == false)
        {
            audioUse.Stop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerScript.HP < 4)
        {
            playLevelmusic = false;
            playBossmusic = false;
            StopTheMusic();
            playDying = true;
            if (!audioUse.isPlaying && playDying)
            {
                audioUse.PlayOneShot(lowHealth, 0.7f);
            }
        } else if (bossScript.OnScreen())
        {
            playLevelmusic = false;
            StopTheMusic();
            playBossmusic = true;
            if (!audioUse.isPlaying && playBossmusic)
            {
                audioUse.PlayOneShot(bossMusic, 0.7f);
            }
        } else if (!audioUse.isPlaying && playLevelmusic){
            audioUse.PlayOneShot(levelMusic, 0.7f);
        }
        StopTheMusic();

    }
}
