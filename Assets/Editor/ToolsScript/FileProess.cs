using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;

/// <summary>
/// 把Excel文件转成TXT格式文件
/// </summary>
public static class FileProess
{
    /// <summary>
    /// 运行Excel转换
    /// </summary>
    [MenuItem("Tools/ExcelToCSV")]
    public static void ExportExcelToTxt()
    {
        GameApp.Instance.Init();
        string gamePath = Application.dataPath;
        string excelPath = gamePath + "/_Excel";
        Debug.Log($"ExcelPath : {excelPath}");
        Debug.Log($"GamePath : {gamePath}");
        Debug.Log("------------------------------------------------------------");

        if (!Directory.Exists(excelPath))
        {
            Directory.CreateDirectory(excelPath);
            return;
        }

        string[] files = Directory.GetFiles(excelPath, "*.xlsx");
        foreach (string file in files)
        {
            Debug.Log($"ExcelFile : {file}");
        }
        Debug.Log("------------------------------------------------------------");

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');

            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                DataSet dataSet = excelDataReader.AsDataSet();

                if (dataSet == null)
                {
                    continue;
                }

                DataTable table = dataSet.Tables[0];

                readTableToTxt(files[i], "Resources/Data", table);
            }
            EditorUtility.DisplayProgressBar("Clear", $"Name : {files[i]}", i * 1.0f / files.Length);
        }

        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 读取数据到TXT里面
    /// </summary>
    /// <param name="filePath">需要读取的文件路径</param>
    /// <param name="outPathStr">需要输出的文件路径</param>
    /// <param name="table">存放在哪个table</param>
    private static void readTableToTxt(string filePath, string outPathStr, DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        string path = Application.dataPath + "/" + outPathStr + "/" + fileName + ".bytes";
        Debug.Log($"BytesPath : {path}");

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = GameApp.ArchiveManager.DataToArchiveNormal(PlayerPrefs.GetString("AESKEY"), dataRow[col].ToString());

                        if (col != table.Columns.Count - 1)
                        {
                            str = str + val + "<,>";
                        }
                        else
                        {
                            str = str + val;
                        }
                    }

                    sw.Write(str);

                    if (row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
