using UnityEngine;

/// <summary>
///     Used in the audio player test scene. Plays and stops audio clips on menu button presses.
/// </summary>
public class SwipeMenuAudioPlayer : MonoBehaviour
{
    private bool _isPlaying;

    public ExampleMenuAudioPlayer audioPlayer;
    public Sprite notPlayingSprite;
    public Sprite playingSrpite;
    public SpriteRenderer spriteRenderer;

    /// <summary>
    ///     If the clip is currently playing then stops clip else plays the clip.
    /// </summary>
    /// <param name="clip">Clip.</param>
    public void Activate(AudioClip clip)
    {
        if (_isPlaying)
        {
            audioPlayer.StopClip();
            _isPlaying = false;
            spriteRenderer.sprite = notPlayingSprite;
        }
        else
        {
            audioPlayer.PlayClip(clip);
            _isPlaying = true;
            spriteRenderer.sprite = playingSrpite;
        }
    }

    /// <summary>
    ///     Sets _isPlaying to false and updates the sprite accordingly.
    /// </summary>
    public void Deactivate()
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            spriteRenderer.sprite = notPlayingSprite;
        }
    }
}