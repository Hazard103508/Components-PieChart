using Charts.Pie;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public PieChart chartA;
    public PieChart chartB;
    public PieChart chartC;
    public PieChart chartD;

    void Start()
    {
        chartA.Add_Item(new Item("Germany", 500, Color.green));
        chartA.Add_Item(new Item("United Kingdom", 450, Color.red));
        chartA.Add_Item(new Item("France", 800, Color.blue, Color.white));
        chartA.Add_Item(new Item("Italy", 200, Color.yellow));
        chartA.Add_Item(new Item("Spain", 350, Color.gray));
        chartA.Draw();

        chartB.Add_Item(new Item("Yes", 800, Color.cyan));
        chartB.Add_Item(new Item("No", 350, Color.magenta, Color.white));
        chartB.Draw();

        chartC.Add_Item(new Item("Green", 0.589f, Color.green));
        chartC.Add_Item(new Item("Red", 0.257f, Color.red));
        chartC.Add_Item(new Item("White", 1.2f, Color.blue, Color.white));
        chartC.Add_Item(new Item("Yellow", 0.879f, Color.yellow));
        chartC.Draw();

        chartD.Add_Item(new Item("English", 17.15f, Color.white));
        chartD.Add_Item(new Item("French", 15.6f, Color.blue, Color.white));
        chartD.Add_Item(new Item("Italian", 12.3f, Color.yellow));
        chartD.Draw();
    }
}
