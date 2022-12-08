using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AddCharacterToCamera : MonoBehaviour
{
    CinemachineTargetGroup targetGroup;

    [SerializeField]
    Transform DefaultCameraTarget;

    private void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    public void AddCharacter(PlayerInput player)
    {
        StartCoroutine(WaitForCharacterLoaded(player)) ;
    }

    public IEnumerator WaitForCharacterLoaded(PlayerInput player)
    {
        PlayerInputHandler handler = player.GetComponent<PlayerInputHandler>();
        yield return new WaitUntil(() => handler.GetPlayer() != null);

        int defaultTargetIndex = targetGroup.FindMember(DefaultCameraTarget);
        if (defaultTargetIndex >= 0)
            targetGroup.RemoveMember(DefaultCameraTarget);
        targetGroup.AddMember(handler.GetPlayer().transform, 1f, 0f);
    }
}
