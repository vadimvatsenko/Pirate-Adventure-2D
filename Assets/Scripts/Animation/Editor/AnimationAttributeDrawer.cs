using System.Collections.Generic;
using Animation.EditorHelpers;
using UnityEditor;
using UnityEngine;

// атрибуты для анимаций
namespace Animation.Editor
{
    [CustomPropertyDrawer(typeof(AnimationNameAttribute))]
    
    public class AnimationAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // хеши для доступной анимации
            var animationHash = AnimatorHashes.NameToHash;

            var animIds = new List<string>();

            foreach (var anim in animationHash)
            {
                animIds.Add(anim.Value);
            }
            
            var indexAnim = Mathf.Max(animIds.IndexOf(property.stringValue), 0);
            indexAnim = EditorGUI.Popup(position, property.displayName, indexAnim, animIds.ToArray());
            property.stringValue = animIds[indexAnim];
        }
    }
}