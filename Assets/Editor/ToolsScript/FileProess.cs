using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;
using UnityEngine.UI;
public static class MyEditor
{
    [MenuItem("Tools/ExcelToCSV")]
    public static void ExportExcelToTxt()
    {
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
        }

        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath, string outPathStr, DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        string path = Application.dataPath + "/" + outPathStr + "/" + fileName + ".txt";
        Debug.Log($"TxtPath : {path}");

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
                        string val = dataRow[col].ToString();

                        if (col != table.Columns.Count - 1)
                        {
                            str = str + val + ",";
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
