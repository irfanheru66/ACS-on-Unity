using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANCOS
{
    
    #region Attribut
    private int beta;
    private List<List<float>> jarakAntarKota = new List<List<float>>();
    // List dari kota-kota yang sudah disinggahi
    List<GameObject> kotaDisinggahi = new List<GameObject>();

    // List dari kota-kota yang masih BELUM disinggahi
    List<GameObject> kotaBelumDisinggahi = new List<GameObject>();
    #endregion

    #region Properties
    public int Beta { get { return this.beta; } set { this.beta = value; } }
    public List<List<float>> JarakAntarKota
    {
        set { this.jarakAntarKota = value; }
        
        private get { return this.jarakAntarKota; }
    }
    #endregion

    #region Function
    #region Private Function
    //private double TotalTemporary()
    //{

    //}
    #endregion

    #region Public Function
    public double CalculateTemporary(int _indexI, int _indexJ, List<List<float>> _phe, int _beta
        , List<List<float>> _inv)
    {
        return _phe[_indexI][_indexJ] * Mathf.Pow(_inv[_indexI][_indexJ], 2);
    }

    public double CalculateProbability(int _indI, int _indJ, List<List<float>> _phe, int _beta
        , List<List<float>> _inv)
    {
        return CalculateTemporary(_indI, _indJ, _phe, _beta, _inv);
    }

    public int GetNextKota()
    {
        return 0;
    }
    #endregion
    #endregion

    #region Constructor
    public ANCOS(List<List<float>> _jarakkota, int _beta)
    {
        this.jarakAntarKota = _jarakkota;
        this.beta = _beta;
    }
    #endregion
}
