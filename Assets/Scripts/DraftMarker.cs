using UnityEngine;

public class DraftMarker : MonoBehaviour
{
    [SerializeField] Color default_color;
    [SerializeField] Color hovered_color;
    [SerializeField] Color clicked_color;
    Vector3 default_size;
    Vector3 hovered_size;
    SpriteRenderer sprite;
    void Start()
    {
        // Making the size proportionate to tile size
        hovered_size = GameManager.instance.tile_manager.tiles[0].transform.localScale;
        default_size = hovered_size * 0.8f;

        // Setting the color
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = default_color;
    }

    public void Deactivate()
    {
        sprite.color = default_color;
        sprite.size = default_size;
        this.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        // setting the correct size and color when mouse is hovered over this marker
        sprite.color = hovered_color;
        transform.localScale = hovered_size;
    }

    private void OnMouseExit()
    {
        // setting the size and color back to default when mouse is hovered away from this marker
        sprite.color = default_color;
        transform.localScale = default_size;
    }

    private void OnMouseDown()
    {
        // Changing the color when player clicks on the marker
        sprite.color = clicked_color;
    }

    private void OnMouseUp()
    {
        // Drafting a new tile in the picked spot
        GameManager.instance.tile_manager.NewDraft();
    }

}
