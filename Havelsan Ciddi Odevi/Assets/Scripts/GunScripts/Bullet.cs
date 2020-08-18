
//This class is used for bullet actions after tank's fire.

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float myLoadedEnergy;

    private Vector3 firstPosition;

    float myVelocity = 10;


    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update

    // Keeps first position that bullet has created.
    void OnEnable()
    {
        firstPosition = transform.position;
        
    }

    //Give velocity and life time to bullet. Velocity is same for each canon. Lifetime depends on shooting range of canon type.
    public void FireBullet()
    {
        rb.velocity = transform.forward * myVelocity;
        Invoke("KillBullet", lifeTime);
    }

    //Deactivate bullet and return it to Bullet Pool for reusing same object. 
    public void KillBullet()
    {
        rb.velocity = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
        BulletPool.Instance.ReturnToQueue(this);

    }

    //When bullet hits an enemy, deal damage to it and deactivate bullet.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            
            other.GetComponent<Enemy>().GetHit(CalculateDamage(transform.position), (float)Math.Round(Vector3.Distance(transform.position, firstPosition), 2));
            CancelInvoke();
            KillBullet();
        }
    }

    //Calculates damage of bullet depends on startPosition, hitPosition, loadedEnergy.
    float CalculateDamage(Vector3 hitPosition)
    {
        float range = Vector3.Distance(hitPosition, firstPosition);

        float usedEnergyForRange = myLoadedEnergy * (float)Math.Log(range, 20);
       
        float damage = myLoadedEnergy - usedEnergyForRange;

        return damage;

    }
}
