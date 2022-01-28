using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<Item> items;
    [System.Serializable]
    public class Item
    {
        public int index;
        public string itemIndexString;
        public int itemBoughtInt;
        public string itemName;
        public Sprite itemImage;
        public bool isUnlocked;
        public bool isActive;
        [TextArea]
        public string itemDescription;
        
    }

    private void Awake()
    {
        foreach(Item item in items)
        {
            item.itemIndexString = "Item_" + item.index.ToString();
            item.itemBoughtInt = PlayerPrefs.GetInt(item.itemIndexString, 0);
            if(item.itemBoughtInt ==1)
            {
                item.isUnlocked = true;
            }
            else
            {
                item.isUnlocked = false;
            }
        }
    }
}
