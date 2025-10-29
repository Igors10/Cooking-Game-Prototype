using UnityEngine;

public class DraftingChoice : MonoBehaviour
{
    Tile tile;
    SpriteRenderer sprite;
    Vector3 default_size;
    Vector3 hovered_size;
    [SerializeField] DraftingWindow drafting_window;

    private void Start()
    {
        default_size = transform.localScale;
        hovered_size = default_size * 1.2f;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Init(Tile stored_tile)
    {
        transform.localScale = default_size;
        tile = stored_tile;
        sprite.sprite = tile.sprite.sprite;
    }
    private void OnMouseEnter()
    {
        transform.localScale = default_size;
    }

    private void OnMouseExit()
    {
        transform.localScale = hovered_size;
    }

    private void OnMouseUp()
    {
        GameManager.instance.tile_manager.ChooseTile(tile);
        drafting_window.DeactivateDraft();
    }
}
