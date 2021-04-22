using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    //public float offsetX;
    public float offsety;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x, 
            playerTransform.position.y, 
            transform.position.z);

        Vector3 tempY = transform.position;
        tempY.y = playerTransform.position.y;
        tempY.y += offsety;
        transform.position = tempY;
    }

    // Update is called once per frame
    /*void LateUpdate()
    {
        //Camera X axis
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        temp.x += offsetX;
        transform.position = temp;

        //Camera Y axis
        Vector3 tempy = transform.position;
        tempy.y = playerTransform.position.y;
        tempy.y += offsety;
        transform.position = tempy;
    }*/
}
