using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTower : MonoBehaviour
{

    public int life = 5;
    AudioSource audioS;
    private bool waitHit = true;

    public GameObject hitParticles;
    public GameObject deathParticles;

    public AudioClip hitClip;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            if (waitHit)
            {
                Instantiate(hitParticles, other.transform.position, other.transform.rotation);
                audioS.PlayOneShot(hitClip);
                waitHit = false;
                StartCoroutine("HitStamp");
                life -= 1;
                if (life <= 0)
                {
                    Destroy(gameObject);
                }
            }

            Destroy(other.gameObject);
        }
        
    }

    


    IEnumerator HitStamp()
    {
        yield return new WaitForSeconds(0.1f);
        waitHit = true;
    }

    private void OnDestroy()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }


}
