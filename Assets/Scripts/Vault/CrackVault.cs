using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackVault : MonoBehaviour
{
    public GameObject loadingPrefab;
    public float loadSpeed = .1f;
    public float safeHeight = 1f;
    public float openSpeed = .05f;
    private Image unloaded;
    private Image loaded;
    private bool isCracking = false;
    private string status = "closed";
    private float loading = 0f;

    private Quaternion openRotation;

    private float finalRotation;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        openRotation = new Quaternion(transform.rotation.x,
                                      transform.rotation.y - Mathf.PI/2,
                                      transform.rotation.z,
                                      transform.rotation.w);
        
        finalRotation = transform.rotation.y - 90f;
    }

    // Update is called once per frame
    void Update()
    {
        //tracks position of UI element
        if(unloaded) {unloaded.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, safeHeight, 0));}

        switch(status)
        {
            case "closed":
            {
                Counter();
                break;
            }
            case "opening":
            {
                Open();
                break;
            }
            case "opened":
            {
                break;
            }
            default:
            {
                status = "closed";
                break;
            }
        }
    }

    private void Counter()
    {
        if(isCracking)
            {
                if(loading < 1)
                {
                    loading = loading + (loadSpeed * Time.deltaTime);
                    loaded.fillAmount = loading;
                }
                else
                {
                    loaded.fillAmount = 1;
                    status = "opening";
                    print("opened");
                }
            }
    }

    private void Open()
    {
        if(transform.position.y  > -2.1)
        {
            transform.Translate(0, -openSpeed * Time.deltaTime, 0);
        }
        else if(unloaded)
        {
            Destroy(unloaded.gameObject);
            status = "opened";
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && unloaded == null)
        {
            isCracking = true;
            navMeshAgent = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            unloaded = Instantiate(loadingPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            loaded = new List<Image>(unloaded.GetComponentsInChildren<Image>()).Find(img => img != unloaded);
        }
        else if(other.gameObject.tag == "Player")
        {
            navMeshAgent = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            isCracking = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        /*
            if other is player
                make navMeshAgent null
                make images null
                destroy timer
        */

        navMeshAgent = null;
        isCracking = false;
    }
}
