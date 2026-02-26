using System.Collections.Generic;
using Creatures.CreaturesStateMachine.Player.Model.Definision.EditorHelper;
using UnityEditor;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Definision.Editor
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]
    public class InventoryIdAttributeDrawer : PropertyDrawer
    {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // поля id только для редактора
            var defs = DefFacade.Instance.Items.ItemsForEditor;
            
            var ids = new List<string>();

            foreach (var def in defs)
            {
                ids.Add(def.Id);
            }
            
            var index = Mathf.Max(ids.IndexOf(property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());
            property.stringValue = ids[index];
        }
    }
}