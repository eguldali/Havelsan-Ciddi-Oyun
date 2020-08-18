
//Parent class for XEnemy, YEnemy, ZEnemy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected float health, fullHealth;
 
    protected float moveSpeed;

    [SerializeField]protected Image healthBar;

    [Header("Audio")]
    [SerializeField] AudioSource enemyAudioSource;
    [SerializeField] AudioClip enemyHitSound, enemyDestroyedSound;

   //Set Enemy pramaeters when spawned.
    public virtual void FillEnemyParameters() { }

    //Update enemy health & healthBar for enemy, 
    //Hit amount, total damage,(if enemey detroyed) killed enemy count & killed enemy range list for game info keeper, 
    //Killed enemy amount for wave controller.
    public void GetHit(float damage, float bulletRange)
    {
        
        GameInfoKeeper.Instance.UpdateHitAmount();
        GameInfoKeeper.Instance.UpdateTotalDamage(damage);

        if(damage >= health)
        {
            health = 0;
            healthBar.fillAmount = 0;

            if (PlayerPrefs.GetInt("SoundOpt") == 0)
            {
                enemyAudioSource.clip = enemyDestroyedSound;
                enemyAudioSource.Play();
            }

            Destroy(gameObject,1f);
          
            WaveController.Instance.UpdateEnemyCounter();
            GameInfoKeeper.Instance.UpdateKilledEnemy();
            GameInfoKeeper.Instance.UpdateRangeList(bulletRange);
            
        }

        else
        {
            health -= damage;
            healthBar.fillAmount = health / fullHealth;
            if (PlayerPrefs.GetInt("SoundOpt") == 0)
            {
                enemyAudioSource.clip = enemyHitSound;
                enemyAudioSource.Play();
            }
        }
    }

    // Looking for collision with Vision Border and Player. When enemy collided with vision border, can be seen by Main Camera. When enemy collided with player, call PlayerController's enemy collision function. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VisionBorder")
        {
            ShowYourSelfToCamera();
        }

        else if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().GotHitFromEnemy();
        }
    }

    // Enemy faces player and move towards to it with given speed.
    protected void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), moveSpeed*Time.deltaTime);
        transform.LookAt(GameObject.Find("Player").transform);
    }

    // Enemy can be seen to player's main camera.
    public virtual void ShowYourSelfToCamera() { }

    // Enemy can be seen to player's LRF camera.
    public virtual void ShowYourSelfToRadar() { }

    // Enemy hide itself from player's LRF camera.
    public virtual void HideYourSelFromRadar() { }

}
