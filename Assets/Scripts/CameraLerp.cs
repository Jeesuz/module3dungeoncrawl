using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] string cameraState = "static";
    float lastFrameTime;
    float screenH;
    float screenW;
    float startX;
    float endX;
    float startY;
    float endY;
    float lerpTime;
    float transitionTime = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        screenH = Camera.main.orthographicSize * 2f;
        screenW = Camera.main.orthographicSize * 2f * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPlayer != null)
        {
        if (cameraState == "static")
        {
            if (Mathf.Abs(cameraPlayer.transform.position.x - transform.position.x) > screenW / 2)
            {
                cameraState = "movingx";
                startX = transform.position.x;
                float directionx = Mathf.Sign(cameraPlayer.transform.position.x - transform.position.x);
                endX = transform.position.x + screenW * directionx;
                lerpTime = 0;
            }
            if (Mathf.Abs(cameraPlayer.transform.position.y - transform.position.y) > screenH / 2)
            {
                cameraState = "movingy";
                startY = transform.position.y;
                float directiony = Mathf.Sign(cameraPlayer.transform.position.y - transform.position.y);
                endY = transform.position.y + screenH * directiony;
                lerpTime = 0;
            }
            Time.timeScale = 1;
        } else if (cameraState == "movingx")
        {
            transform.position = new Vector3(Mathf.Lerp(startX, endX, lerpTime / transitionTime), endY, -10);
            lerpTime += Time.unscaledDeltaTime;
            Time.timeScale = 0;
            if (transform.position.x == endX)
            {
                cameraState = "static";
            }
        } else if ( cameraState == "movingy")
        {
            transform.position = new Vector3(endX, Mathf.Lerp(startY, endY, lerpTime / transitionTime), -10);
            lerpTime += Time.unscaledDeltaTime;
            Time.timeScale = 0;
            if (transform.position.y == endY)
            {
                cameraState = "static";
            }
        }
        }
    }
}
