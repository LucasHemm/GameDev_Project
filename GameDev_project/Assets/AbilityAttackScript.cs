using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityAttackScript : MonoBehaviour
{
    public Interact interact;
    public Button abilityButton;
    public Pathfinder pathfinder;
    public Ability ability;
    public int abilityNumber;
    public Character currentCharacter;
    private Character lastCharacterUsed;

    private bool isInTargetMode = false;

    private string modifier;
    private string damageType;
    

    void Start()
    {
        abilityButton.onClick.AddListener(OnAbilityClick);
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

    void OnAbilityClick()
    {
        isInTargetMode = !isInTargetMode;
        currentCharacter = interact.selectedCharacter;
        
        if (isInTargetMode)
        {
            
            pathfinder.ResetPathfinder();
            if(abilityNumber == 1)
            {
                ability = interact.selectedCharacter.ability1;
            }
            else if(abilityNumber == 2)
            {
                ability = interact.selectedCharacter.ability2;
            }

            
            currentCharacter.isAttacking = true;
            lastCharacterUsed = currentCharacter;
            modifier = ability.modifier;
            damageType = ability.damageType;
            
            showRangeForAbility(interact.selectedCharacter, ability.range);
        }
        else
        {
            lastCharacterUsed.isAttacking = false;
            pathfinder.ResetPathfinder();
        }
    }

    void HandleTargetModeInput()
    {
        int dmg = GenerateRandomDamage(ability, currentCharacter);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Character hitCharacter = hit.collider.GetComponent<Character>();
                if (hitCharacter != null && hitCharacter.isEnemy)
                {
                    // Check if the enemy is in the current frontier tiles
                    if (pathfinder.currentFrontier.tiles.Contains(hitCharacter.characterTile))
                    {
                        ability.currentUses--;
                        hitCharacter.TakeDamage(dmg, damageType);
                        currentCharacter.hasAttacked = true;
                        OnAbilityClick();
                    }
                }
            }
        }
    }



    public int GenerateRandomDamage(Ability ability, Character character)
    {
        int bonusdmg = 0;
        

        if(character.characterClass != null)
        {
            if(modifier == "strength")
            {
                bonusdmg += character.characterClass.strength;
            }
            else if(modifier == "agility")
            {
                bonusdmg += character.characterClass.agility;
            }
            else if(modifier == "intellect")
            {
                bonusdmg += character.characterClass.intellect;
            }
        }

        int randomDamage = UnityEngine.Random.Range(ability.minDamage, ability.maxDamage + 1);
        randomDamage += bonusdmg;
        
        return randomDamage;
    }

    public void showRangeForAbility(Character character, int rangeInt)
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
