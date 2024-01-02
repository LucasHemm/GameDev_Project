using UnityEngine;

public class Interact : MonoBehaviour
{
    #region member fields
    [SerializeField]
    AudioClip click;
    [SerializeField]
    AudioClip pop;
    [SerializeField]
    LayerMask interactMask;

    Camera mainCam;
    Tile currentTile;
    public Character selectedCharacter;
    Pathfinder pathfinder;
    #endregion

    private void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();

        if (pathfinder == null)
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
    }

    private void Update()
    {
        Clear();
        MouseUpdate();
    }

    public void NewMoveCharacter()
    {
        InspectCharacter();
    }

    private void MouseUpdate()
    {
        if (!Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f, interactMask))
            return;

        currentTile = hit.transform.GetComponent<Tile>();
        InspectTile();
    }

    private void InspectTile()
    {
        if (currentTile.Occupied)
            InspectCharacter();
        else
            NavigateToTile();
    }

    private void InspectCharacter()
    {
        //if (currentTile.occupyingCharacter.Moving)
          //  return;

        currentTile.SetColor(TileColor.Highlighted);

        if (Input.GetMouseButtonDown(0))
            SelectCharacter();
    }

    private void Clear()
    {
        if (currentTile == null  || currentTile.Occupied == false)
            return;

        currentTile.ClearColor();
        currentTile = null;
    }

   private void SelectCharacter()
{
    selectedCharacter = currentTile.occupyingCharacter;

    // Check if the selected character is an enemy
    if (IsEnemy(selectedCharacter))
    {
        // If it's an enemy, do not select it
        selectedCharacter = null;
        return;
    }

    else if (selectedCharacter.hasMoved == false)
    {
        pathfinder.FindPaths(selectedCharacter);
    }   
    else if(selectedCharacter.hasAttacked == true && selectedCharacter.hasMoved == true)
    {
        selectedCharacter = null;       
        return;
    }

    //GetComponent<AudioSource>().PlayOneShot(pop);
}
private bool IsEnemy(Character character)
{
    
    return character != null && character.isEnemy; // Replace IsEnemy with your actual property or logic
}


    private void NavigateToTile()
    {
        if (selectedCharacter == null)
            return;
        if(selectedCharacter.isAttacking == true)
        {
            return;
        }

        if (selectedCharacter.Moving == true || currentTile.CanBeReached == false)
            return;


        if(selectedCharacter.hasMoved == true)
        {
            return;
        }
        Path currentPath = pathfinder.PathBetween(currentTile, selectedCharacter.characterTile);

        if (Input.GetMouseButtonDown(0))
        {
            //GetComponent<AudioSource>().PlayOneShot(click);
            selectedCharacter.Move(currentPath);
            pathfinder.ResetPathfinder();
            //selectedCharacter = null;
        }
    }
}