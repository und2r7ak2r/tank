using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    public GameObject ShellExplosionPrefab;
    public AudioClip shellExplosion;
    public void OnTriggerEnter(Collider collider)
    {
        GameObject.Instantiate(ShellExplosionPrefab, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(shellExplosion, transform.position);
        GameObject.Destroy(this.gameObject);
        if(collider.tag=="tank")
        {
            collider.SendMessage("TakeDamage");
        }
    }
}
