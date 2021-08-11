using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANCOS
{

    #region Attribut
    public static float lgb = Mathf.Infinity;
    public static List<string> tourTerpendek;
    private int _beta;
    public int kotaNow;
    private List<List<float>> jarakAntarKota = new List<List<float>>();
    private List<ModelKota> kotaNotVisited = new List<ModelKota>();
    private List<List<float>> invers = new List<List<float>>();
    private List<List<float>> phe;
    private List<List<float>> _phe;
    public float sumLnm = 0;
    private int C;
    public List<string> kotaVisited = new List<string>();
    #endregion

    #region Properties
    public int Beta { get { return this._beta; } set { this._beta = value; } }
    
    public List<List<float>> JarakAntarKota
    {
        set { this.jarakAntarKota = value; }
        
        private get { return this.jarakAntarKota; }
    }
    public List<ModelKota> kotanotvisited
    {
        set { kotaNotVisited = value.GetRange(0, value.Count); }
        get { return kotaNotVisited; }
    }

    public List<List<float>> pheLoc
    {
        set { _phe = cloning(value); }
        get { return this._phe; }
    }
    public List<List<float>> pheGlo
    {
        set { phe = cloning(value);  }
        get { return this.phe; }
    }
    #endregion

    #region Function
    #region Private Function
    //private double TotalTemporary()
    //{

    //}
    private float getTemp(float phe, float invers)
    {
        return phe * Mathf.Pow(invers, Beta);
    }

    private float getProb(float temp, float sumTemp)
    {
        return temp / sumTemp;
    }

    private float sum(List<float> arr)
    {
        float sums = 0;

        foreach (var item in arr)
        {
            sums += item;
        }
        return sums;
    }

    private float deltaPhe(float lnm, int c)
    {
        return 1 / (lnm * c);
    }

    private float newPhe(float _phe,float lnm, int c) 
    {
        return (1 - Constanta.P) * _phe + Constanta.P * deltaPhe(lnm, c); 
    }

    #endregion





    #region Public Function

    public int nextCity(int _kotanow)
    {


        for (int i = 0; i < kotaNotVisited.Count; i++)
        {
            if (_kotanow == kotaNotVisited[i].indexKota)
            {
                string namaKota = kotaNotVisited[i].namaKota;
                /*Debug.Log(namaKota);*/
                kotaVisited.Add(namaKota);
                kotaNotVisited.RemoveAt(i);
                break;
            }
        }

        int kotaNow = _kotanow;
        List<int> _index = new List<int>();
        List<float> _temps = new List<float>();
        List<float> _prob = new List<float>();
        float q = Random.Range(0.0f, 1.0f);
        int iKota = 0;
        Debug.Log(q);

        foreach (var item in kotaNotVisited)
        {
            _index.Add(item.indexKota);
            _temps.Add(getTemp(phe[kotaNow][item.indexKota], invers[kotaNow][item.indexKota]));
            /*Debug.Log(getTemp(phe[kotaNow][item.indexKota], invers[kotaNow][item.indexKota]));*/
        }

/*        Debug.Log(_temps.IndexOf(Mathf.Max(_temps.ToArray())));
        Debug.Log(_index[_temps.IndexOf(Mathf.Max(_temps.ToArray()))]);*/
        
        
        if (q <= Constanta.q0)
        {
            /*Debug.Log(_index[_temps.IndexOf(Mathf.Max(_temps.ToArray()))]);*/
            iKota = _index[_temps.IndexOf(Mathf.Max(_temps.ToArray()))];
        }
        else
        {
            float sumTemp = sum(_temps);
            foreach (var item in _temps)
            {
                _prob.Add(getProb(item, sumTemp));
                Debug.Log(getProb(item, sumTemp));
            }
            /*Debug.Log(_prob.IndexOf(Mathf.Max(_prob.ToArray())));*/
            iKota = _index[_prob.IndexOf(Mathf.Max(_prob.ToArray()))];
        }

        float _newPhe = newPhe(phe[kotaNow][iKota], JarakAntarKota[kotaNow][iKota], C);
        _phe[kotaNow][iKota] = phe[kotaNow][iKota] + _newPhe;
        _phe[iKota][kotaNow] = phe[iKota][kotaNow] + _newPhe;

        /*        foreach (var items in _phe)
                {
                    string msg = "";
                    foreach (var item in items)
                    {
                        msg += item + " | ";
                    }
                    Debug.Log(msg);
                }*/
        sumLnm += JarakAntarKota[kotaNow][iKota];
        Debug.Log(iKota);
        return iKota;
    }

    private List<List<float>> cloning(List<List<float>> arr1) {
        List<List<float>> clone = new List<List<float>>();
        foreach (var items in arr1)
        {
            List<float> floats = new List<float>();
            foreach (var item in items)
            {
                floats.Add(item);
            }
            clone.Add(floats);
        }
        return clone;

    }
    #endregion
    #endregion

    #region Constructor

    public ANCOS(List<List<float>> jarakAntarKota, 
        int beta, 
        List<ModelKota> kotaList, 
        List<List<float>> inversJarakAntarKota, 
        List<List<float>> pheromoneGlobal, 
        int kotaTarget)
    {
        this.jarakAntarKota = jarakAntarKota;
        Beta = beta;
        this.C = kotaList.Count;
        this.kotaNotVisited = kotaList.GetRange(0,kotaList.Count);
        this.invers = inversJarakAntarKota;
        this.phe = cloning(pheromoneGlobal);
        this._phe = cloning(pheromoneGlobal);
        this.kotaNow = kotaTarget;
        Debug.Log(kotaNow);
    }

    #endregion
}
