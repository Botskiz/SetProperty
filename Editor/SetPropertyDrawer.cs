// Copyright (c) 2014 Luminary LLC
// Copyright (c) 2016 Botskiz
// Licensed under The MIT License (See LICENSE for full text)
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(SetPropertyAttribute))]
public class SetPropertyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Rely on the default inspector GUI
		EditorGUI.BeginChangeCheck();
		EditorGUI.PropertyField(position, property, label, true);

		// Update only when necessary
		SetPropertyAttribute setProperty = attribute as SetPropertyAttribute;
		if ( EditorGUI.EndChangeCheck() ) {
			// When a SerializedProperty is modified the actual field does not have the current value set (i.e.  
			// FieldInfo.GetValue() will return the prior value that was set) until after this OnGUI call has completed. 
			// Therefore, we need to mark this property as dirty, so that it can be updated with a subsequent OnGUI event 
			// (e.g. Repaint)
			setProperty.IsDirty = true;
			// In arrays/lists, the property that has changed is not necessarily the one that causes the update OnGUI event.
			// Therefore, we will also store a reference to the changed property.
			setProperty.Property = property;
		}
		else if ( setProperty.IsDirty ) {

			// The user might have selected more than one object, so we need to make sure the property gets updated properly on all of these.
			foreach ( UnityEngine.Object obj in setProperty.Property.serializedObject.targetObjects ) {

				// The propertyPath may reference something that is a child field of a field on this Object, so it is necessary
				// to find which object is the actual parent before attempting to set the property with the current value.
				object parent = GetParentObjectOfProperty(setProperty.Property.propertyPath, obj);
				Type type = parent.GetType();
				PropertyInfo pi = type.GetProperty(setProperty.Name);

				Type propertyType = pi.PropertyType;
				Type fieldType = fieldInfo.FieldType;

				if ( pi == null ) {
					Debug.LogError("Invalid property name: " + setProperty.Name + " @" + setProperty.Property.propertyPath + "\nCheck your [SetProperty] attribute");
				}
				else if ( propertyType != fieldType ) {
					Debug.LogError("Invalid property type " + propertyType.ToString() + " @" + setProperty.Property.propertyPath + "\nProperty must be of same type as source Field type " + fieldType.ToString());
				}
				else {
					// Using FieldInfo instead of the SerializedProperty accessors causes our property to be updated incorrectly, unless it's the last element of a collection.
					// Therefore we need to deal with the SerializedProperty accessors here.
					object propValue = GetSerializedPropertyValue(setProperty.Property, propertyType);
					pi.SetValue(parent, propValue, null);
				}
			}
			setProperty.IsDirty = false;
			setProperty.Property = null;
		}
	}


	// Override GetPropertyHeight to get proper height values for default property drawers.
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property);
	}


	// Get information about the parent object of the target property.
	private object GetParentObjectOfProperty(string path, object obj) {

		string[] fields = path.Split('.');

		// We've finally arrived at the final object that contains the property
		if ( fields.Length == 1 ) {
			return obj;
		}

		// We may have to walk public or private fields along the chain to finding our container object, so we have to allow for both
		FieldInfo fi = obj.GetType().GetField(fields[0], (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
		obj = fi.GetValue(obj);

		// The target field may be inside a list or an array, so we check to see if our field is some kind of ILíst.
		if ( typeof(IList).IsAssignableFrom(fi.FieldType) ) {

			// The propertyPath of contains a data[index] field along the path.
			// We'll use this index to get to our desired parent object.
			int index = Int32.Parse(Regex.Match(fields[2], @"\d+").Value);

			// Cast to IList to address the proper element by its index.
			IList objList = (IList)obj;
			obj = objList[index];

			return GetParentObjectOfProperty(string.Join(".", fields, 3, fields.Length - 3), obj);
		}

		// Keep searching for our object that contains the property
		return GetParentObjectOfProperty(string.Join(".", fields, 1, fields.Length - 1), obj);
	}


	// Get the value object of a property object.
	private object GetSerializedPropertyValue(SerializedProperty serializedProperty, Type propertyType) {

		switch ( serializedProperty.propertyType ) {

			case SerializedPropertyType.AnimationCurve:
				return serializedProperty.animationCurveValue;

			case SerializedPropertyType.ArraySize:
				return serializedProperty.arraySize;

			case SerializedPropertyType.Boolean:
				return serializedProperty.boolValue;

			case SerializedPropertyType.Bounds:
				return serializedProperty.boundsValue;

			case SerializedPropertyType.Character:
				return (char)serializedProperty.intValue;

			case SerializedPropertyType.Color:
				return serializedProperty.colorValue;

			case SerializedPropertyType.Enum:
				return serializedProperty.enumValueIndex;

			case SerializedPropertyType.Float:
				if ( propertyType == typeof(double) ) {
					Debug.Log("Double");
					return serializedProperty.doubleValue;
				}
				return serializedProperty.floatValue;

			case SerializedPropertyType.Generic:
				break;

			case SerializedPropertyType.Gradient:
				break;

			case SerializedPropertyType.Integer:
				if ( propertyType == typeof(short)) {
					Debug.Log("Short");
					return (short)serializedProperty.intValue;
				}
				else if ( propertyType == typeof(long)) {
					Debug.Log("Long");
					return serializedProperty.longValue;
				}
				return serializedProperty.intValue;

			case SerializedPropertyType.LayerMask:
				break;

			case SerializedPropertyType.ObjectReference:
				return serializedProperty.objectReferenceValue;

			case SerializedPropertyType.Quaternion:
				return serializedProperty.quaternionValue;

			case SerializedPropertyType.Rect:
				return serializedProperty.rectValue;

			case SerializedPropertyType.String:
				return serializedProperty.stringValue;

			case SerializedPropertyType.Vector2:
				return serializedProperty.vector2Value;

			case SerializedPropertyType.Vector3:
				return serializedProperty.vector3Value;

			case SerializedPropertyType.Vector4:
				return serializedProperty.vector4Value;
		}

		throw new NotImplementedException("Unimplemented accessor for SerializedPropertyType." + serializedProperty.propertyType);
	}
}
