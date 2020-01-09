using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Used in the multiple menu test scene to update the stars shown when the use presses a button.
/// </summary>
public class UpdateMenuItemOnClick : MonoBehaviour
{
    private int _currentItem = -1;
    public Text debugText;

    public SpriteRenderer[] starRenderers;

    public Sprite starSprite;
    public TextMesh titleText;

    /// <summary>
    ///     Updates the star sprite on button press.
    /// </summary>
    public void UpdateStar()
    {
        debugText.text = "Load: " + titleText.text;

        if (_currentItem == 2)
            return;

        _currentItem = (_currentItem + 1) % starRenderers.Length;

        starRenderers[_currentItem].sprite = starSprite;
    }
}