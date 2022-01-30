using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostInteract : Interactable
{
  [SerializeField] GameObject closedBin;
  [SerializeField] GameObject openedBin;
  [SerializeField] bool opened;

    public override void Interact(Character character)
    {
      if (opened == false)
      {
        opened = true;
        closedBin.SetActive(false);
        openedBin.SetActive(true);
      }

    }
}
