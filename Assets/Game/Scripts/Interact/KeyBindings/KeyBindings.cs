using System.Windows.Input;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    [SerializeField] private KeyCommand _dashCommand;
    [SerializeField] private KeyCommand _inventoryUICommand;
    [SerializeField] private KeyCommand _interact;
    [SerializeField] private KeyCommand _characteristicsUICommand;
    
    [SerializeField] private KeyCommand _skillPanelKeyBindQ;
    [SerializeField] private KeyCommand _skillPanelKeyBindR;
    [SerializeField] private KeyCommand _skillPanel1KeyBind1;
    [SerializeField] private KeyCommand _skillPanel1KeyBind2;
    [SerializeField] private KeyCommand _skillPanel1KeyBind3;
    [SerializeField] private KeyCommand _skillPanel1KeyBind4;
    [SerializeField] private KeyCommand _skillPanel1KeyBind5;
    [SerializeField] private KeyCommand _skillPanel1KeyBind6;
    [SerializeField] private KeyCommand _skillPanel1KeyBind7;
    [SerializeField] private KeyCommand _skillPanel1KeyBind8;
    [SerializeField] private KeyCommand _skillPanel1KeyBind9;
    [SerializeField] private KeyCommand _skillPanel1KeyBind10;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashCommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryUICommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interact?.Execute();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _characteristicsUICommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _skillPanel1KeyBind1.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _skillPanel1KeyBind2.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _skillPanel1KeyBind3.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _skillPanel1KeyBind4.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _skillPanel1KeyBind5.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _skillPanel1KeyBind6.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _skillPanel1KeyBind7.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _skillPanel1KeyBind8.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _skillPanel1KeyBind9.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _skillPanel1KeyBind10.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _skillPanelKeyBindQ.Execute();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            _skillPanelKeyBindR.Execute();
        }
    }
    public void BindInteract(KeyCommand keyCommand)
    {
        _interact = keyCommand;
    }
}


