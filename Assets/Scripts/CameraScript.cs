using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform CamTransform;
    public Transform Player;
    bool startFollowing = false;

    void Start()
    {
        CamTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Player.transform.position.x >= 0f)
            startFollowing = true;

        if (startFollowing)
            CamTransform.position = new Vector3(Player.position.x, CamTransform.position.y, CamTransform.position.z);
    }
}
