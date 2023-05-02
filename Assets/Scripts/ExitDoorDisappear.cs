using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorDisappear : MonoBehaviour
{
    BossBehavior bossScript;
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        bossScript = boss.GetComponent<BossBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossScript.bossDead)
        {
            Destroy(gameObject);
        }
    }
}
