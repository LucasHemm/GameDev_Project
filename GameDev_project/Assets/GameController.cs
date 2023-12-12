using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    //array of enemy gameobjects
    [SerializeField] public GameObject[] enemies;
    //array of player gameobjects
    [SerializeField] public GameObject[] heroes;

    public bool isPlayerTurn;
    Pathfinder pathfinder;
    

    
    // Start is called before the first frame update
    void Start()
    {

        isPlayerTurn = false;
        //find all gameobjects with tag "Enemy"
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //find all gameobjects with tag "Player"
        heroes = GameObject.FindGameObjectsWithTag("Player");
        if (pathfinder == null)
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerTurn == false)
        {
        //loop thorugh the enemies array and for each of them look for the closest hero
            foreach(GameObject enemy in enemies)
            {
                //set the closest hero to null
                GameObject closestHero = null;
                //set the distance to the closest hero to infinity
                float closestHeroDistance = Mathf.Infinity;
                //loop through the heroes array
                foreach(GameObject hero in heroes)
                {
                    //calculate the distance between the enemy and the hero
                    float distance = Vector3.Distance(enemy.transform.position, hero.transform.position);
                    //if the distance is less than the closest hero distance
                    if(distance < closestHeroDistance)
                    {
                        //set the closest hero to the current hero
                        closestHero = hero;
                        //set the closest hero distance to the current distance
                        closestHeroDistance = distance;
                    }
                }
                //if the closest hero is not null
                if(closestHero != null)
                {
                    //get the character component of the enemy
                    Character enemyCharacter = enemy.GetComponent<Character>();
                    //get the character component of the closest hero
                    Character heroCharacter = closestHero.GetComponent<Character>();
                    //if the enemy is not moving
                    if(!enemyCharacter.Moving)
                    {
                        
                        pathfinder.FindPaths(enemyCharacter);

                        Tile heroTile = null;
                        Tile enemyTile = enemyCharacter.characterTile;


                        //run through the pathfinder currentfrontier and pick get the tile that matched herochracter.characterTile and enemycharacter.characterTile
                        // foreach(Tile tile in pathfinder.currentFrontier.tiles)
                        // {
                        //     List<Tile> tiles = pathfinder.FindAdjacentTilesForEnemy(tile);
                        //    foreach(Tile tile2 in tiles)
                        //    {
                        //     Debug.Log("Tile2: " + tile2);
                        //        if(tile2 == heroCharacter.characterTile)
                        //        {
                        //           heroTile = tile;
                        //        }
                               
                        //    }

                        // }
                        if(heroTile == null)
                        {   
                            Tile currentHeroTile = heroCharacter.characterTile;
                            Tile closestTileToHero = null;
                            float closestTileToHeroDistance = Mathf.Infinity;

                            foreach(Tile tileT in pathfinder.currentFrontier.tiles)
                            {

                            float distance = Vector3.Distance(currentHeroTile.transform.position, tileT.transform.position);

                            if(distance < closestTileToHeroDistance)
                                {
                                //set the closest hero to the current hero
                                closestTileToHero = tileT;
                                //set the closest hero distance to the current distance
                                closestTileToHeroDistance = distance;
                                }
                            }

                            heroTile = closestTileToHero;
                    
                        }
                       





                        Debug.Log("HeroTile: " + heroTile);
                        Debug.Log("EnemyTile: " + enemyTile);
                        Debug.Log("test");

                        



                        //get the path between the enemycharacter and the herocharacter
                        Path path = pathfinder.PathBetween(heroTile, enemyTile);
                        //make the enemy move with the path
                        enemyCharacter.Move(path);
                        
                    }
                }
                Debug.Log("Enemy action");
                //pathfinder.ResetPathfinder();
            }
            Debug.Log("Enemy Turn finished");
            isPlayerTurn = true;
        }
        
    }
}