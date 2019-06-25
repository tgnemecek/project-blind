using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public string character;
    private int characterIndex;
    private bool[][] matrix;
    public Transform node;

    public string type;
    public bool isEnergized;

    public void Setup(string character, int index)
    {
        matrix = new BrailleDictionary().GetMatrix(character);
        characterIndex = index;
        CreateNodes();
    }

    void CreateNodes()
    {
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                GameObject currentNode = Instantiate(node, gameObject.transform).gameObject;
                SetPositions(currentNode, x, y);
                NodePosition thisNodePosition = new NodePosition(x, y, characterIndex);
                currentNode.GetComponent<Node>().Setup(matrix, thisNodePosition);
            }
        }
    }
    void SetPositions(GameObject currentNode, int x, int y)
    {
        RectTransform panelRectTransform = currentNode.GetComponent<RectTransform>();
        float slotWidth = currentNode.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float slotHeight = currentNode.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        float width = currentNode.GetComponent<RectTransform>().rect.width;
        float height = currentNode.GetComponent<RectTransform>().rect.height;
        float borderX = 0f;
        float borderY = -0f;
        float positionX = borderX;
        float positionY = borderY;

        if (x == 1)
        {
            borderX = -borderX;
            positionX = slotWidth - width + borderX;
        }
        if (y == 1)
        {
            positionY = (-slotHeight + height) / 2;
        }
        else if (y == 2)
        {
            positionY = -slotHeight + height - borderY;
        }
        currentNode.transform.localPosition = new Vector3(positionX, positionY, 0f);
    }
}
