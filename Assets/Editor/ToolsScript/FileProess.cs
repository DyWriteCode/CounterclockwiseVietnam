using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "FileProess", menuName = "ScriptableObject/FileProess", order = 0)]
public class FileProess : ScriptableObject
{
    [LabelText("ExcelPath")] 
    public string ExcelPath = $"{Application.dataPath}/_Excel";
    [LabelText("CSVPath")] 
    public string CSVPath = $"{Application.dataPath}/Resources/Data";

    [Button("ExcelToCSV", ButtonSizes.Large, Style = ButtonStyle.Box)]
    public void GetTotalScore()
    {
        ExportExcelToTxt(ExcelPath, CSVPath);
        Debug.Log($"ExcelPath : {ExcelPath}");
        Debug.Log($"CSVPath : {CSVPath}");
    }

    public static void ExportExcelToTxt(string excelPath, string csvPath)
    {
        if (!Directory.Exists(excelPath))
        {
            Directory.CreateDirectory(excelPath);
            return;
        }
        string[] files = Directory.GetFiles(excelPath, "*.xlsx");
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
                readTableToTxt(files[i], csvPath, table);
            }
        }
        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath, string outPathStr, DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string path = filePath + "/" + fileName + ".txt";
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
