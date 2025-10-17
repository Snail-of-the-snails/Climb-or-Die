using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    string objectName;
    public bool hitGround;
    public GameObject terrain;
    void Start()
    {
        objectName = gameObject.name;
    }
    // Update is called once per frame
    void Update()
    {
        terrain = FPSController.terrain;
        hitGround = FPSController.hitGround;
        /*if (objectName == "StartTerrain" )
        {
            Debug.Log(hitGround);
        }*/

        if (hitGround && (terrain.name == objectName))
        {
            GetComponent<TerrainCollider>().enabled = true;

        }
        else
        {
            GetComponent<TerrainCollider>().enabled = false;
        }
    }
}
