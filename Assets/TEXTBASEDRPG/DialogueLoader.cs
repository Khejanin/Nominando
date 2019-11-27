using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Xml;


public class DialogueLoader : MonoBehaviour
{
    // Start is called before the first frame update
    static void loadDialogueXML(string dir)
    {
        string assetPath = Application.dataPath;
        string path = assetPath + "/introductionText.xml";
        Debug.Log(path);
        FileStream fs = new FileStream(path,FileMode.Open);
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
    void Update()
    {
        
    }
}
