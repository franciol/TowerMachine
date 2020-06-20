using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveEnemyTo : MonoBehaviour
{
    private NavMeshAgent Agent;
    public Transform Alvo;
    private Rigidbody rgbd;
    private bool onFloor = false;
    private bool WaitHit = true;
    public int life = 5;
    public GameObject hitParticles;
    public GameObject deathParticles;
    private AudioSource audioSource;
    public AudioClip hitClip;
    public int pointsWorth = 1;


    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        rgbd = GetComponent<Rigidbody>();
        Agent.SetDestination(Alvo.position);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Bullet")
        {
            if (WaitHit)
            {
                life -= 1;
                WaitHit = false;
                StartCoroutine("HitStamp");
                
                if (life <= 0)
                {
                    FindObjectOfType<ResetWave>().add_points(pointsWorth);
                    Destroy(gameObject);
                }
                else
                {
                    audioSource.PlayOneShot(hitClip);
                }
                Instantiate(hitParticles, other.transform.position, other.transform.rotation);
            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "PlayerBase")
        {
            //causar dano 
            FindObjectOfType<cameraCntrl>().damageBase(pointsWorth * 2);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }

    IEnumerator HitStamp()
    {
        yield return new WaitForSeconds(0.01f);
        WaitHit = true;
    }

    private void OnDestroy()
    {
        Instantiate(deathParticles,transform.position,Quaternion.identity);
    }
}
