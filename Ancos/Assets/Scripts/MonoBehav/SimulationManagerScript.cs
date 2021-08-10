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
    [SerializeField] List<MyAgent> agents = new List<MyAgent>();
    [SerializeField] List<ModelAgent> agentsModel = new List<ModelAgent>();

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
        for (int i = 0; i < daftarKota.Length; i++)
        {
            ModelKota m = new ModelKota()
            {
                indexKota = i ,
                namaKota = daftarKota[i].name,
                transformKota = daftarKota[i].transform,
                koordinatKota = daftarKota[i].transform.position

            };
            kotaList.Add(m);
        }

        /* foreach (GameObject semut in GameObject.FindGameObjectsWithTag("agent"))
         {
             agents.Add(semut.GetComponent<MyAgent>());
         }*/





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
        debug(pheromoneGlobal);
        /*ancos = new ANCOS(jarakAntarKota, 
            Constanta.beta, 
            kotaList,
            inversJarakAntarKota,
            pheromoneGlobal,
            kotaTarget);*/
        GameObject[] semut = GameObject.FindGameObjectsWithTag("agent");
        for (int i = 0; i < semut.Length; i++)
        {
            agents.Add(semut[i].GetComponent<MyAgent>());
            ancos = new ANCOS(jarakAntarKota,
            Constanta.beta,
            kotaList,
            inversJarakAntarKota,
            pheromoneGlobal,
            i);
            agentsModel.Add(new ModelAgent(i, ancos,i));
            UpdateNextKota(i,i);
        }

        
    }
    
    private void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (agentsModel[i].Ancos.kotanotvisited.Count == 0)
            {
                agentsModel[i].Ancos.kotanotvisited = kotaList;
                pheromoneGlobal = matAdd(pheromoneGlobal, agentsModel[i].Ancos.pheLoc);
                agentsModel[i].Ancos.pheGlo = pheromoneGlobal;
                agentsModel[i].Ancos.pheLoc = pheromoneGlobal;
                Debug.Log("----------------semut ke-" + i + " -----------------");
                debug(pheromoneGlobal);
            }
            else if (JarakAgentKeKota(agents[i].transform.position, kotaList[agentsModel[i].kotaNow].koordinatKota))
            {
                nextKota(i);
                UpdateNextKota(i, agentsModel[i].kotaNow);
            }
        }
    }

    private List<List<float>> 
        matAdd(List<List<float>> arr1, List<List<float>> arr2) 
    {
        List<List<float>> res = 
            new List<List<float>>();

        for (int i = 0; i < arr1.Count; i++)
        {
            List<float> add = new List<float>();
            for (int j = 0; j < arr1.Count; j++)
            {
                if (arr1[i][j] == arr2[i][j])
                {
                    add.Add(arr1[i][j]);
                }
                else
                {
                    add.Add(arr1[i][j] + arr2[i][j]);
                }
            }
            res.Add(add);
        }
        return res;
    }

    void debug(List<List<float>> arr)
    {
        foreach (var items in arr)
        {
            string msg = "";
            foreach (var item in items)
            {
                msg += item + " | ";
            }
            Debug.Log(msg);

        }
    }
    void nextKota(int index)
    {
        agentsModel[index].kotaNow = agentsModel[index].Ancos.nextCity(agentsModel[index].kotaNow);
    }

    void UpdateNextKota(int i,int kotaTarget)
    {
        Debug.Log("semut-"+i + " pergi ke kota-" + kotaTarget);
         agents[i].GetComponent<MyAgent>().target =
                    kotaList[kotaTarget].transformKota;
         
    }

    bool JarakAgentKeKota(Vector3 _agent, Vector3 _target)
    {
        return Vector3.Distance(_agent, _target) < 3;
    }

    
}
