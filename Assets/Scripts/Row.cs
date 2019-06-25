using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Transform slot;

    public void Setup(int position)
    {
        Config config = GameObject.Find("GameController").GetComponent<Config>();
        int act = config.currentAct;
        int level = config.currentLevel;
        int charactersPerLine = config.charactersPerLine;
        string rowOfText = new Narration(act, level).GetTextFromRow(position, charactersPerLine);

        for (int i = 0; i < rowOfText.Length; i++)
        {
            GameObject currentSlot = Instantiate(slot, gameObject.transform).gameObject;
            float xPosition = i * 100f;
            string character = rowOfText.Substring(i, 1);
            currentSlot.transform.localPosition = new Vector3(xPosition, 0f, 0f);
            currentSlot.GetComponent<Slot>().Setup(position, i);
        }
    }
}
