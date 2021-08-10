using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Constanta
{
    public static int beta = 2;
    public static float q0 = 0.9f;
    public static float P
    {
        get { return 1 - q0; }
    }

    public static IEnumerator ExportToCSVS(string _namaFile, string _data)
    {
        // tentukan dulu alamat file
        string alamatFile =
            Application.dataPath.Replace("/Assets", "") + "/" + _namaFile + ".csv";

        // <optional> ngecek apakah file sudah ada
        if (File.Exists(alamatFile))
            File.Delete(alamatFile);

        // objek stream
        var streamData = File.CreateText(alamatFile);

        #region Isiin DATA
        // isiin datanya
        //string data = string.Empty;
        //data += "JARAK ANTAR KOTA" + System.Environment.NewLine;

        //for (int i = 0; i < jarakAntarKota.Count; i++)
        //{
        //    for (int j = 0; j < jarakAntarKota.Count; j++)
        //    {
        //        data += jarakAntarKota[i][j] + ", ";
        //    }

        //    data += System.Environment.NewLine;
        //}

        //data += System.Environment.NewLine;
        //data += System.Environment.NewLine;

        //data += "INVERS JARAK ANTAR KOTA" + System.Environment.NewLine;

        //for (int i = 0; i < jarakAntarKota.Count; i++)
        //{
        //    for (int j = 0; j < jarakAntarKota.Count; j++)
        //    {
        //        data += inversJarakAntarKota[i][j] + ", ";
        //    }

        //    data += System.Environment.NewLine;
        //}

        //data += System.Environment.NewLine;
        //data += System.Environment.NewLine;

        //data += "PHEROMONE ANTAR KOTA" + System.Environment.NewLine;

        //for (int i = 0; i < jarakAntarKota.Count; i++)
        //{
        //    for (int j = 0; j < jarakAntarKota.Count; j++)
        //    {
        //        data += pheromoneGlobal[i][j] + ", ";
        //    }

        //    data += System.Environment.NewLine;
        //}



        #endregion

        // cetak data ke stream
        streamData.WriteLine(_data);

        // tutup stream-nya
        streamData.Close();

        // coroutine
        yield return new WaitForSeconds(2.0f);

        // kalau mau dibuka langsung file
        //Application.OpenURL(alamatFile);
    }

}
