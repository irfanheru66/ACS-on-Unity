using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManagerScript
{
    // Update is called once per frame
    public IEnumerator ExportToCSV(string _namaFile, List<List<float>> _data)
    {
        // tentukan dulu alamat file
        Debug.Log("yes");

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
                Debug.Log(item);
                data += item + ",";
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
