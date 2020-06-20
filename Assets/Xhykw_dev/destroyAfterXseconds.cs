using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterXseconds : MonoBehaviour
{
    public float seconds = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DieIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DieIn()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
