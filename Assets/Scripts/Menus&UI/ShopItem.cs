using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public GameObject ItemBuyButton;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemCostText;
    public Image ItemImage;
    public bool IsBought;
    public int index;
    public string ItemName;
    public string itemIndexString;
    public int itemBoughtInt;
    public int cost;
    public ShopManager shopmanager;
    void Start()
    {
        switch(index)
        {
            case 0:
                ItemName = "Runa zdrowia";
                break;
            case 1:
                ItemName = "Runa ataku";
                break;
            case 2:
                ItemName = "Runa ruchu";
                break;
            case 3:
                ItemName = "Runa spowolnienia";
                break;
            case 4:
                ItemName = "Runa szalu";
                break;
            case 5:
                ItemName = "Runa przeznaczenia";
                break;
            case 6:
                ItemName = "Runa obrony";
                break;
            case 7:
                ItemName = "Worek wielu rzeczy";
                break;
            case 8:
                ItemName = "Pierœcieñ losu";
                break;
            case 9:
                ItemName = "Szczêœliwa moneta";
                break;

        }
        ItemNameText.text = ItemName;
        ItemCostText.text = "Koszt: " + cost.ToString();
        shopmanager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        itemIndexString = "Item_" + index.ToString();
        itemBoughtInt = PlayerPrefs.GetInt(itemIndexString, 0);
        if(itemBoughtInt == 1)
        {
            IsBought = true;
            ItemBuyButton.SetActive(false);
            ItemImage.color = Color.white;
        }
        else
        {
            IsBought = false;
            ItemImage.color = new Color(0.4339623f, 0.4339623f, 0.4339623f);
        }

    }

    public void BuyItem()
    {
        if(shopmanager.PlayerPoints >= cost && !IsBought)
        {
            ItemBuyButton.SetActive(false);
            IsBought = true;
            ItemImage.color = Color.white;
            shopmanager.UpdatePoints(cost);
            PlayerPrefs.SetInt(itemIndexString, 1);
        }
    }
}
