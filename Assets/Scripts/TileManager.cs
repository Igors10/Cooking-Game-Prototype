using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // List of all the tiles currently on the board
    [HideInInspector] public List<Tile> tiles = new List<Tile>(); 
    
    // Draft markers are UI elements that appear when player is offered to explore a new tile
    [HideInInspector] public List<GameObject> draft_markers = new List<GameObject>();

    [SerializeField] GameObject marker_prefab;
    [SerializeField] DraftingWindow draft;
    Vector3 next_tile_position;
    Quaternion next_tile_rotation;

    private void Awake()
    {
        InstantiateMarkers(); // spawning draft markers
    }

    void InstantiateMarkers()
    {
        for (int a = 0; a < 6; a++)
        {
            GameObject new_marker_prefab = Instantiate(marker_prefab, transform.position, Quaternion.identity, transform);
            draft_markers.Add(new_marker_prefab);
            new_marker_prefab.SetActive(false);
        }
    }

    float FindTileDistance(Tile tile)
    {
        float tile_width = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        float tile_distance = tile_width * Mathf.Cos(Mathf.PI / 6);

        return tile_distance;
    }

    public void ActivateDraftMarkers(Tile tile)
    {
        for (int i = 0; i < 6; i++)
        {
            if (tile.paths[i] == Path.BLOCKED) continue; // there is no pathway
            if (tile.connected_tiles[i] != null) continue; // there is another tile already

            // Spawning the marker near the correct tile
            draft_markers[i].SetActive(true);
            draft_markers[i].transform.position = tile.transform.position;

            // Setting markers rotation
            float marker_rotation = 180.0f - 60.0f * i;
            draft_markers[i].transform.rotation = Quaternion.Euler(0, 0, marker_rotation);

            // Positionning the marker correctly
            draft_markers[i].transform.position += draft_markers[i].transform.up * FindTileDistance(tile);
        }
    }

    public void NewDraft(Vector3 tile_position, Quaternion tile_rotation)
    {
        next_tile_position = tile_position;
        next_tile_rotation = tile_rotation;

        draft.gameObject.SetActive(true);
        draft.ActivateDraft(FetchTileOptions(GameManager.instance.current_tile), tile_position);  //<< Finished here

        DeactivateMarkers();
    }

    Tile[] FetchTileOptions(Tile tile_from)
    {
        // make logic for the game to know which tiles to ask for
        TileList all_tile_list= GetComponent<TileList>();
        Tile[] tiles_to_return = new Tile[3];

        tiles_to_return[0] = all_tile_list.GetDraftTile(tile_from.biome, GetRandomRarity());
        tiles_to_return[1] = all_tile_list.GetDraftTile(tile_from.biome, GetRandomRarity());
        tiles_to_return[2] = all_tile_list.GetDraftTile(GetRandomBiome(), GetRandomRarity());

        return tiles_to_return;
    }

    Rarity GetRandomRarity()
    {
        float random_n = Random.value;

        if (random_n < GameManager.instance.rare_tile_chance) return Rarity.RARE;
        else if (random_n < GameManager.instance.uncommon_tile_chance) return Rarity.UNCOMMON;

        return Rarity.BASIC;
    }

    Biome GetRandomBiome()
    {
        int random_biome_id = Random.Range(0, System.Enum.GetValues(typeof(Biome)).Length);

        return (Biome)random_biome_id;
    }

    public void ChooseTile(Tile tile)
    {
        Tile new_tile = Instantiate(tile, next_tile_position, next_tile_rotation);
    }
    public void DeactivateMarkers()
    {
        foreach (GameObject marker in draft_markers)
        {
            marker.GetComponent<DraftMarker>().Deactivate();
        }
    }
}
