using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAgent : MonoBehaviour
{
    [SerializeField] float kecepatan;
    [SerializeField] float rotSpeed;
    public Transform target;
    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

    }

    // Update is called once per frame
    void Update()
    {
        // arahkan rotasi
        Rotasi(target);

        // melakukan pererakan
        Bergerak();
    }

    void Bergerak()
    {
        myTransform.position += myTransform.forward * kecepatan * Time.deltaTime;
    }

    void Rotasi(Transform antTarget)
    {
        myTransform.rotation = Quaternion.Slerp(
            myTransform.rotation,
            Quaternion.LookRotation(antTarget.position - myTransform.position),
            rotSpeed * Time.deltaTime
            );
    }
}
