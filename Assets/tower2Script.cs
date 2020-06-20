using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower2Script : MonoBehaviour
{
    private bool canShoot = true;
    private bool right = true;
    List<GameObject> enemys = new List<GameObject>();
    public GameObject bullet;
    public float timeBetweenShots = 1.0f;
    public float bulletSpeed = 2000;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public float x, y, z;

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
        if (enemys.Count > 0)
        {
            if (enemys[0])
            {
                if (canShoot)
                {
                    canShoot = false;
                    if (right)
                    {
                        GameObject bullet1 = Instantiate(bullet, new Vector3(-x, y, z) + transform.position, transform.rotation);
                        bullet1.GetComponent<Rigidbody>().AddForce((bullet1.transform.forward * bulletSpeed));
                    }
                    else
                    {
                        GameObject bullet1 = Instantiate(bullet, new Vector3(x, y, z) + transform.position, transform.rotation);
                        bullet1.GetComponent<Rigidbody>().AddForce((bullet1.transform.forward * bulletSpeed));
                    }
                    right = !right;
                    
                    StartCoroutine("StartCountdown");
                    audioSource.PlayOneShot(shootSound);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy1" | other.tag == "Enemy2")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy1" | other.tag == "Enemy2")
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

