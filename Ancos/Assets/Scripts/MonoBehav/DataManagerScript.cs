using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerScript : MonoBehaviour
{
    SimulationManagerScript simScript;
    public List<List<float>> jarakAntarKota = new List<List<float>>();

    // Start is called before the first frame update
    void Start()
    {
        simScript = GetComponent<SimulationManagerScript>();

        GetAllJarak();

        for (int i = 0; i < jarakAntarKota.Count; i++)
        {
            Debug.Log("baris ke-" + (i + 1));
            for (int j = 0; j < jarakAntarKota[i].Count; j++)
            {
                Debug.Log("jarak dari " + (i + 1) + " - " + (j + 1) + " : " + jarakAntarKota[i][j]);
            }
            Debug.Log("----------------");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllJarak()
    {
        GameObject[] kota = simScript.daftarKota;

        for (int i = 0; i < kota.Length; i++)
        {
            List<float> _jarak = new List<float>();

            for (int j = 0; j < kota.Length; j++)
            {
                float jarak = Vector3.Distance(kota[i].transform.position, kota[j].transform.position);
                _jarak.Add(jarak);
            }

            jarakAntarKota.Add(_jarak);
        }
    }
}
