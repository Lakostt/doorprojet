using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	void OnGUI()
	{
		int posX = 10;
		int posY = 10;
		int width = 100;
		int height = 30;
		int offset = 10;
		List<string> items = Managers.Inventory.GetItemList ();
		if (items.Count == 0) {
			GUI.Box (new Rect (posX,posY,width,height), "No Items");
		}
		foreach(string item in items)
		{
			int count = Managers.Inventory.GetItemCount (item);
			Texture2D image = Resources.Load<Texture2D> ("Icons/" + item);
			GUI.Box (new Rect (posX,posY,width,height), new GUIContent ("[" + count + "]",image));
			posX += width + offset;
		}
		string equipped = Managers.Inventory.EquippedItem;
		if (equipped != null) {
			posX = Screen.width - width - offset;
			Texture2D image = Resources.Load<Texture2D> ("Icons/" + items);
			GUI.Box (new Rect(posX, posY, width,height), new GUIContent("Equipped",image));
		}
		posX = 10;
		posY += height + offset;
		foreach(string item in items)
		{
			if (GUI.Button (new Rect (posX, posY, width, height), "Equip" + item)) {
				Managers.Inventory.EquipItem (item);
				if (name == "Health") {
					Managers.Player.ChangeHealth (25);
				}
			}
			posX += width + offset;
		}
		}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
