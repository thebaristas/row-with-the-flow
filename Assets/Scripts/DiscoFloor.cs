using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiscoFloor : MonoBehaviour{

    public float changeFrequencySeconds;
    public int gridHalfWidth;
    public int gridHalfHeight;
    public Tilemap tilemap;
    public Sprite tileSprite;
    public Color[] colours;

    void Start() {
        InitializeFloor();
        InvokeRepeating("RandomizeFloor", 0, changeFrequencySeconds);
    }

    void InitializeFloor() {
        for (int i = -gridHalfWidth; i < gridHalfWidth; ++i) {
            for (int j = -gridHalfHeight; j < gridHalfHeight; ++j) {
                Vector3Int position = new Vector3Int(i, j, 0);

                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = tileSprite;

                tilemap.SetTile(position, tile);
                tilemap.SetTileFlags(position, TileFlags.None);
            }
        }
    }

    void RandomizeFloor() {
        for (int i = -gridHalfWidth; i < gridHalfWidth; ++i) {
            for (int j = -gridHalfHeight; j < gridHalfHeight; ++j) {
                Vector3Int position = new Vector3Int(i, j, 0);
                Color colour = colours[Random.Range(0, colours.Length)];

                tilemap.SetColor(position, colour);
            }
        }
    }

}
