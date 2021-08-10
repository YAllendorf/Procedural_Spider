using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public GameObject terrain;
    public float cameraHeight; //ASSIGN IN INSPECTOR
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(terrain.transform.position, new Vector3(0, 1, 0), speed * Time.deltaTime);
        this.transform.position = new Vector3(this.transform.position.x, cameraHeight, this.transform.position.z);
    }
}
