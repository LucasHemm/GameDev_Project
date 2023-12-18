using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public GameObject[] heroes;

    [SerializeField]public bool isPlayerTurn;
    Pathfinder pathfinder;
    public Camera mainCamera; // Reference to the Main camera, assign it in the inspector


    void Start()
    {
        isPlayerTurn = false; // Start with player's turn
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        heroes = GameObject.FindGameObjectsWithTag("Player");
        if (pathfinder == null)
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();

        mainCamera = Camera.main;
        StartCoroutine(TurnLoop());
    }

    IEnumerator TurnLoop()
    {
        while (true)
        {
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

   IEnumerator EnemyTurn()
{
    foreach (GameObject enemy in enemies)
    {
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
            Character enemyCharacter = enemy.GetComponent<Character>();
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

                Debug.Log("HeroTile: " + heroTile);
                Debug.Log("EnemyTile: " + enemyTile);
                Debug.Log("test");

                Path path = pathfinder.PathBetween(heroTile, enemyTile);
                enemyCharacter.Move(path);

                // Wait for the enemy to stop moving
                while (enemyCharacter.Moving)
                {
                    yield return null;
                }
            }
        }

        pathfinder.ResetPathfinder();
        enemy.GetComponent<Character>().hasMoved = true;
    }

    Debug.Log("Enemy Turn finished");
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
        }
    }

}
