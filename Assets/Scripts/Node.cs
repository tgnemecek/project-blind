using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler
{
    private bool isActive = false;
    private bool isSelected = false;
    private int orderInSelection;
    public GameObject button;
    NodePosition position;
    public GameObject gameController;
    GameObject connection;
    Camera camera;
    
    public static List<NodePosition> selectedNodes = new List<NodePosition>();

    public void Setup(bool[][] matrix, NodePosition thisNodePosition)
    {
        position = thisNodePosition;
        gameController = GameObject.Find("GameController").gameObject;
        connection = GameObject.Find("Connection").gameObject;
        camera = Camera.main;
        
        SetPivot(gameObject.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f));

        isActive = matrix[position.y][position.x];
        if (isActive == true)
        {
            button.GetComponent<Image>().color = Color.black;
        }
    }
    void Update()
    {
        if (gameController.GetComponent<Config>().currentState == Config.GameState.Idle && isSelected)
        {
            isSelected = false;
            button.GetComponent<Image>().color = Color.black;
        }
        if (gameController.GetComponent<Config>().currentState == Config.GameState.DrawingLine && isSelected)
        {
            if (orderInSelection >= selectedNodes.Count)
            {
                isSelected = false;
                button.GetComponent<Image>().color = Color.black;
                RemoveVertex();
            }
        }
    }
    public void OnClick()
    {
        if (isActive)
        {
            if (gameController.GetComponent<Config>().currentState == Config.GameState.Idle)
            {
                ToggleSelection();
                AddToSelection();
                gameController.GetComponent<Config>().ChangeState(Config.GameState.DrawingLine);
            }
            else if (gameController.GetComponent<Config>().currentState == Config.GameState.DrawingLine)
            {
                Reset();
                gameController.GetComponent<Config>().ChangeState(Config.GameState.Idle);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            if (gameController.GetComponent<Config>().currentState == Config.GameState.DrawingLine)
            {
                if (selectedNodes.Count > 0)
                {
                    for (int i = 0; i < selectedNodes.Count; i++)
                    {
                        if (selectedNodes[i].x == position.x && selectedNodes[i].y == position.y && selectedNodes[i].charPosition == position.charPosition)
                        {
                            selectedNodes.RemoveRange(i+1, selectedNodes.Count - i-1);
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
    }
    private void ToggleSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            button.GetComponent<Image>().color = Color.black;
        }
        else
        {
            isSelected = true;
            button.GetComponent<Image>().color = Color.green;
        }
    }
    private void AddToSelection()
    {
        orderInSelection = selectedNodes.Count;
        NewVertex(gameObject.transform.position);
        selectedNodes.Add(position);
    }
    private void Reset()
    {
        selectedNodes = new List<NodePosition>();
        ResetLine();
    }
    private bool CheckIfConnected()
    {
        int charactersPerLine = gameController.GetComponent<Config>().charactersPerLine;
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
        else if (position.charPosition == (lastCharPos + 1) && position.charPosition != 0 && lastX == 1)
        {
            return (position.x == 0 && position.y == lastY);
        }
        // Second Node is in a Slot that is to the left of the current Slot
        else if (position.charPosition == (lastCharPos - 1) && position.charPosition != charactersPerLine && lastX == 0)
        {
            return (position.x == 1 && position.y == lastY);
        }
        return false;
    }
    void SetPivot(RectTransform rectTransform, Vector2 pivot)
    {
        if (rectTransform == null) return;

        Vector2 size = rectTransform.rect.size;
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
    }
    void StartLine()
    {
        LineRenderer renderer = connection.GetComponent<LineRenderer>();
        renderer.enabled = true;
        Vector3 startPosition = gameObject.transform.position;
        startPosition.z = camera.nearClipPlane;
        startPosition = camera.ScreenToWorldPoint(startPosition);
        startPosition.x = gameObject.transform.position.x;
        startPosition.y = gameObject.transform.position.y;
        renderer.SetPosition(0, startPosition);
    }
    void NewVertex(Vector3 vertexPosition)
    {
        LineRenderer renderer = connection.GetComponent<LineRenderer>();
        renderer.positionCount++;
        Vector3[] currentVertexes = new Vector3[renderer.positionCount];
        renderer.GetPositions(currentVertexes);
        Vector3 newPosition = vertexPosition;
        newPosition.z = camera.nearClipPlane;
        newPosition = camera.ScreenToWorldPoint(newPosition);
        newPosition.x = vertexPosition.x;
        newPosition.y = vertexPosition.y;
        renderer.SetPosition(renderer.positionCount-1, newPosition);
    }
    void RemoveVertex()
    {
        LineRenderer renderer = connection.GetComponent<LineRenderer>();
        renderer.positionCount--;
    }
    void ResetLine()
    {
        connection.GetComponent<LineRenderer>().positionCount = 0;
    }
}