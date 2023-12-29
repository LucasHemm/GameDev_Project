using System;
using UnityEngine;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
    void ClearGrid()
    {
        
        for (int i = transform.childCount; i >= transform.childCount; i--)
        {
            if (transform.childCount == 0)
                break;

            int c = Mathf.Clamp(i - 1, 0, transform.childCount);
            DestroyImmediate(transform.GetChild(c).gameObject);
        }
    }

    Vector2 DetermineTileSize(Bounds tileBounds)
    {
        return new Vector2((tileBounds.extents.x * 2) * 0.75f, (tileBounds.extents.z * 2));
    }

    public void GenerateGrid(GameObject tile, Vector2Int gridsize)
    {
        ClearGrid();
        Vector2 tileSize = DetermineTileSize(tile.GetComponent<MeshFilter>().sharedMesh.bounds);
        Vector3 position = transform.position;

        for (int x = 0; x < gridsize.x; x++)
        {
            for (int y = 0; y < gridsize.y; y++)
            {
                position.x = transform.position.x + tileSize.x * x;
                position.z = transform.position.z + tileSize.y * y;

                position.z += OffsetUnevenRow(x, tileSize.y);

                CreateTile(tile, position, new Vector2Int(x, y));
            }
        }
    }

    public void AutoGenerateGrid(GameObject tile, Vector2Int gridSize)
{
    int numberOfTiles = gridSize.x * gridSize.y;
    int tilesToBeDeleted = (int) Math.Floor(numberOfTiles * 0.07f);
    if (numberOfTiles < 70)
    {
        tilesToBeDeleted = (int) Math.Floor(numberOfTiles * 0.04f);
    }
    
    List<Vector2Int> tiles = new List<Vector2Int>();

    for (int i = 0; i < tilesToBeDeleted; i++)
    {
        Vector2Int randomTile = new Vector2Int(UnityEngine.Random.Range(0, gridSize.x), UnityEngine.Random.Range(0, gridSize.y));

        if (!tiles.Contains(randomTile))
        {
            tiles.Add(randomTile);
        }
        else
        {
            i--;
        }
    }

    ClearGrid();
    Vector2 tileSize = DetermineTileSize(tile.GetComponent<MeshFilter>().sharedMesh.bounds);
    Vector3 position = transform.position;

    for (int x = 0; x < gridSize.x; x++)
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            Vector2Int currentTile = new Vector2Int(x, y);

            if (tiles.Contains(currentTile))
            {
                continue;
            }

            position.x = transform.position.x + tileSize.x * x;
            position.z = transform.position.z + tileSize.y * y;
            position.z += OffsetUnevenRow(x, tileSize.y);

            CreateTile(tile, position, currentTile);
        }
    }
}


    float OffsetUnevenRow(float x, float y)
    {
        return x % 2 == 0 ? y / 2 : 0f;
    }

    void CreateTile(GameObject t, Vector3 pos, Vector2Int id)
    {
        GameObject newTile = Instantiate(t.gameObject, pos, Quaternion.identity, transform);
        newTile.name = "Tile " + id;
        

    }

}

