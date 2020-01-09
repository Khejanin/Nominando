using System.IO;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private static void loadDialogueXML(string dir)
    {
        var assetPath = Application.dataPath;
        var path = assetPath + "/introductionText.xml";
        Debug.Log(path);
        var fs = new FileStream(path, FileMode.Open);
    }


    /* async Task TestReader(System.IO.Stream stream)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Async = true;

        using (XmlReader reader = XmlReader.Create(stream, settings))
        {
            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        
                        break;
                    case XmlNodeType.Text:
                        Console.WriteLine("Text Node: {0}",
                                 await reader.GetValueAsync());
                        break;
                    case XmlNodeType.EndElement:
                        Console.WriteLine("End Element {0}", reader.Name);
                        break;
                    default:
                        Console.WriteLine("Other node {0} with value {1}",
                                        reader.NodeType, reader.Value);
                        break;
                }
            }
        }
    }
    */
    // Update is called once per frame
    private void Update()
    {
    }
}