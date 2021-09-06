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
    public GameObject agentPrefab;
    public int numAgents;
    public GameObject[] daftarKota;
    public List<ModelKota> kotaList = new List<ModelKota>();
    [SerializeField] List<MyAgent> agents = new List<MyAgent>();
    [SerializeField] List<ModelAgent> agentsModel = new List<ModelAgent>();

    public List<List<float>> jarakAntarKota = new List<List<float>>();
    public List<List<float>> inversJarakAntarKota = new List<List<float>>();
    public List<List<float>> pheromoneGlobal = new List<List<float>>();
    private ANCOS ancos;
    private DataManagerScript dms = new DataManagerScript();

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
                koordinatKota = daftarKota[i].transform.position,
                rotationKota = daftarKota[i].transform.rotation,

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
        StartCoroutine(ExportToCSV("JarakAntarKota", jarakAntarKota));
        


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
        StartCoroutine(ExportToCSV("InversJarakAntarKota", inversJarakAntarKota));

        for (int i = 0; i < daftarKota.Length; i++)
        {
            List<float> phe = new List<float>();

            for (int j = 0; j < daftarKota.Length; j++)
            {
                phe.Add(0.00001f);
            }

            pheromoneGlobal.Add(phe);
        }
        StartCoroutine(ExportToCSV("PheromoneGlobalAwal", pheromoneGlobal));
        /*ancos = new ANCOS(jarakAntarKota, 
            Constanta.beta, 
            kotaList,
            inversJarakAntarKota,
            pheromoneGlobal,
            kotaTarget);*/

        for (int i = 0; i < numAgents; i++)
        {
            Instantiate(agentPrefab,kotaList[i].koordinatKota,kotaList[i].rotationKota);
        }

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
                if (agentsModel[i].Ancos.sumLnm < ANCOS.lgb)
                {
                    ANCOS.lgb = agentsModel[i].Ancos.sumLnm;
                    agentsModel[i].Ancos.sumLnm = 0;

                    ANCOS.tourTerpendek = agentsModel[i].Ancos.kotaVisited;
                    agentsModel[i].Ancos.kotaVisited = new List<string>();
                }
                
                Debug.Log("jarak Terpendek saat ini = " + ANCOS.lgb);
                string _msg = "";
                foreach (string item in ANCOS.tourTerpendek)
                {
                    _msg += item + " -> ";
                }
                StartCoroutine(Constanta.ExportToCSVS("LGB", "LGB Terpendek saat ini adalah: " + ANCOS.lgb + " " + _msg));
                Debug.Log(_msg);
 
                Debug.Log("----------------semut ke-" + i + " -----------------");
                debug(pheromoneGlobal);
                StartCoroutine(ExportToCSV("PheromoneGlobalTerakhir", pheromoneGlobal));
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

    IEnumerator ExportToCSV(string _namaFile, List<List<float>> _data)
    {
        // tentukan dulu alamat file

        string alamatFile =
            Application.dataPath.Replace("/Assets", "") + "/" + _namaFile + ".csv";

        // <optional> ngecek apakah file sudah ada
        /*        if (File.Exists(alamatFile))
                    File.Delete(alamatFile);
        */
        // objek stream
        var streamData = File.CreateText(alamatFile);

        #region Isiin DATA
        string data = string.Empty;
        data += _namaFile + System.Environment.NewLine;

        foreach (var items in _data)
        {
            foreach (var item in items)
            {
                data += item + " ,";
            }
            data += System.Environment.NewLine;
        }

        #endregion
        streamData.WriteLine(data);
        // tutup stream-nya
        streamData.Close();

        // coroutine
        yield return new WaitForSeconds(2.0f);

        // kalau mau dibuka langsung file
        //Application.OpenURL(alamatFile);
    }




}
