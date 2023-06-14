using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemySpawner script = (EnemySpawner)target;

        script.useVine = EditorGUILayout.Toggle("Use Vine?", script.useVine);

        if (script.useVine)
        {
            script.vinePoint = EditorGUILayout.ObjectField("Vine origin point", script.vinePoint, typeof(Transform), true) as Transform;
        }

        script.isFreezing = EditorGUILayout.Toggle("Is Freezing?", script.isFreezing);

        if (script.isFreezing)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Freeze Index");
            script.freezeIndex = EditorGUILayout.IntField(script.freezeIndex);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("How long to freeze");
            script.howLongToFreeze = EditorGUILayout.FloatField(script.howLongToFreeze);
            EditorGUILayout.EndHorizontal();
        }

        script.movementType = (MovementEnum)EditorGUILayout.EnumPopup("Movement Type", script.movementType);

        if (script.movementType == MovementEnum.Circle)
        {
            script.rotationCenter = EditorGUILayout.ObjectField("Rotation Center", script.rotationCenter, typeof(Transform), true) as Transform;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Rotation Radius");
            script.rotationRadius = EditorGUILayout.FloatField(script.rotationRadius);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Angular Speed");
            script.angularSpeed = EditorGUILayout.FloatField(script.angularSpeed);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("xDivider");
            script.xDivider = EditorGUILayout.FloatField(script.xDivider);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("yDivider");
            script.yDivider = EditorGUILayout.FloatField(script.yDivider);
            EditorGUILayout.EndHorizontal();
        }
    }
}
