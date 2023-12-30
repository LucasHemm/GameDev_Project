using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAttackScript : MonoBehaviour
{
    public Interact interact;
    public Button basicAttack;
    public Pathfinder pathfinder;
    public Weapon weapon1;
    public Character currentCharacter;

    private bool isInTargetMode = false;

    void Start()
    {
        basicAttack.onClick.AddListener(OnBasicAttackClick);
        if (pathfinder.illustrator == null)
            pathfinder.illustrator = GetComponent<PathIllustrator>();
    }

    void Update()
    {
        if (isInTargetMode)
        {
            HandleTargetModeInput();
        }
    }

    void OnBasicAttackClick()
    {
        isInTargetMode = !isInTargetMode;
        


        if (isInTargetMode)
        {
            
            pathfinder.ResetPathfinder();
            weapon1 = interact.selectedCharacter.weapon;
            currentCharacter = interact.selectedCharacter;
            showRangeForAction(interact.selectedCharacter, (int)interact.selectedCharacter.weapon.range);
        }
        else
        {
            
            pathfinder.ResetPathfinder();
        }
    }

    void HandleTargetModeInput()
    {
       int dmg = GenerateRandomDamage(weapon1,currentCharacter);
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Character hitCharacter = hit.collider.GetComponent<Character>();
                
                if (hitCharacter != null && hitCharacter.isEnemy)
                {
                    if(pathfinder.currentFrontier.tiles.Contains(hitCharacter.characterTile))
                    {
                        hitCharacter.TakeDamage(dmg,"physical");
                        currentCharacter.hasAttacked = true;
                        OnBasicAttackClick();
                    }
                    
                }
            }
        }
    }



    public int GenerateRandomDamage(Weapon weapon, Character character)
    {
        int bonusdmg = 0;

        if(character.characterClass != null)
        {
            if(weapon.type == "Melee")
            {
                bonusdmg += character.characterClass.strength;
            }
            else if(weapon.type == "Ranged")
            {
                bonusdmg += character.characterClass.agility;
            }
        }

        int randomDamage = UnityEngine.Random.Range(weapon.minDamage, weapon.maxDamage + 1);
        randomDamage += bonusdmg;
        
        return randomDamage;
    }

    public void showRangeForAction(Character character, int rangeInt)
    {
        pathfinder.ResetPathfinder();

        Queue<Tile> openSet = new Queue<Tile>();
        openSet.Enqueue(character.characterTile);
        character.characterTile.cost = 0;

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet.Dequeue();

            foreach (Tile adjacentTile in pathfinder.FindAdjacentTilesForEnemy(currentTile))
            {
                if (openSet.Contains(adjacentTile))
                    continue;

                adjacentTile.cost = currentTile.cost + 1;

                if (!pathfinder.IsValidTile(adjacentTile, rangeInt))
                    continue;

                adjacentTile.parent = currentTile;

                openSet.Enqueue(adjacentTile);
                pathfinder.AddTileToFrontier(adjacentTile);
            }
        }

        pathfinder.illustrator.IllustrateFrontier(pathfinder.currentFrontier);
    }
}
