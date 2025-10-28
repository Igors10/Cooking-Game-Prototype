using Unity.VisualScripting;
using UnityEngine;

public class DraftingWindow : MonoBehaviour
{
    public DraftingChoice[] drafting_choices = new DraftingChoice[3];
    [SerializeField] float floating_distance = 2f;

    public void ActivateDraft(Tile[] passed_tiles, Vector3 position)
    {
        transform.position = position;
        transform.Translate(0, floating_distance, 0);

        for (int a = 0; a < passed_tiles.Length; a++)
        {
            drafting_choices[a].Init(passed_tiles[a]);
        }
    }
    public void DeactivateDraft()
    {

    }
}
