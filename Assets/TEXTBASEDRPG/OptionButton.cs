using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionButton : MonoBehaviour
{

    DialogueOption dialogueOption;
     
         public TextMeshProUGUI tmp;
     
         public void updateOption(DialogueOption option)
         {
             dialogueOption = option;
             if(option != null)
                 tmp.SetText(option.optionString);
         }
     
         public void updateText(string text)
         {
             tmp.SetText(text);
         }
     
         public DialogueOption getOption()
         {
             return dialogueOption;
         }

}
