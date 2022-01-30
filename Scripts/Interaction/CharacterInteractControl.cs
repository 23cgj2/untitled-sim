using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterInteractControl : MonoBehaviour
{
    PlayerMove characterControl;
    Rigidbody2D rgbd;
    ToolbarController toolbarController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;

    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapController tileMapController;
    [SerializeField] float maxDistance = 2.5f;

    Vector3Int selectedTilePosition;
    bool selectable;

    Character character;
    [SerializeReference] HighlightController highlightController;

    private void Awake()
    {
      characterControl = GetComponent<PlayerMove>();
      rgbd = GetComponent<Rigidbody2D>();
      toolbarController = GetComponent<ToolbarController>();
    }

    private void Update()
    {
      SelectTile();
      CanSelectCheck();
      Marker();

      Check();

      if(Input.GetKeyDown("h"))
      {
        if(Interact() == true)
        {
          return;
        }
        InteractGrid();
      }

    }

    private void SelectTile()
    {
      selectedTilePosition = tileMapController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
      Vector2 characterPosition = transform.position;
      Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
      markerManager.Show(selectable);
    }

    private void Marker()
    {
      markerManager.markedCellPosition = selectedTilePosition;
    }

    private void Check()
    {
      Vector2 position = rgbd.position + characterControl.lastMoveDirection * offsetDistance;

      Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

      foreach(Collider2D c in colliders)
      {
        Interactable select = c.GetComponent<Interactable>();
        if(select != null)
        {
          highlightController.Highlight(select.gameObject);
          return;

        }
        highlightController.Hide();
      }
    }

    private bool Interact()
    {
      Vector2 position = rgbd.position + characterControl.lastMoveDirection * offsetDistance;

      Item item = toolbarController.GetItem;
      if(item == null) { return false; }
      if(item.onAction == null) { return false; }

      bool complete = item.onAction.OnApply(position);

      return complete;

    }

    private void InteractGrid()
    {
      if(selectable == true)
      {
        TileBase tileBase = tileMapController.GetTileBase(selectedTilePosition);
        TileData tileData = tileMapController.GetTileData(tileBase);
        if(tileData != plowableTiles)
        {
          return;
        }


        if(cropsManager.Check(selectedTilePosition))
        {
          cropsManager.Seed(selectedTilePosition);

        } else {
          cropsManager.Plow(selectedTilePosition);
        }
      }
    }
}
