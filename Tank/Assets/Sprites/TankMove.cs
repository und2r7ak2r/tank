using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float speed = 3;
    public float number = 2;
    public AudioClip drivingSound;
    public AudioClip idleSound;

    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("VerticalPlayer"+number);
        float horizontal = Input.GetAxis("HorizontalPlayer"+number);
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if(dir!= Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            audio.clip = drivingSound;
            if(!audio.isPlaying)
            audio.Play();
        }else
        {
            audio.clip = idleSound;
            if (!audio.isPlaying)
            audio.Play();
        }
        
    }
}
