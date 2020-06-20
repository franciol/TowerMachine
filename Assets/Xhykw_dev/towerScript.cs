using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    private bool canShoot = true;

    List<GameObject> enemys = new List<GameObject>();
    public GameObject bullet;
    public int timeBetweenShots = 1;
    public float bulletSpeed = 2000;
    public AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (enemys.Count > 0)
        {
            if (enemys[0])
            {
                transform.LookAt(enemys[0].transform);
            }
            else 
            {
                enemys.RemoveAt(0);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemys.Count > 0)
        {
            if (enemys[0])
            {
                if (canShoot)
                {
                    canShoot = false;
                    GameObject bullet1 = Instantiate(bullet, new Vector3(0, 3.5f, 0) + transform.position, transform.rotation);
                    bullet1.GetComponent<Rigidbody>().AddForce((bullet1.transform.forward * bulletSpeed));
                    StartCoroutine("StartCountdown");
                    audioSource.PlayOneShot(shootSound);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy1" | other.tag == "Enemy2")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy1" | other.tag == "Enemy2")
        {
            enemys.Remove(other.gameObject);
        }
    }


    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}
