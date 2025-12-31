using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsListUI : KeyCommand
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _characteristicPrefab;
    [SerializeField] private WindowUICommand _windowUICommand;
    private List<CharacteristicsFields> _list = new();
    
    private void Start()
    {
        foreach (var keyValue in _characteristics.CharacteristicsList)
        {
            GameObject characteristicElementUI = Instantiate(_characteristicPrefab, _parent.transform);
            CharacteristicsFields characteristicsFields = characteristicElementUI.GetComponent<CharacteristicsFields>();
            _list.Add(characteristicsFields);
            characteristicsFields.Text.text = keyValue.Key;
            characteristicsFields.Value.text = keyValue.Value.ToString();
        }
    }

    public void UpdateCharacteristics()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            _list[i].Value.text = _characteristics.CharacteristicsList[_list[i].Text.text].ToString();
        }
    }

    public override void Execute()
    {
        _windowUICommand.Execute();
        UpdateCharacteristics();
    }
}
