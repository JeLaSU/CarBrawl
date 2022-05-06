using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassActivew : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ground;


    private void Awake()
    {
        ground.transform.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
