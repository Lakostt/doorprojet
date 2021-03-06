﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get; private set; }
	Dictionary<string,int> _items; // Словарь предметов
	public string EquippedItem {get; private set;}
	public bool EquipItem (string name)
	{
		if (_items.ContainsKey (name) && EquippedItem != name) {
			EquippedItem = name;
			Debug.Log ("Equipped: " + name);
			return true;
		}
		EquippedItem = null;
		Debug.Log ("Unequipped");
		return false;	
	}
    public bool UsingItem(string name)
        {
		if(_items.ContainsKey(name))
        {
            _items[name]--;
            if(_items[name] == 0)
            {
                _items.Remove(name);
            }
        }
        else
        {
            Debug.Log("Item " + name + "is missing");
            return false;
        }
        DisplayItems();
            return true;
    }
	public void StartUp(NetworkService serivce)
	{
		Debug.Log ("Inventory Manager starting..");
		status = ManagerStatus.Started;
		_items = new Dictionary<string, int>();
	}
	void DisplayItems()
	{
		string itemDisplay = "Items: ";
		foreach (KeyValuePair<string,int> item in _items) {
			itemDisplay += item.Key + "(" + item.Value + ")" + " ";
		}
		Debug.Log (itemDisplay);
	}
	public void addItem(string name)
	{
		if (_items.ContainsKey (name))
			_items [name]++;
		else
			_items [name] = 1;
		DisplayItems ();
	}
	public List<string> GetItemList()
	{
		List<string> list = new List<string> (_items.Keys);
		return list;
	}
	public int GetItemCount(string name)
	{
		if(_items.ContainsKey(name)) {
			return _items [name];
				}
		return 0;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
