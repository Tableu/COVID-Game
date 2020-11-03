using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathAlgorithm : MonoBehaviour
{
    public Tilemap tileMap = null;
    public List<String> validTiles;
    public List<Vector3Int> availableTiles;
    public GameObject enemyPrefab;
    private Vector3Int[,] tileGrid;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        availableTiles = new List<Vector3Int>();
        //Sets up colliders for buildings
        foreach (var position in tileMap.cellBounds.allPositionsWithin) {
            if (!tileMap.HasTile(position)) {
                continue;
            }
            if (validTiles.Contains((tileMap.GetSprite(position).name))){
                availableTiles.Add(position);
                tileMap.SetColliderType(position,Tile.ColliderType.None);
            }else{
                tileMap.SetColliderType(position,Tile.ColliderType.Grid);
            }
            
        }
        var tilePath = FindPath(new List<Vector3Int>(),availableTiles[1],availableTiles[2]);
        var enemy = Instantiate(enemyPrefab, tileMap.CellToWorld(tilePath[0]), Quaternion.identity);
        var worldPath = new List<Vector3>();
        foreach (var tile in tilePath){
            worldPath.Add(tileMap.CellToWorld(tile));
        }
        enemy.GetComponent<EnemyPathing>().path = worldPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Basic algorithm that returns a path between two points. Returns null if no path is found.
    private List<Vector3Int> FindPath(List<Vector3Int> path,Vector3Int start, Vector3Int end)
    {
        path.Add(start);
        if (start.Equals(end))
            return path;
        var moves = GetMoves(start);
        foreach (var move in moves)
        {
            if (path.Contains(move))
                continue;
            var result = FindPath(path, move, end);
            if (result != null)
                return result;
        }
        return null;
    }
    //Returns a list of neighboring tiles to the indicated position.
    private List<Vector3Int> GetMoves(Vector3Int position){
        var moves = new List<Vector3Int>(){
            new Vector3Int(position.x-1,position.y,position.z),new Vector3Int(position.x,position.y+1,position.z),
            new Vector3Int(position.x,position.y-1,position.z),new Vector3Int(position.x+1,position.y,position.z)
        };
        var validMoves = new List<Vector3Int>();
        foreach (var move in moves){
            if(tileMap.HasTile(move)){
                if(validTiles.Contains((tileMap.GetSprite(move).name)))
                    validMoves.Add(move);
            }
        }
        return validMoves;
    }
}
