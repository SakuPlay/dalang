using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sakuplay.Narradho.Framework;
using Sakuplay.Narradho.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class ObjectBar : MonoBehaviour, IInitializable
{
    
    private DialogueEditor m_DialogueEditor;

    public ObjectButton DialogueButton; 
    
    public void Initialize(List<object> objects)
    {
        this.m_DialogueEditor = (DialogueEditor)objects.FirstOrDefault(x => x.GetType() == typeof(DialogueEditor));
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DialogueButton.MOnBeginDrag += DialogueButton_MOnBeginDrag;
        DialogueButton.MOnDrag += DialogueButton_MOnDrag;
        DialogueButton.MOnEndDrag += DialogueButton_MOnEndDrag;
    }

    private void DialogueButton_MOnBeginDrag(PointerEventData eventData)
    {
        var node = m_DialogueEditor.CreateNode(eventData);
        node.gameObject.SetActive(true);
        node.transform.position = ((InputSystemUIInputModule)EventSystem.current.currentInputModule).point.action.ReadValue<Vector2>();
    }
    
    private void DialogueButton_MOnDrag()
    {
        
    }
    
    private void DialogueButton_MOnEndDrag()
    {
        
    }
}
