using SwipeMenu;
using UnityEngine;

/// <summary>
///     Enables a mesh renderer when a menu item is centred and conversly disables renderer when menu not centred.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class ShowTextOnMenuCentred : MonoBehaviour
{
    private MeshRenderer _text;
    public MenuItem ownerMenu;

    private void Start()
    {
        _text = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Menu.instance.MenuCentred(ownerMenu))
            _text.enabled = true;
        else
            _text.enabled = false;
    }
}