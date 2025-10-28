using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TileManager tile_manager;

    private void Awake()
    {
        instance = this;
    }
}
