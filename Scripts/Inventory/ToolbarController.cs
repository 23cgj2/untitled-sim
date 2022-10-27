using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
      get {
        return GameManager.instance.inventoryContainer.slots[selectedTool].item;
      }
    }

    private void Update()
    {
      // float delta = Input.mouseScrollDelta.y;
      // if(delta != 0)
      // {
      //   if(delta > 0)
      //   {
      //     selectedTool += 1;
      //     selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
      //   } else {
      //     selectedTool -= 1;
      //     selectedTool = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);
      //   }
      // }
      if(Input.GetKeyDown("1"))
      {
        selectedTool = 0;
      }
      if(Input.GetKeyDown("2"))
      {
        selectedTool = 1;
      }
      if(Input.GetKeyDown("3"))
      {
        selectedTool = 2;
      }
      if(Input.GetKeyDown("4"))
      {
        selectedTool = 3;
      }
      if(Input.GetKeyDown("5"))
      {
        selectedTool = 4;
      }
      if(Input.GetKeyDown("6"))
      {
        selectedTool = 5;
      }
      if(Input.GetKeyDown("7"))
      {
        selectedTool = 6;
      }
      if(Input.GetKeyDown("8"))
      {
        selectedTool = 7;
      }
      if(Input.GetKeyDown("9"))
      {
        selectedTool = 8;
      }
      if(Input.GetKeyDown("0"))
      {
        selectedTool = 9;
      }
      if(Input.GetKeyDown("-"))
      {
        selectedTool = 10;
      }

      onChange?.Invoke(selectedTool);
    }

    internal void Set(int id)
    {
      selectedTool = id;
    }
}
