using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //array of enemy gameobjects
    [SerializeField] public GameObject[] enemies;
    //array of player gameobjects
    [SerializeField] public GameObject[] heroes;

    // Start is called before the first frame update
    void Start()
    {
        //find all gameobjects with tag "Enemy"
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //find all gameobjects with tag "Player"
        heroes = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
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
                    //find the path to the closest hero
                    //enemyCharacter.FindPath(heroCharacter.characterTile);
                }
            }
        }
        
    }
}
