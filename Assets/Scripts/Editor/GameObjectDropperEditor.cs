using Components.Dropper;
using UnityEditor;

namespace Editor
{
    public class GameObjectDropperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Получаем ссылку на объект
            GameObjectDropper dropper = (GameObjectDropper)target;

            // Рисуем поле prefabs
            SerializedProperty prefabsProp = serializedObject.FindProperty("prefabs");
            EditorGUILayout.PropertyField(prefabsProp, true);

            // Если в массиве только один префаб — показываем gameObjectCountToDrop
            if (prefabsProp.arraySize == 1)
            {
                SerializedProperty countProp = serializedObject.FindProperty("gameObjectCountToDrop");
                EditorGUILayout.PropertyField(countProp);
            }
            else
            {
                EditorGUILayout.HelpBox("gameObjectCountToDrop скрыт — используется только при одном префабе",
                    MessageType.Info);
            }

            // Остальные поля
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spreadForce"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyOnFinish"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
