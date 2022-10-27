using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
  Undefined,
  Compost,
  Arugula,
  Beet,
  BellPepper,
  Broccoli,
  Cabbage,
  Carrot,
  Celery,
  // pick 3
  Cucumber,
  Eggplant,
  GreenBean,
  Kale,
  Lettuce,
  Onion,
  Parsnip,
  Pea,
  // pick 2
  Radish,
  Spinach,
  SweetPotato,
  // pick 1
  SwissChard,
  Tomato,
  Turnip,
  Zucchini

}

[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]

public class GatherResourceNode : ToolAction
{
  [SerializeField] float sizeOfInteractableArea = 1f;
  [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    public override bool OnApply(Vector2 worldPoint)
    {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

      foreach(Collider2D c in colliders)
      {
        ToolHit select = c.GetComponent<ToolHit>();
        if(select != null)
        {
          if (select.CanBeHit(canHitNodesOfType) == true )
          {
            select.Hit();
            return true;
          }

        }

      }
      return false;
    }
}
