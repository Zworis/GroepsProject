using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Vector2 turn;
    public float Sensitivity;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.y = 1;
        transform.position = position;
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if ((turn.y <= -89 && Input.GetAxis("Mouse Y") <= 0) || (turn.y >= 0 && Input.GetAxis("Mouse Y") >= 0))
            {
                turn.x += Input.GetAxis("Mouse X") * Sensitivity;
                transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
            }
            else
            {
                turn.y += Input.GetAxis("Mouse Y") * Sensitivity;
                turn.x += Input.GetAxis("Mouse X") * Sensitivity;
                transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Speed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Speed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, -Speed);
        }
        if (Input.GetKey(KeyCode.E) && GameObject.Find("Main Camera").transform.localPosition.z <= -1)
        {
            GameObject.Find("Main Camera").transform.Translate(0, 0, GameObject.Find("Main Camera").transform.localPosition.z * -0.05f);
            //GameObject.Find("Main Camera").GetComponent<"Camera">().farClipPlane = 100 - GameObject.Find("Main Camera").transform.position.z;
        }
        else if (Input.GetKey(KeyCode.Q) && GameObject.Find("Main Camera").transform.localPosition.z >= -100)
        {
            GameObject.Find("Main Camera").transform.Translate(0, 0, GameObject.Find("Main Camera").transform.localPosition.z * 0.05f);
        }
        Camera.main.farClipPlane = 100 - (GameObject.Find("Main Camera").transform.localPosition.z * 1.8f);
        Speed = 0.2f * -(GameObject.Find("Main Camera").transform.localPosition.z / 22);
    }
}
