using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    public GameObject shellPrefab;
    private Transform FirePosition;
    public KeyCode fireKey=KeyCode.Space;
    public float shellSpeed = 15;
    public AudioClip fireSound;
    void Start()
    {
        FirePosition = transform.Find("Fireposition");
    }

    
    void Update()
    {
        if(Input.GetKeyDown(fireKey))
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            GameObject go=GameObject.Instantiate(shellPrefab, FirePosition.position, FirePosition.rotation);
            go.GetComponent<Rigidbody>().velocity=go.transform.forward*shellSpeed;
        }
    }
}
