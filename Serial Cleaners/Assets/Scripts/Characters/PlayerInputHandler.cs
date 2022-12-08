using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    List<GameObject> CharactersPrefabs = new List<GameObject>();

    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        Transform spawnPlane = GameObject.Find("SpawnPlane")?.transform;
        Vector3 bottomLeft = spawnPlane.TransformPoint(new Vector3(5, 0, 5));
        Vector3 topRight = spawnPlane.TransformPoint(new Vector3(-5, 0, -5));
        Vector3 spawnPos = new Vector3(Random.Range(bottomLeft.x, topRight.x), spawnPlane.position.y, Random.Range(bottomLeft.z, topRight.z));
        Vector3 spawnRot = new Vector3(0, 0, -1);

        // TODO: Instead of random, let player chose which character to play
        player = Instantiate(CharactersPrefabs[Random.Range(0, CharactersPrefabs.Count)], spawnPos, Quaternion.LookRotation(spawnRot, Vector3.up), transform).GetComponent<PlayerMovement>();

        SkinnedMeshRenderer playerMesh = player.transform.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] modelMaterials = playerMesh.materials;

        modelMaterials[1].color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 0.5f, 1, 1);

    }

    public void Move(InputAction.CallbackContext context)
    {
        if(player)
        {
            player.Move(context);
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (player)
        {
            player.Look(context);
        }
    }

    public void Submit(InputAction.CallbackContext context)
    {

    }

    public void Pickup(InputAction.CallbackContext context)
    {
        
    }

    public PlayerMovement GetPlayer()
    {
        return player;
    }
}
