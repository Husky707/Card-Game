using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class FieldBuilder : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] int Columns = 5;
    [SerializeField] int Rows = 3;
    [SerializeField] float TileWidth = 2.5f;
    [SerializeField] float TileHeight = 3f;
    [SerializeField] float VerticalMagins = .2f;
    [SerializeField] float HorizontalMagins = .2f;
    int totalColumns => Columns*2;
    int totalRows => Rows;//* 2;

    [Header("Hooks")]
    [SerializeField] GameObject fieldTile = null;
    [SerializeField] Transform fieldCenter = null;
    private Vector3 fieldOrigin;

    GameObject[] fieldTiles;

    private void Awake()
    {
        if (fieldCenter == null)
            fieldCenter = transform;
    }


    public void GenerateField()
    {
        fieldTiles = new GameObject[(totalColumns * totalRows)];
        GetTopLeftOrigin(fieldCenter);


        for(int yy = 0; yy < totalRows; yy++)
        {
            for(int xx = 0; xx < totalColumns; xx++)
            {
                //Get next tile location
                Vector3 nextPosition = GetTileLocation(xx, yy);
                //Create new Tile
                fieldTiles[(yy * totalColumns) + xx] = Instantiate(fieldTile, nextPosition, Quaternion.identity, fieldCenter);
                //Add tile to array
            }
        }
    }

    private Vector3 GetTileLocation(int xi, int yi)
    {
        float xx = (xi * TileWidth) + (xi * 2 * HorizontalMagins);
        float yy = (yi * TileHeight) + (yi * 2 * VerticalMagins);
        Vector3 posVector = new Vector3(xx, 0f, yy);

        return posVector + fieldOrigin;
    }

    private void GetTopLeftOrigin(Transform fieldCenter)
    {
        int halfrows = (int)Mathf.Floor(totalRows / 2);
        float yorigin = -((VerticalMagins * 2) * halfrows - (TileHeight/2.0f)) - (halfrows * TileHeight) - (TileHeight/2.0f);
        int halfColumns = (int)Mathf.Floor(totalColumns / 2);
        float xorigin = -((HorizontalMagins * 2 * halfColumns)) - (TileWidth * halfColumns) + (TileWidth/2.0f) + HorizontalMagins;

        fieldOrigin = new Vector3(xorigin, 0f, yorigin);
    }

    public void Rebuild()
    {
        ClearField();
        GenerateField();
    }

    public void ClearField()
    {
        if (fieldTiles == null || fieldTiles.Length <= 0)
            return;

        foreach (GameObject obj in fieldTiles)
        {
            if (Application.isEditor)
                DestroyImmediate(obj);
            else
                Destroy(obj);
        }

        fieldTiles = new GameObject[totalColumns * totalRows];
    }
}
