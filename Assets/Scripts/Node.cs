using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Node : MonoBehaviour
{
    public int gridX = 0;
    public int gridY = 0;

    Narration narration;
    NodePosition position;
    Config config;
    GameObject gameController;
    GameObject line;

    private bool[][] matrix;
    
    public bool isDisabled = false;
    public bool isBlack = false;
    private bool isSelected = false;
    private int orderInSelection;
    public GameObject circle;
    public GameObject particles;
    private ParticleSystem particleSystem;

    
    public static List<NodePosition> selectedNodes = new List<NodePosition>();

    void Start()
    {
        // Get from parent:
        int absoluteIndex = transform.parent.GetComponent<Slot>().absoluteIndex;
        position = new NodePosition(gridX, gridY, absoluteIndex);
        isDisabled = transform.parent.GetComponent<Slot>().isDisabled;


        config = GameObject.Find("GameController").gameObject.GetComponent<Config>();
        line = GameObject.Find("Connection").transform.Find("Line").gameObject;
        particleSystem = particles.GetComponent<ParticleSystem>();

        int act = config.currentAct;
        int level = config.currentLevel;
        narration = new Narration(act, level);

        matrix = new BrailleDictionary().GetMatrix(narration.GetCharacter(absoluteIndex));
        isBlack = matrix[position.y][position.x];

        if (isBlack)
        {
            SetColor(circle, Color.black);
        }
    }
    void SetColor(GameObject target, Color newColor)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        renderer.material.SetColor("_BaseColor", newColor);
    }
    public void MouseClick()
    {
        if (config.currentState == Config.GameState.Idle)
        {
            ToggleSelection();
            AddToSelection();
            particleSystem.Play();
            config.ChangeState(Config.GameState.DrawingLine);
        }
        else if (config.currentState == Config.GameState.DrawingLine)
        {
            Reset();
            config.ChangeState(Config.GameState.Idle);
        }
    }
    private void ToggleSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            SetColor(circle, Color.black);
        }
        else
        {
            isSelected = true;
            SetColor(circle, Color.yellow);
        }
    }
    private void AddToSelection()
    {
        orderInSelection = selectedNodes.Count;
        NewVertex(gameObject.transform.position);
        selectedNodes.Add(position);
    }
    void NewVertex(Vector3 vertexPosition)
    {
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        int positionCount = renderer.positionCount;
        vertexPosition.z = vertexPosition.z - 0.005f;

        Vector3[] positions = new Vector3[99];
        renderer.GetPositions(positions);

        Array.Resize(ref positions, positionCount+1);

        positions[positionCount] = vertexPosition;
        renderer.positionCount++;
        renderer.SetPositions(positions);
    }
    public void MouseOver()
    {
        if (config.currentState == Config.GameState.DrawingLine)
        {
            if (selectedNodes.Count > 0)
            {
                for (int i = 0; i < selectedNodes.Count; i++)
                {
                    if (selectedNodes[i].x == position.x && selectedNodes[i].y == position.y && selectedNodes[i].charPosition == position.charPosition)
                    {
                        selectedNodes.RemoveRange(i + 1, selectedNodes.Count - i - 1);
                        return;
                    }
                }
                bool isValid = CheckIfConnected();
                if (isValid)
                {
                    AddToSelection();
                    ToggleSelection();
                }
            }
        }
    }
    private bool CheckIfConnected()
    {
        int charactersPerLine = config.charactersPerLine;
        int lastIndex = selectedNodes.Count - 1;
        int lastX = selectedNodes[lastIndex].x;
        int lastY = selectedNodes[lastIndex].y;
        int lastCharPos = selectedNodes[lastIndex].charPosition;
        int lastCharPosInRow = lastCharPos % charactersPerLine;

        int currentCharPosInRow = position.charPosition % charactersPerLine;

        // Second Node is inside the same Slot
        if (position.charPosition == lastCharPos)
        {
            if (position.x == lastX)
            {
                return (position.y == (lastY + 1) || position.y == (lastY - 1));
            }
            else
            {
                return (position.y == lastY);
            }
        }
        // Second Node is in a Slot that is above the current Slot
        else if (currentCharPosInRow == lastCharPosInRow && position.charPosition > lastCharPos)
        {
            return (position.y == 0 && position.x == lastX);
        }
        // Second Node is in a Slot that is below the current Slot
        else if (currentCharPosInRow == lastCharPosInRow && position.charPosition < lastCharPos)
        {
            return (position.y == 2 && position.x == lastX);
        }
        // Second Node is in a Slot that is to the right of the current Slot
        else if (position.charPosition == (lastCharPos + 1) && position.charPosition != charactersPerLine && lastX == 1)
        {
            return (position.x == 0 && position.y == lastY);
        }
        // Second Node is in a Slot that is to the left of the current Slot
        else if (position.charPosition == (lastCharPos - 1) && position.charPosition != 0 && lastX == 0)
        {
            return (position.x == 1 && position.y == lastY);
        }
        return false;
    }
    private void Reset()
    {
        selectedNodes = new List<NodePosition>();
        ResetLine();
    }
    void ResetLine()
    {
        line.GetComponent<LineRenderer>().positionCount = 0;
    }
    void Update()
    {
        if (isDisabled || !isBlack) return;

        if (config.currentState == Config.GameState.Idle && isSelected)
        {
            isSelected = false;
            SetColor(circle, Color.black);
        }
        if (config.currentState == Config.GameState.DrawingLine && isSelected)
        {
            if (orderInSelection >= selectedNodes.Count)
            {
                isSelected = false;
                SetColor(circle, Color.black);
                RemoveVertex();
            }
        }
    }
    void RemoveVertex()
    {
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        renderer.positionCount--;
    }
    //void Update()
    //{
    //    if (isDisabled) return;

    //    if (config.currentState == Config.GameState.Idle && isSelected)
    //    {
    //        isSelected = false;
    //        //circle.GetComponent<Image>().color = Color.black;
    //    }
    //    if (config.currentState == Config.GameState.DrawingLine && isSelected)
    //    {
    //        if (orderInSelection >= selectedNodes.Count)
    //        {
    //            isSelected = false;
    //            //circle.GetComponent<Image>().color = Color.black;
    //            RemoveVertex();
    //        }
    //    }
    //}
    //public void OnClick()
    //{
    //    if (isBlack && !isDisabled)
    //    {
    //        if (config.currentState == Config.GameState.Idle)
    //        {
    //            ToggleSelection();
    //            AddToSelection();
    //            config.ChangeState(Config.GameState.DrawingLine);
    //        }
    //        else if (config.currentState == Config.GameState.DrawingLine)
    //        {
    //            Reset();
    //            config.ChangeState(Config.GameState.Idle);
    //        }
    //    }
    //}
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (isBlack && !isDisabled)
    //    {
    //        if (config.currentState == Config.GameState.DrawingLine)
    //        {
    //            if (selectedNodes.Count > 0)
    //            {
    //                for (int i = 0; i < selectedNodes.Count; i++)
    //                {
    //                    if (selectedNodes[i].x == position.x && selectedNodes[i].y == position.y && selectedNodes[i].charPosition == position.charPosition)
    //                    {
    //                        selectedNodes.RemoveRange(i+1, selectedNodes.Count - i-1);
    //                        return;
    //                    }
    //                }
    //                bool isValid = CheckIfConnected();
    //                if (isValid)
    //                {
    //                    AddToSelection();
    //                    ToggleSelection();
    //                }
    //            }
    //        }
    //    }
    //}
    //private void ToggleSelection()
    //{
    //    if (isSelected)
    //    {
    //        isSelected = false;
    //        //circle.GetComponent<Image>().color = Color.black;
    //    }
    //    else
    //    {
    //        isSelected = true;
    //        //circle.GetComponent<Image>().color = Color.yellow;
    //    }
    //}
    //private void AddToSelection()
    //{
    //    orderInSelection = selectedNodes.Count;
    //    NewVertex(gameObject.transform.position);
    //    selectedNodes.Add(position);
    //}
    //private void Reset()
    //{
    //    selectedNodes = new List<NodePosition>();
    //    ResetLine();
    //}
    //private bool CheckIfConnected()
    //{
    //    int charactersPerLine = config.charactersPerLine;
    //    int lastIndex = selectedNodes.Count - 1;
    //    int lastX = selectedNodes[lastIndex].x;
    //    int lastY = selectedNodes[lastIndex].y;
    //    int lastCharPos = selectedNodes[lastIndex].charPosition;
    //    int lastCharPosInRow = lastCharPos % charactersPerLine;

    //    int currentCharPosInRow = position.charPosition % charactersPerLine;

    //    // Second Node is inside the same Slot
    //    if (position.charPosition == lastCharPos)
    //    {
    //        if (position.x == lastX)
    //        {
    //            return (position.y == (lastY + 1) || position.y == (lastY - 1));
    //        }
    //        else
    //        {
    //            return (position.y == lastY);
    //        }
    //    }
    //    // Second Node is in a Slot that is above the current Slot
    //    else if (currentCharPosInRow == lastCharPosInRow && position.charPosition > lastCharPos)
    //    {
    //        return (position.y == 0 && position.x == lastX);
    //    }
    //    // Second Node is in a Slot that is below the current Slot
    //    else if (currentCharPosInRow == lastCharPosInRow && position.charPosition < lastCharPos)
    //    {
    //        return (position.y == 2 && position.x == lastX);
    //    }
    //    // Second Node is in a Slot that is to the right of the current Slot
    //    else if (position.charPosition == (lastCharPos + 1) && position.charPosition != charactersPerLine && lastX == 1)
    //    {
    //        return (position.x == 0 && position.y == lastY);
    //    }
    //    // Second Node is in a Slot that is to the left of the current Slot
    //    else if (position.charPosition == (lastCharPos - 1) && position.charPosition != 0 && lastX == 0)
    //    {
    //        return (position.x == 1 && position.y == lastY);
    //    }
    //    return false;
    //}
    //void StartLine()
    //{
    //    LineRenderer renderer = connection.GetComponent<LineRenderer>();
    //    renderer.enabled = true;
    //    Vector3 startPosition = gameObject.transform.position;
    //    startPosition.z = camera.nearClipPlane;
    //    startPosition = camera.ScreenToWorldPoint(startPosition);
    //    startPosition.x = gameObject.transform.position.x;
    //    startPosition.y = gameObject.transform.position.y;
    //    renderer.SetPosition(0, startPosition);
    //}
    //void NewVertex(Vector3 vertexPosition)
    //{
    //    LineRenderer renderer = connection.GetComponent<LineRenderer>();
    //    renderer.positionCount++;
    //    Vector3[] currentVertexes = new Vector3[renderer.positionCount];
    //    renderer.GetPositions(currentVertexes);
    //    Vector3 newPosition = vertexPosition;
    //    newPosition.z = camera.nearClipPlane;
    //    newPosition = camera.ScreenToWorldPoint(newPosition);
    //    newPosition.x = vertexPosition.x;
    //    newPosition.y = vertexPosition.y;
    //    renderer.SetPosition(renderer.positionCount-1, newPosition);
    //}
    //void RemoveVertex()
    //{
    //    LineRenderer renderer = connection.GetComponent<LineRenderer>();
    //    renderer.positionCount--;
    //}
    //void ResetLine()
    //{
    //    connection.GetComponent<LineRenderer>().positionCount = 0;
    //}
}