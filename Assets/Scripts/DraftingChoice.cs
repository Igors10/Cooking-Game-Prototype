using UnityEngine;

public class DraftingChoice : MonoBehaviour
{
    Tile tile;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Vector3 default_size;
    Vector3 hovered_size;
    [SerializeField] DraftingWindow drafting_window;

    private void Start()
    { 
        hovered_size = default_size * 1.2f;
    }

    public void Init(Tile stored_tile)
    {
        transform.localScale = default_size;
        tile = stored_tile;
        // debugging
        if (tile == null) { Debug.Log("DraftingChoice: passed tile does not exist"); }
        if (tile.sprite == null) { Debug.Log("DraftChoice: tile's sprite does not exist"); }
        if (sprite == null) { Debug.Log("DraftingChoice: drafting choices sprite does not exist"); }
        // debugging
        sprite.sprite = tile.sprite.sprite;
    }
    private void OnMouseEnter()
    {
        transform.localScale = hovered_size;
    }

    private void OnMouseExit()
    {
        transform.localScale = default_size;
    }

    private void OnMouseUp()
    {
        GameManager.instance.tile_manager.ChooseTile(tile);
        drafting_window.DeactivateDraft();
    }
}
