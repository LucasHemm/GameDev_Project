using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    [SerializeField] int biomeCount = 1;
    [SerializeField] Material[] materials = new Material[6];
    [SerializeField] int enemyCount = 3;
    [SerializeField] GameObject enemyPrefab;

    public GameController gameController;

    [SerializeField] GameObject[] flowers = new GameObject[8];

    [SerializeField] GameObject[] obstacles = new GameObject[6];

    public PersistenceBetweenScenes persistenceSO;

    
    #region member fields
    GameObject parent;
    GameObject startTile;
    Vector3 gridPosition;
    List<GameObject> tiles;
    
   
    #endregion

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //find start tile by name
        startTile = GameObject.Find("StartTile");
        CreateGrid(RandomizeGridSize());
        tiles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        tiles.Remove(startTile);
        Destroy(startTile);
        SetTileMaterial(tiles);
        

        //Spawn enemies
        SpawnEnemies();
        SpawnPlayer();
        gameController.StartBattle();
    }

     public void CreateGrid(Vector2Int gridSize)
    {
        TileGenerator tg;

        AssignGridParent();

        if (!parent.GetComponent<TileGenerator>())
            tg = parent.AddComponent<TileGenerator>();
        else
            tg = parent.GetComponent<TileGenerator>();

        tg.AutoGenerateGrid(startTile, gridSize);
    }

    void AssignGridParent()
    {
        if (parent == null)
            parent = new GameObject("Grid");

        parent.transform.position = gridPosition;
    }




    //Returns gridsize vector to be random numbers between 7 and 14
    Vector2Int RandomizeGridSize()
    {
        int x = Random.Range(7, 15);
        int y = Random.Range(7, x);
        
        return new Vector2Int(x, y);
    }   

    void SetTileMaterial(List<GameObject> tiles)
{
    foreach (GameObject tileObject in tiles)
    {
        Tile tile = tileObject.GetComponent<Tile>();
        int material;

        // Generate a random number between 0 and 1
        float randomValue = Random.value;

        // Set material based on probability
        if (randomValue <= 0.7f)
        {
            material = 1;
        }
        else
        {
            material = 2;
        }

        SetTileColour(tile, material);

        // Check if the tile should have an obstacle
        float obstacleSpawnChance = Random.value;
        if (obstacleSpawnChance >= 0.93f && obstacleSpawnChance <= 0.97f)
        {
            SpawnObstacle(tile);
        }

        // Check if the tile should have flowers
        else if (material == 1 && randomValue <= 0.3f && biomeCount != 3)
        {
            SpawnFlowers(tile);
        }
    }
}


   // sets colour for each tile depending on the tile cost
    void SetTileColour(Tile tile, int material)
    {
        if(biomeCount ==1)
        {        
            switch (material)
            {
                case 1:
                    tile.GetComponent<Renderer>().sharedMaterial = materials[0];
                    break;
                case 2:
                    tile.GetComponent<Renderer>().material = materials[1];
                    break;
                default:
                    break;
            }
        }
        else if(biomeCount ==2)
        {
            switch (material)
            {
                case 1:
                    tile.GetComponent<Renderer>().sharedMaterial = materials[2];
                    break;
                case 2:
                    tile.GetComponent<Renderer>().material = materials[3];
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (material)
            {
                case 1:
                    tile.GetComponent<Renderer>().sharedMaterial = materials[4];
                    break;
                case 2:
                    tile.GetComponent<Renderer>().material = materials[5];
                    break;
                default:
                    break;
            }
        }
    }

   //Spawn enemies on random tiles
    void SpawnEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++)
        {
            Tile tile = GetRandomTile();
            if (tile.Occupied)
            {
                i--;
                continue;
            }
            enemyPrefab = gameController.enemyTypes[Random.Range(0, gameController.enemies.Length)];
            GameObject enemy = Instantiate(enemyPrefab, tile.transform.position, Quaternion.identity);
            enemy.GetComponent<Character>().FinalizePosition(tile);
            enemies.Add(enemy);
        }
        gameController.enemies = enemies.ToArray();

    }

    Tile GetRandomTile()
    {
        int randomTile = Random.Range(0, tiles.Count);
        return tiles[randomTile].GetComponent<Tile>();
    }

    //Spawn player on random tile

    void SpawnPlayer()
    {

        
        if(persistenceSO.levelsCleared != 0)
        {
            Debug.Log("persistenceSO.characters[0] != null");
            List<GameObject> heroes = new List<GameObject>();

            int counter = 0;
            foreach(GameObject hero in gameController.characters)
            {
                Debug.Log(counter);
                if(persistenceSO.characterClassNames.Contains(hero.GetComponent<Character>().characterClass.className))
                {
                    Debug.Log("yessir");
                    
                
                Tile tile = GetRandomTile();
                while (tile.Occupied)
                {   
                    tile = GetRandomTile();
                }
                
                Debug.Log("hero.GetComponent<Character>().currentHealth = " + persistenceSO.currentHealths[counter] + "" + persistenceSO.armors[counter].itemName + "" + persistenceSO.weapons[counter].itemName);
                GameObject player = Instantiate(hero, tile.transform.position, Quaternion.identity);
                player.GetComponent<Character>().healthBar.SetHealth(persistenceSO.currentHealths[counter]);
                player.GetComponent<Character>().currentHealth = persistenceSO.currentHealths[counter];
                player.GetComponent<Character>().armor = persistenceSO.armors[counter];
                player.GetComponent<Character>().weapon = persistenceSO.weapons[counter];
                player.GetComponent<Character>().characterClass.className = persistenceSO.characterClassNames[counter];
                player.GetComponent<Character>().FinalizePosition(tile);
                heroes.Add(player);
                counter++;  
            
            
              }
            }
            gameController.heroes = heroes.ToArray();
            return;
        }
        else
        {
        Debug.Log("**************persistenceSO.characters[0] == null");
        List<GameObject> heroes = new List<GameObject>();
        //Character charactertest;
        foreach(GameObject hero in gameController.characters)
        {
            Tile tile = GetRandomTile();
            while (tile.Occupied)
            {
                tile = GetRandomTile();
            }
            GameObject player = Instantiate(hero, tile.transform.position, Quaternion.identity);
            player.GetComponent<Character>().FinalizePosition(tile);
            heroes.Add(player);
        }
        gameController.heroes = heroes.ToArray();
        }
    }

    //Spawns flowers on 
    void SpawnFlowers(Tile tile)
    {
        int flowerCount = Random.Range(0, 7);
        if(biomeCount ==2)
        {
            flowerCount = 7;
        }
        
        GameObject flower = Instantiate(flowers[flowerCount], tile.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity);

    
    }

   //Spawns obstacles on given tile
    void SpawnObstacle(Tile tile)
    {
        int obstacleType = UnityEngine.Random.Range(0, 6);
        if(biomeCount == 1  && obstacleType == 5)
        {
            obstacleType = 0;
        }
        else if(biomeCount ==2 && obstacleType == 0)
        {
            obstacleType = 3;
        }
        else if (biomeCount == 3 && (obstacleType == 0 || obstacleType == 3 || obstacleType == 4))
        {
            obstacleType = 5;
        }

        GameObject obstacle = Instantiate(obstacles[obstacleType], tile.transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        tile.Occupied = true;

    }   
}
