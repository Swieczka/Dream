using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI PointsCounter;
    public List<ShopItem> ItemList;
    public int PlayerPoints;
    void Start()
    {
        PlayerPoints = PlayerPrefs.GetInt("EXP", 0);
        PointsCounter.text = "Twoje punkty: " + PlayerPoints.ToString();
    }

    void Update()
    {
        
    }

    public void UpdatePoints(int cost)
    {
        PlayerPoints -= cost;
        PlayerPrefs.SetInt("EXP", PlayerPoints);
        PointsCounter.text = "Twoje punkty: " + PlayerPoints.ToString();
    }
}
