using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public Interact interact;
    public Button basicAttack;
    [SerializeField]
    LayerMask tileMask;     

    public PathIllustrator illustrator;
    public Frontier currentFrontier = new Frontier();




    void Start()
    {
        basicAttack.onClick.AddListener(OnBasicAttackClick);
        if (illustrator == null)
            illustrator = GetComponent<PathIllustrator>();
    }

    void Update()
    {
    }

    void OnBasicAttackClick()
    {
        //get the selecetd character from interact
        Character selectedCharacter = interact.selectedCharacter;
        //get the range of the selected characters weapon and convert the the range from a double to an int
        double range = selectedCharacter.weapon.range;
        int rangeInt = (int)range;
        


        //call showRangeForAction function

        showRangeForAction(selectedCharacter, rangeInt);
        
        //debug it
        Debug.Log("basic attack clicked");


        //make it so when basicAttack is clicked it will create a circle around the interact selected character
    }


    public void showRangeForAction(Character character,int rangeInt)
    {
        ResetPathfinder();

        Queue<Tile> openSet = new Queue<Tile>();
        openSet.Enqueue(character.characterTile);
        character.characterTile.cost = 0;
    
        while (openSet.Count > 0)
        {
            Tile currentTile = openSet.Dequeue();

            foreach (Tile adjacentTile in FindAdjacentTiles(currentTile))
            {
                if (openSet.Contains(adjacentTile))
                    continue;

                adjacentTile.cost = currentTile.cost + 1;

                if (!IsValidTile(adjacentTile, rangeInt))
                    continue;

                adjacentTile.parent = currentTile;

                openSet.Enqueue(adjacentTile);
                AddTileToFrontier(adjacentTile);
            }
        }

        illustrator.IllustrateFrontier(currentFrontier);
    }
    public void ResetPathfinder()
    {
        illustrator.Clear();

        foreach (Tile item in currentFrontier.tiles)
        {
            item.InFrontier = false;
            item.ClearColor();
        }

        currentFrontier.tiles.Clear();
    }
    public List<Tile> FindAdjacentTiles(Tile origin)
    {
        List<Tile> tiles = new List<Tile>();
        Vector3 direction = Vector3.forward;
        float rayLength = 50f;
        float rayHeightOffset = 1f;

        //Rotate a raycast in 60 degree steps and find all adjacent tiles
        for (int i = 0; i < 6; i++)
        {
            direction = Quaternion.Euler(0f, 60f, 0f) * direction;

            Vector3 aboveTilePos = (origin.transform.position + direction).With(y: origin.transform.position.y + rayHeightOffset);

            if (Physics.Raycast(aboveTilePos, Vector3.down, out RaycastHit hit, rayLength, tileMask))
            {
                Tile hitTile = hit.transform.GetComponent<Tile>();
                if (hitTile.Occupied == false)
                    tiles.Add(hitTile);
            }
        }

        if (origin.connectedTile != null)
            tiles.Add(origin.connectedTile);

        return tiles;
    }
    bool IsValidTile(Tile tile, int maxcost)
    {
        bool valid = false;

        if (!currentFrontier.tiles.Contains(tile) && tile.cost <= maxcost)
            valid = true;

        return valid;
    }

    void AddTileToFrontier(Tile tile)
    {
        tile.InFrontier = true;
        currentFrontier.tiles.Add(tile);
    }


}
