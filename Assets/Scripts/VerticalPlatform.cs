using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    public float waitTime;

    private PlatformEffector2D effector;


    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            effector.rotationalOffset = 90f;          
        }

        if (Input.GetKey(KeyCode.D))
        {
            effector.rotationalOffset = -90f;
        }
    }
}
