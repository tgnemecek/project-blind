using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Transform slot;

    public void Setup(string narration, int position)
    {
        for (int i = 0; i < narration.Length; i++)
        {
            GameObject currentSlot = Instantiate(slot, gameObject.transform).gameObject;
            float xPosition = i * 100f;
            string character = narration.Substring(i, 1);
            int charactersPerLine = GameObject.Find("GameController").GetComponent<Config>().charactersPerLine;
            currentSlot.transform.localPosition = new Vector3(xPosition, 0f, 0f);
            currentSlot.GetComponent<Slot>().Setup(character, i + (charactersPerLine * position));
        }
    }
}
