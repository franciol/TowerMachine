using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveArray
{
    [SerializeField]
    private int e1,e2;

    public int get1()
    {
        return e1;
    }
    public int get2()
    {
        return 2;
    }
    public void set1(int s1)
    {
        e1 = s1;
    }
    public void set2(int s1)
    {
        e2 = s1;
    }

}



public class ResetWave : MonoBehaviour
{
    public int points = 100;

    public List<SpawnNew> spawners;

    [SerializeField]
    private List<WaveArray> dd;

    public bool reset = true;
    public bool endWave = false;
    public int wave = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool tmp = true;
        if(reset)
        {

            reset = false;
            foreach(SpawnNew sp in spawners)
            {
                sp.newWave(dd[wave].get1(),dd[wave].get2());
                
            }

            wave += 1;
        }
        foreach (SpawnNew sp in spawners)
        {
            tmp = sp.end_wave() & tmp;
        }
        endWave = tmp;
    }
    public void add_points(int point)
    {
        points += point;
    }

}
