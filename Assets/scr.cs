using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class scr : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM3", 38400);
    private Rigidbody rigigBody;
    private Vector3 movement;
    private int forward;


    void Start()
    {
        stream.Open();
        rigigBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        string value = stream.ReadLine();
        string[] vec3 = value.Split(',');

        int vertical = int.Parse(vec3[0]);
        //string horizontal = vec3[1];

        Debug.Log(vertical);
        if (vec3[0] != "" && vec3[1] != "")
        {
            if (vertical >= 100 && vertical <= 300)
            {
                forward = 0;
            }
            if (vertical < 100)
            {
                forward = -5;
            }
            if (vertical > 300)
            {
                forward = 5;
            }
            Debug.Log(vertical);
            stream.BaseStream.Flush();
            movement = transform.forward.normalized * forward;
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = movement * 1 * Time.deltaTime;
        rigigBody.MovePosition(rigigBody.position + velocity);
    }
}