using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistorySystem : MonoBehaviour
{

    private static HistorySystem hs;
    private TextMeshProUGUI tmpro;
    private ContentSizeFitter csf;
    private VerticalLayoutGroup vlg;
    

    public GameObject content;
    
    private HistorySystem()
    {
        
    }

    public static HistorySystem GetHistorySystem()
    {
        return hs;
    }

    public void addText(string text)
    {
        tmpro.text = tmpro.text + "\n" + text;
        APIHandler.getAPIHandler().UploadText(tmpro.text);
        setAllDirty();
    }

    public void setText(string text)
    {
        tmpro.text = text;
        setAllDirty();
    }

    private void setAllDirty()
    {
        tmpro.SetAllDirty();
        Canvas.ForceUpdateCanvases();
        vlg.enabled = false;
        vlg.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        hs = this;
        tmpro = GetComponent<TextMeshProUGUI>();
        csf = content.GetComponent<ContentSizeFitter>();
        vlg = content.GetComponent<VerticalLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
