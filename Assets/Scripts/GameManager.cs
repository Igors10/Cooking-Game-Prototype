using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TileManager tile_manager;
    public Tile current_tile; // WIP thing, needs to be assigned

    private void Awake()
    {
        instance = this;
    }
}
