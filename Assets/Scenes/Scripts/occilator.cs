using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class occilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector; 
    
    [Range(-1,1)]
    [SerializeField] float movementFactor;

    [SerializeField] float movementFactorDis = 0.01f;

    [SerializeField] float period = 2f;

    


    Vector3 statingPos;

    // Start is called before the first frame update
    void Start()
    {
        statingPos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float cycle = Time.time/period;//grows continuously with time
        const float tau = Mathf.PI*2f;// about 6.28
        movementFactor = Mathf.Sin(cycle * tau);

        Vector3 Offset = movementVector * movementFactor; 

        transform.position = statingPos + Offset;


    } 
}
