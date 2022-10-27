using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]

public class PlowTile : ToolAction

{
   [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapController tileMapController, Item item)
    {
      TileBase tileToPlow = tileMapController.GetTileBase(gridPosition);

      if(canPlow.Contains(tileToPlow) == false)
      {
        return false;
      }

      tileMapController.cropsManager.Plow(gridPosition);

      return true;
    }
}
