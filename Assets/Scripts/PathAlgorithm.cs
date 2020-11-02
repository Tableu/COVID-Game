using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathAlgorithm : MonoBehaviour
{
    public Tilemap tileMap = null;
    public List<String> validTiles;
    public List<Vector3Int> availableTiles;

    private Vector3Int[,] tileGrid;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        availableTiles = new List<Vector3Int>();
        tileGrid = new Vector3Int[tileMap.cellBounds.size.y,tileMap.cellBounds.size.x];
        int index = 0;
        int rowSize = tileMap.cellBounds.size.x;
        foreach (var position in tileMap.cellBounds.allPositionsWithin) {
            if (!tileMap.HasTile(position)) {
                continue;
            }
            
            if (validTiles.Contains((tileMap.GetSprite(position).name))){
                availableTiles.Add(position);
                tileGrid[index/rowSize, index%rowSize] = position;
                tileMap.SetColliderType(position,Tile.ColliderType.None);
            }else{
                tileMap.SetColliderType(position,Tile.ColliderType.Grid);
            }

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
