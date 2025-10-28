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

    private void Start()
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

    public void ActivateDraftChoices(Tile tile)
    {
        for (int i = 0; i < 6; i++)
        {
            if (tile.open_sides[i] == false) continue; // there is no pathway
            if (tile.connected_tiles[i] != null) continue; // there is another tile already

            // Spawning the marker near the correct tile
            draft_markers[i].SetActive(true);
            draft_markers[i].transform.position = tile.transform.position;

            // Setting markers rotation
            float marker_rotation = 180.0f - 60.0f * i;
            draft_markers[i].transform.rotation = Quaternion.Euler(0, 0, marker_rotation);

            // Positionning the marker correctly
            //transform.Translate(Vector3.up * FindTileDistance(tile), Space.Self);
            draft_markers[i].transform.position += draft_markers[i].transform.up * FindTileDistance(tile);
        }
    }

    public void NewDraft()
    {
        DeactivateMarkers();

        // logic for new draft choice
    }

    public void DeactivateMarkers()
    {
        foreach (GameObject marker in draft_markers)
        {
            marker.GetComponent<DraftMarker>().Deactivate();
        }
    }
}
