using System;
using System.Linq;
using System.Reflection;
using Code.Attributes;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(SerializedReferenceTypeChangeAttribute))]
    public class SerializedReferenceTypeChangePropertyDrawer : PropertyDrawerBase
    {
        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
                return GetPropertyHeight(property) + EditorGUIUtility.singleLineHeight + 5;
            return GetPropertyHeight(property);
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            var fieldTypename = property.type;
            fieldTypename = fieldTypename.Replace("managedReference<", "").Replace(">", "");
            var isEmpty = string.IsNullOrEmpty(fieldTypename);
            
            if (isEmpty) fieldTypename = label.text + ": null";
            else fieldTypename = label.text + ": " + fieldTypename;
            
            if (isEmpty)
            {
                var foldoutRect = rect;
                foldoutRect.height = EditorGUIUtility.singleLineHeight; 
                property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, new GUIContent(fieldTypename));
            }
            else
            {
                EditorGUI.PropertyField(rect, property, new GUIContent(fieldTypename), true);
            }

            if (property.isExpanded)
            {
                var buttonRect = rect;
                buttonRect.y += rect.height - EditorGUIUtility.singleLineHeight - 5;
                buttonRect.height = EditorGUIUtility.singleLineHeight;

                if (GUI.Button(buttonRect, new GUIContent(" Change type")))
                {
                    var menu = GetGenericMenu(property);
                    menu.ShowAsContext();
                }
            }

            EditorGUI.EndProperty();
        }

        private GenericMenu GetGenericMenu(SerializedProperty property)
        {
            var baseType = ((SerializedReferenceTypeChangeAttribute)attribute).ArrayType;
            var types = Assembly.GetAssembly(baseType).GetTypes()
                .Where(t => !t.IsAbstract && t.IsSubclassOf(baseType));
            var menu = new GenericMenu();

            foreach (var type in types)
                menu.AddItem(new GUIContent(type.Name), false, Validate, (property, type));
            return menu;
        }

        private void Validate(object typeObject)
        {
            var (property, type) = ((SerializedProperty, Type))typeObject;
            if (type == null) return;
            if (property.type == type.Name) return;
            SerializedPropertyExtensions.SetValue(property, Activator.CreateInstance(type));
        }
    }
}