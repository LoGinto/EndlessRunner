using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    [SerializeField] float smoothness = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(transform.position.x,transform.position.y,offset.z+player.position.z);
        transform.position = Vector3.Lerp(transform.position,newPos,smoothness*Time.deltaTime);    
    }
}
