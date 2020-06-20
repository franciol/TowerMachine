using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerEnemyScript : MonoBehaviour
{
    private bool canShoot = true;

    List<GameObject> towers = new List<GameObject>();
    GameObject PlayerMain = null;
    public GameObject bullet;
    public int timeBetweenShots = 5;
    public float bulletSpeed = 2000;
    public AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if(PlayerMain != null)
        {
            transform.LookAt(PlayerMain.transform);
        }
        else if (towers.Count > 0)
        {
            if (towers[0])
            {
                transform.LookAt(towers[0].transform);
            }
            else
            {
                towers.RemoveAt(0);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (towers.Count > 0)
        {
            if (towers[0])
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
        if (other.tag == "Towers" )
        {
            towers.Add(other.gameObject);
        }
        else if(other.tag == "PlayerBase")
        {
            PlayerMain = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Towers" )
        {
            towers.Remove(other.gameObject);
        }
        else if (other.tag == "PlayerBase")
        {
            PlayerMain = null;
        }
    }


    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    

}
