using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public GameObject[] heroes;
    public GameObject[] characters = new GameObject[3];
    public GameObject[] enemyTypes = new GameObject[1];


    [SerializeField]public bool isPlayerTurn;
    Pathfinder pathfinder;
    public Camera mainCamera; // Reference to the Main camera, assign it in the inspector

    public static GameController Instance;
    public int levelsCleared = 0;

    void Start()
    {
        isPlayerTurn = true; // Start with player's turn
        if(levelsCleared == 0)
        {
            heroes = characters;
            enemies = enemyTypes;
        }
        if(enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        
        if(heroes.Length == 0)
        {
        heroes = GameObject.FindGameObjectsWithTag("Player");
        }

        if (pathfinder == null)
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();

        mainCamera = Camera.main;
        
    }

    //awake method
    void Awake()
    {
        if (Instance != null)
        {
            //destroy gameobject  immediately if there is another instance
            Destroy(gameObject);
            return;
        }
                       
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator TurnLoop()
    {

        //get name of current scene
        string sceneName = SceneLoader.GetCurrentSceneName();


        while (sceneName == "GeneratedScene")
        {
            if(enemies.Length == 0)
            {
                levelsCleared++;
                sceneName = "";
                SceneLoader.LoadChoice();
            }
            
            foreach (GameObject hero in heroes)
            {
            Character heroCharacter = hero.GetComponent<Character>();
            if (heroCharacter.currentHealth <= 0)
            {
                RemoveFromHeroArray(hero, heroes);
                Destroy(hero);
                //break;

            }
            }

            foreach (GameObject enemy in enemies)
            {
            if (enemy == null)
                continue;
            Character enemyCharacter = enemy.GetComponent<Character>();
            if (enemyCharacter.currentHealth <= 0)
            {
                RemoveFromEnemyArray(enemy, enemies);
                Destroy(enemy);
                //break;
            }
            }
            if (isPlayerTurn)
            {
                mainCamera.GetComponent<Interact>().enabled = true; // Enable Interact script
                yield return StartCoroutine(PlayerTurn());
            }
            else
            {
                mainCamera.GetComponent<Interact>().enabled = false; // Disable Interact script
                yield return StartCoroutine(EnemyTurn());
                isPlayerTurn = true;
            }
        }
    }

    void RemoveFromEnemyArray(GameObject character, GameObject[] array)
    {
        List<GameObject> updatedList = new List<GameObject>(array);
        updatedList.Remove(character);
        enemies = updatedList.ToArray();
    }
    void RemoveFromHeroArray(GameObject character, GameObject[] array)
    {
        List<GameObject> updatedList = new List<GameObject>(array);
        updatedList.Remove(character);
        heroes = updatedList.ToArray();
    }

   IEnumerator EnemyTurn()
{
    foreach (GameObject enemy in enemies)
    {
        Character enemyCharacter = enemy.GetComponent<Character>();
        if (enemy.GetComponent<Character>().hasMoved)
            continue;

        GameObject closestHero = null;
        float closestHeroDistance = Mathf.Infinity;

        foreach (GameObject hero in heroes)
        {
            float distance = Vector3.Distance(enemy.transform.position, hero.transform.position);

            if (distance < closestHeroDistance)
            {
                closestHero = hero;
                closestHeroDistance = distance;
            }
        }

        if (closestHero != null)
        {
            Character heroCharacter = closestHero.GetComponent<Character>();

            if (!enemyCharacter.Moving)
            {
                pathfinder.FindPaths(enemyCharacter);

                Tile heroTile = null;
                Tile enemyTile = enemyCharacter.characterTile;

                if (heroTile == null)
                {
                    Tile currentHeroTile = heroCharacter.characterTile;
                    Tile closestTileToHero = null;
                    float closestTileToHeroDistance = Mathf.Infinity;

                    foreach (Tile tileT in pathfinder.currentFrontier.tiles)
                    {
                        float distance = Vector3.Distance(currentHeroTile.transform.position, tileT.transform.position);

                        if (distance < closestTileToHeroDistance)
                        {
                            closestTileToHero = tileT;
                            closestTileToHeroDistance = distance;
                        }
                    }

                    heroTile = closestTileToHero;
                    heroTile.Occupied = true;
                }

            

                Path path = pathfinder.PathBetween(heroTile, enemyTile);
                enemyCharacter.Move(path);

                // Wait for the enemy to stop moving
                while (enemyCharacter.Moving)
                {
                    yield return null;
                }
            }
        }
        if(enemyCharacter.hasAttacked == false)
        {
            Character heroCharacter = closestHero.GetComponent<Character>();
            int rangeInt = (int)enemyCharacter.weapon.range;
            showRangeForAction(enemyCharacter, rangeInt);
            int dmg = GenerateRandomDamage(enemyCharacter.weapon, enemyCharacter);
            foreach (Tile tile in pathfinder.currentFrontier.tiles)
            {
                //if herocharcater is in the tile then takedamage
                if (tile.occupyingCharacter == heroCharacter)
                {
                    heroCharacter.TakeDamage(dmg, "physical");
                    //enemyCharacter.hasAttacked = true;
                }
            }
            


            
        }

        pathfinder.ResetPathfinder();
        enemy.GetComponent<Character>().hasMoved = true;
    }

    
    isPlayerTurn = true;

    foreach (GameObject enemy in enemies)
    {
        Character enemyCharacter = enemy.GetComponent<Character>();
        enemyCharacter.hasMoved = false;
    }


    StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        //check if a player character is moving, if it is set its hasMoved to be true
       foreach (GameObject hero in heroes)
        {
            Character heroCharacter = hero.GetComponent<Character>();

            if(heroCharacter.Moving)
            {
                heroCharacter.hasMoved = true;
            }
        }
        yield return null;
    }


    public void EndTurn()
    {
        isPlayerTurn = false;
        foreach (GameObject hero in heroes)
        {
            Character heroCharacter = hero.GetComponent<Character>();

            heroCharacter.hasMoved = false;
            heroCharacter.hasAttacked = false;
        }
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


    public void StartBattle()
    {
        StartCoroutine(TurnLoop());
    }
}
