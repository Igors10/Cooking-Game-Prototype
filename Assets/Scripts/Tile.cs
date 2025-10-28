using UnityEngine;

public enum Rarity
{
    BASIC,
    UNCOMMON,
    RARE,
    UNIQUE
}

public enum Biome
{
    GRASSLANDS,
    WETLANDS,
    COASTLANDS,
    UNDERLANDS,
    FORAGELANDS
}

public class Tile : MonoBehaviour
{
    [Header("Tile properties")] // properties of each individual tiles
    public bool[] open_sides;
    public Rarity rarity;
    public Biome biome;
    public string t_name;
    public string t_description;

    [Header("Tile components")] // Tile gameobject components
    public GameObject[] sides;
    [SerializeField] GameObject[] borders;
    [SerializeField] GameObject border_obj;

    [Header("Object refs")] // references to other gameObjects
    public Tile[] connected_tiles = new Tile[6];
    TileManager tile_manager;

    private void Start()
    {
        tile_manager = GameManager.instance.tile_manager;
        tile_manager.tiles.Add(this);

        // After initializing draft choices are iniciated right away
        tile_manager.ActivateDraftChoices(this);
    }


}
