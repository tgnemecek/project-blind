using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCircle : MonoBehaviour
{
    private bool isDisabled = false;
    private bool isBlack = false;

    Config config;

    private void Start()
    {
        isDisabled = transform.parent.GetComponent<Node>().isDisabled;
        isBlack = transform.parent.GetComponent<Node>().isBlack;
    }
    void OnMouseDown()
    {
        if (isDisabled || !isBlack) return;
        transform.parent.GetComponent<Node>().MouseClick();
    }
    void OnMouseOver()
    {
        if (isDisabled || !isBlack) return;
        transform.parent.GetComponent<Node>().MouseOver();
    }
}
