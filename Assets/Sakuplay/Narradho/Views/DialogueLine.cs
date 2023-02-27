using System.Collections.Generic;
using System.Linq;
using Sakuplay.Narradho.Framework;
using Sakuplay.Narradho.Views;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueLine : MonoBehaviour, IInitializable
{
    
    
    private DialogueEditor m_DialogueEditor;

    
    public GameObject dragBody;
    public InputLinePeg input;
    public OutputLinePeg output;
    
    private Vector2 m_InitialMouseDelta;
    
    [SerializeField] private RectTransform menuPosition;
    
    
    public void Initialize(List<object> objects)
    {
        this.m_DialogueEditor = (DialogueEditor)objects.FirstOrDefault(x => x.GetType() == typeof(DialogueEditor));
    }
    
    public void MouseEnter()
    {
        m_DialogueEditor.ShowEditMenu(menuPosition.position);
    }

    public void MouseExit()
    {
    }

    public void Down()
    {
        m_DialogueEditor.HideEditMenu();
        InitializeMouseDelta();

    }

    public void Up()
    {
        m_DialogueEditor.ShowEditMenu(menuPosition.position);
    }
    
    public void Drag()
    {
        m_DialogueEditor.HideEditMenu();
        
        var mousePosition = EventSystem.current.currentInputModule.input.mousePosition;
        this.transform.position = mousePosition + m_InitialMouseDelta;
        
        this.transform.SetAsLastSibling();
        
        output.UpdateLines();
        input.UpdateLines();
    }

    void InitializeMouseDelta()
    {
        Vector2 transformPos = this.transform.position; 
        var mousePosition = EventSystem.current.currentInputModule.input.mousePosition;
        m_InitialMouseDelta = transformPos - mousePosition;
    }


}
