using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHp : MonoBehaviour
{
    public int hp = 100;
    public GameObject tankExplosion;
    public AudioClip TankExplosionSound;
    public void TakeDamage()
    {
        if (hp <= 0) return;
        hp -= Random.Range(0, 50);
        if(hp<=0)
        {
            Instantiate(tankExplosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(TankExplosionSound, transform.position);    
            Destroy(gameObject);
        }
    }


}
