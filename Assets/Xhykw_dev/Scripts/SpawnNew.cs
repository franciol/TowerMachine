using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNew : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    int enemysCount1;
    int enemysCount2;
    bool inst = true;
    public Transform Alvo;
    public int number_to_generate1;
    public int number_to_generate2;
    private int generated1 = 0;
    private int generated2 = 0;
    public float time_wait = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemysCount1 = GameObject.FindGameObjectsWithTag("Enemy1").Length;
        enemysCount2 = GameObject.FindGameObjectsWithTag("Enemy2").Length;

        if (inst)
        {
            if (generated1 < number_to_generate1 )
            {
                generated1 += 1;
                inst = false;
                GameObject a = Instantiate(Enemy1, gameObject.transform.position, transform.rotation);
                a.GetComponent<MoveEnemyTo>().Alvo = Alvo;
                StartCoroutine("waitInstantiate");
            }
            else if (generated2 < number_to_generate2)
            {
                generated2 += 1;
                inst = false;
                GameObject a = Instantiate(Enemy2, gameObject.transform.position, transform.rotation);
                a.GetComponent<MoveEnemyTo>().Alvo = Alvo;
                StartCoroutine("waitInstantiate");
            }
        }

    }


    IEnumerator waitInstantiate()
    {
        yield return new WaitForSeconds(time_wait);
        inst = true;
    }

    public void newWave(int enemys1, int enemys2)
    {
        number_to_generate1 = enemys1;
        number_to_generate2 = enemys2;
        print(enemys1 + " " + enemys2);
        generated1 = 0;
        generated2 = 0;


    }
    public bool end_wave()
    {
        return ((generated1 == number_to_generate1) & (generated2 == number_to_generate2) & (GameObject.FindGameObjectsWithTag("Enemy1").Length==0) & (GameObject.FindGameObjectsWithTag("Enemy2").Length==0));
    }

    
}
