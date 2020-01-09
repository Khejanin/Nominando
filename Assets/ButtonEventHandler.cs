using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EventSystem
{
    public class ButtonEventHandler : MonoBehaviour
    {
        public EventInfo _eventInfo;

        public void SetEventInfo(EventInfo info)
        {
            if (info)
            {
                SetInteractable(true);
                _eventInfo = info;
                SetText(info.buttonOptionString);
            }
            else
            {
                SetText("");
                SetInteractable(false);
            }
        }

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (_eventInfo != null)
                    EventSystem.FireEvent(_eventInfo);
                else
                    Debug.Log(
                        "A button tried to send NULL to the Event System! Check if your data is linked properly!");
            });
        }

        public void SetText(string text)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
        }

        public void SetInteractable(bool enabled)
        {
            gameObject.GetComponent<Button>().interactable = enabled;
        }
    }
}