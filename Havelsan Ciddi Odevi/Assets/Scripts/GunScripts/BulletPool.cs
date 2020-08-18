
//Singelton class for object pooling. Keeps deactivated bullets and reuse them.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;

    private Queue<Bullet> bulletsInQueue = new Queue<Bullet>();

    public static BulletPool Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    //Look for Queue for a reusable bullet object. If queue has not have any, call function for new bullet.
    public Bullet GetBulletFromQueue()
    {
        if(bulletsInQueue.Count == 0)
        {
            AddBulletToQueue();
        }

        return bulletsInQueue.Dequeue();
    }
    
    //Clones a new bullet and add it to bullet queue.
    void AddBulletToQueue()
    {
        Bullet cloneBullet = Instantiate(bulletPrefab);
        cloneBullet.gameObject.SetActive(false);
        bulletsInQueue.Enqueue(cloneBullet);
    }

    //Add a deactivated bullet back to queue.
    public void ReturnToQueue(Bullet killedBullet)
    {
        bulletsInQueue.Enqueue(killedBullet);
    }


   
}
