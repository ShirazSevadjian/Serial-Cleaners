using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CarFieldOfView))]

//Inspired by https://github.com/Comp3interactive/FieldOfView
public class CarFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        CarFieldOfView fieldOfView = (CarFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.radius);

        Vector3 angle1 = DirectionFromAngle(fieldOfView.transform.eulerAngles.y, -fieldOfView.angle / 2);
        Vector3 angle2 = DirectionFromAngle(fieldOfView.transform.eulerAngles.y, fieldOfView.angle / 2);

        Handles.color = Color.blue;
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + angle1 * fieldOfView.radius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + angle2 * fieldOfView.radius);

        //If we spot player
        if (fieldOfView.spottedPlayer)
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.player1.transform.position);
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.player2.transform.position);
        }

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
