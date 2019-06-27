using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // Pass to children:
    public bool isDisabled = false;
    public int absoluteIndex;

    public GameObject overlay;

    // Later usage, to enable different types of slots:
    public string type;

    public void Setup(int rowIndex, int relativeIndex)
    {
        Config config = GameObject.Find("GameController").GetComponent<Config>();
        int act = config.currentAct;
        int level = config.currentLevel;
        int charactersPerLine = config.charactersPerLine;
        
        absoluteIndex = relativeIndex + (charactersPerLine * rowIndex);

        Narration narration = new Narration(act, level);
        isDisabled = !(absoluteIndex >= narration.initialIndex && absoluteIndex <= narration.lastIndex);

        if (isDisabled)
        {
            overlay.GetComponent<Renderer>().enabled = true;
        }
    }
}
