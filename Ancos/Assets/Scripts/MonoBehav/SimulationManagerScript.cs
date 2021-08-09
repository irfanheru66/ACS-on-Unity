using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// script ini menghandle urusan
/// - update next kota
/// - menentukan jarak dari agent ke kota
/// - menghimpun kota-kota
/// - menghimpun semut-semut
/// </summary>
public class SimulationManagerScript : MonoBehaviour
{
    public GameObject[] daftarKota;
    public List<ModelKota> kotaList = new List<ModelKota>();
    private int kotaTarget = 0;
    [SerializeField] List<MyAgent> agents = new List<MyAgent>();
    
    public List<List<float>> jarakAntarKota = new List<List<float>>();
    public List<List<float>> inversJarakAntarKota = new List<List<float>>();
    public List<List<float>> pheromoneGlobal = new List<List<float>>();
    private ANCOS ancos;

    // Start is called before the first frame update
    void Start()
    {
        // menghimpun kota-kota
        daftarKota = GameObject.FindGameObjectsWithTag("kota");

        // membuat daftar model kota
        foreach (GameObject kota in daftarKota)
        {
            ModelKota m = new ModelKota()
            {
                namaKota = kota.name,
                koordinatKota = kota.transform.position
            };
            kotaList.Add(m);
        }

        foreach (GameObject semut in GameObject.FindGameObjectsWithTag("agent"))
        {
            agents.Add(semut.GetComponent<MyAgent>());
        }

        UpdateNextKota();




        // menghitung jarak antar kota
        // Vector3.Distance(a, b);
        for (int i = 0; i < daftarKota.Length; i++)
        {
            List<float> jarak = new List<float>();

            for (int j = 0; j < daftarKota.Length; j++)
            {
                float _jarak = Vector3.Distance(
                    daftarKota[i].transform.position,
                    daftarKota[j].transform.position);

                jarak.Add(_jarak);
            }
            
            jarakAntarKota.Add(jarak);
        }

        // hitung invers
        for (int i = 0; i < daftarKota.Length; i++)
        {
            List<float> _invers = new List<float>();

            for (int j = 0; j < daftarKota.Length; j++)
            {
                _invers.Add(1 / jarakAntarKota[i][j]);
            }

            inversJarakAntarKota.Add(_invers);
        }

        for (int i = 0; i < daftarKota.Length; i++)
        {
            List<float> phe = new List<float>();

            for (int j = 0; j < daftarKota.Length; j++)
            {
                phe.Add(0.00001f);
            }

            pheromoneGlobal.Add(phe);
        }

        ancos = new ANCOS(jarakAntarKota, Constanta.beta);
    }
    
    private void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (JarakAgentKeKota(agents[i].transform, daftarKota[kotaTarget].transform))
            {
                UpdateNextKota();
            }
        }
    }

    void UpdateNextKota()
    {
        kotaTarget++;
        kotaTarget %= kotaList.Count;

        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].GetComponent<MyAgent>().target = 
                daftarKota[kotaTarget].transform;
        }
    }

    bool JarakAgentKeKota(Transform _agent, Transform _target)
    {
        return Vector3.Distance(_agent.position, _target.position) < 3;
    }

    
}
