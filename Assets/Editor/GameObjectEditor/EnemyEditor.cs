using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

[CanEditMultipleObjects, CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SetPostion"))
        {
            Tilemap tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
            var allPos = tilemap.cellBounds.allPositionsWithin;
            int min_x = 0;
            int min_y = 0;
            if (allPos.MoveNext())
            {
                Vector3Int current = allPos.Current;
                min_x = current.x;
                min_y = current.y;
            }
            Enemy enemy = target as Enemy;
            Vector3Int cellPos = tilemap.WorldToCell(enemy.transform.position);
            enemy.RowIndex = (int)MathF.Abs(min_y - cellPos.y);
            enemy.ColIndex = (int)MathF.Abs(min_x - cellPos.x);
            enemy.transform.position = tilemap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, -1);
        }
    }
}
