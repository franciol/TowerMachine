using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cameraCntrl : MonoBehaviour
{
    public Text text;
    public Text endtext;

    public Camera cam;
    private bool torre1;
    private bool torre2;
    public GameObject Tower1;
    public GameObject Tower2;
    public GameObject Plane;
    public Button torre1B;
    public Button torre2B;
    public Button NextWave;
    public LayerMask IgnoreMe;
    private int playerLife = 100;

    // Start is called before the first frame update
    void Start()
    {
        endtext.enabled = false;
        cam.aspect = (16/9);
        torre1B.onClick.AddListener(tower1_click);
        torre2B.onClick.AddListener(tower2_click);
        NextWave.onClick.AddListener(nextWave);


    }

    // Update is called once per frame
    void Update()
    {
        if(playerLife<=0)
        {
            print("lost");
            StartCoroutine("endGame");
            endtext.enabled = true;
            endtext.text = "YOU LOST\nPoints: " + FindObjectOfType<ResetWave>().points + "\nWave: " + FindObjectOfType<ResetWave>().wave;
        }
        else if(FindObjectOfType<ResetWave>().endWave & FindObjectOfType<ResetWave>().wave >= 9)
        {
            endtext.enabled = true;
            StartCoroutine("endGame");
            endtext.text = "YOU WIN!!!\nPoints: " + FindObjectOfType<ResetWave>().points + "\nWave: " + FindObjectOfType<ResetWave>().wave;
        }
        int money = FindObjectOfType<ResetWave>().points;
        text.text = "Money: "+money.ToString()+"\nLife: "+playerLife+"/100\nWave: "+FindObjectOfType<ResetWave>().wave;
        NextWave.interactable = FindObjectOfType<ResetWave>().endWave;
        if (money >= 10)
        {
            torre1B.interactable = true;
        }
        else
        {
            torre1B.interactable = false;
            torre1 = false;
        }
        if (money >= 50)
        {
            torre2B.interactable = true;
        }
        else
        {
            torre2 = false;
            torre2B.interactable = false;
        }




        float x_in = Input.GetAxis("Horizontal");
        float y_in = Input.GetAxis("Vertical");
        transform.Translate(x_in, 0, y_in);
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("DummyScene");
        }
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= mouseScroll * 10;

        if (Input.GetMouseButton(0))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1000f,~IgnoreMe,QueryTriggerInteraction.Ignore))
            {
                Debug.DrawLine(transform.position, hit.point);
                if (torre1)
                {
                    FindObjectOfType<ResetWave>().points -= 10;
                    GameObject a = Instantiate(Tower1, hit.point, Quaternion.identity, Plane.transform);
                    a.transform.localScale = new Vector3(0.005f, 1f, 0.005f);
                    a.transform.position = new Vector3(a.transform.position.x, 0, a.transform.position.z);
                    
                    torre1 = false;
                }
                else if (torre2)
                {
                    FindObjectOfType<ResetWave>().points -= 50;
                    GameObject a = Instantiate(Tower2, hit.point, Quaternion.identity, Plane.transform);
                    a.transform.localScale = new Vector3(0.005f, 1f, 0.005f);
                    a.transform.position = new Vector3(a.transform.position.x, 0, a.transform.position.z);
                    
                    torre2 = false;
                }

            }
        }
    }

    void tower1_click()
    {
        torre1 = !torre1;
    }
    void tower2_click()
    {
        torre2 = !torre2;
    }
    void nextWave()
    {
        FindObjectOfType<ResetWave>().reset = true;
    }
    public void damageBase(int damage)
    {
        playerLife -= damage;
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
