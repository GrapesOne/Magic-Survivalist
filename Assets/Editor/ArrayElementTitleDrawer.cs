using Code.Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor {

    [CustomPropertyDrawer(typeof(ArrayElementTitleAttribute))]
    public class ArrayElementTitleDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual ArrayElementTitleAttribute Atribute {
            get { return (ArrayElementTitleAttribute)attribute; }
        }

        SerializedProperty TitleNameProp;

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label) {
            string fullLabel = "";
            foreach (var varname in Atribute.Varname) {
                if (varname[0] == '$') {
                    fullLabel += varname[1..];
                }
                else {
                    string FullPathName = property.propertyPath + "." + varname;
                    TitleNameProp = property.serializedObject.FindProperty(FullPathName);
                    string newlabel = GetTitle();
                    fullLabel += newlabel + " | ";
                }
            }

            if (string.IsNullOrEmpty(fullLabel)) fullLabel = label.text;
            else fullLabel = fullLabel.Remove(fullLabel.Length - 3);
            EditorGUI.PropertyField(position, property, new GUIContent(fullLabel, label.tooltip), true);
        }

        private string GetTitle() {
            switch (TitleNameProp.propertyType) {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    return TitleNameProp.intValue.ToString();
                case SerializedPropertyType.Boolean:
                    return TitleNameProp.boolValue.ToString();
                case SerializedPropertyType.Float:
                    return TitleNameProp.floatValue.ToString();
                case SerializedPropertyType.String:
                    return TitleNameProp.stringValue;
                case SerializedPropertyType.Color:
                    return TitleNameProp.colorValue.ToString();
                case SerializedPropertyType.ObjectReference:
                    return TitleNameProp.objectReferenceValue != null ? TitleNameProp.objectReferenceValue.ToString() : "null";
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    return TitleNameProp.enumNames[TitleNameProp.enumValueIndex];
                case SerializedPropertyType.Vector2:
                    return TitleNameProp.vector2Value.ToString();
                case SerializedPropertyType.Vector3:
                    return TitleNameProp.vector3Value.ToString();
                case SerializedPropertyType.Vector4:
                    return TitleNameProp.vector4Value.ToString();
                case SerializedPropertyType.Rect:
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    break;
                case SerializedPropertyType.Bounds:
                    break;
                case SerializedPropertyType.Gradient:
                    break;
                case SerializedPropertyType.Quaternion:
                    break;
                default:
                    break;
            }

            return "";
        }
    }

}