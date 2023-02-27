using System;
using System.Collections;
using System.Collections.Generic;
using Sakuplay.Narradho.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;

public class DialogueEditor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] public NodeEditMenu editMenu;

    public DialogueLine nodePrefab;
    public Line linePrefab;

    private void Start()
    {
        nodePrefab.gameObject.SetActive(false);
        linePrefab.gameObject.SetActive(false);
    }

    public void ShowEditMenu(Vector2 position)
    {
        editMenu.gameObject.SetActive(true);
        editMenu.transform.position = position;
    }

    public void HideEditMenu()
    {
        editMenu.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bool hoveredDialogueLine = false;
        bool hoveredEditMenu = false;
        foreach (var o in eventData.hovered)
        {
            if (o.GetComponent<DialogueLine>())
            {
                hoveredDialogueLine = true;
                break;
            }else if (o.GetComponent<NodeEditMenu>())
            {
                hoveredEditMenu = true;
                break;
            }
        }

        if (!hoveredDialogueLine && !hoveredEditMenu)
        {
            editMenu.gameObject.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        editMenu.gameObject.SetActive(false);
    }

    public DialogueLine CreateNode(PointerEventData eventData)
    {
        var newNode = Instantiate(nodePrefab, nodePrefab.transform.parent);

        // reconnect
        newNode.Initialize(Connector.objects);

        ExecuteEvents.Execute<IEndDragHandler>(EventSystem.current.currentSelectedGameObject, eventData, ExecuteEvents.endDragHandler);
        eventData.pointerDrag = null;
        eventData.pointerDrag = newNode.dragBody;
        ExecuteEvents.Execute<IBeginDragHandler>(newNode.dragBody, eventData, ExecuteEvents.beginDragHandler);


        return newNode;
    }
}
