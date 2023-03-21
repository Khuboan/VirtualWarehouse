using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownTest : MonoBehaviour
{
    public static string[] showText;
    public Sprite[] sprite;
    Dropdown dropDownItem;
    List<string> temoNames;
    List<Sprite> sprite_list;
    void Start()
    {
        dropDownItem = this.GetComponent<Dropdown>();
        temoNames = new List<string>();
        sprite_list = new List<Sprite>();


        AddNames();
        UpdateDropDownItem(temoNames);


    }
    void UpdateDropDownItem(List<string> showNames)
    {
        dropDownItem.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < showNames.Count; i++)
        {
            temoData = new Dropdown.OptionData();
            temoData.text = showNames[i];
            temoData.image = sprite_list[i];
            dropDownItem.options.Add(temoData);
        }

        dropDownItem.captionText.text = showNames[0];

    }

    void AddNames()
    {
        for (int i = 0; i < showText.Length; i++)
        {
            temoNames.Add(showText[i]);
        }
        for (int i = 0; i < sprite.Length; i++)
        {
            sprite_list.Add(sprite[i]);
        }
    }
}
