// Copyright (c) 2014 Luminary LLC
// Copyright (c) 2016 Botskiz
// Licensed under The MIT License (See LICENSE for full text)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetPropertyExample : MonoBehaviour {

	[System.Serializable]
	public class PropertyClass {

		[SerializeField, SetProperty("BoolValue")]
		private bool boolValue;
		public bool BoolValue {
			get {
				return boolValue;
			}
			set {
				boolValue = value;
				Debug.Log("BoolValue = " + boolValue.ToString());
			}
		}

		[SerializeField, SetProperty("CharValue")]
		private char charValue;
		public char CharValue {
			get {
				return charValue;
			}
			set {
				charValue = value;
				Debug.Log("CharValue = " + charValue.ToString());
			}
		}

		[SerializeField, SetProperty("ShortValue")]
		private short shortValue;
		public short ShortValue {
			get {
				return shortValue;
			}
			set {
				shortValue = value;
				Debug.Log("ShortValue = " + shortValue.ToString());
			}
		}

		[SerializeField, SetProperty("IntValue")]
		private int intValue;
		public int IntValue {
			get {
				return intValue;
			}
			set {
				intValue = value;
				Debug.Log("IntValue = " + intValue.ToString());
			}
		}

		[SerializeField, SetProperty("LongValue")]
		private long longValue;
		public long LongValue {
			get {
				return longValue;
			}
			set {
				longValue = value;
				Debug.Log("LongValue = " + longValue.ToString());
			}
		}

		[SerializeField, SetProperty("FloatValue")]
		private float floatValue;
		public float FloatValue {
			get {
				return floatValue;
			}
			set {
				floatValue = value;
				Debug.Log("FloatValue = " + floatValue.ToString());
			}
		}

		[SerializeField, SetProperty("DoubleValue")]
		private double doubleValue;
		public double DoubleValue {
			get {
				return doubleValue;
			}
			set {
				doubleValue = value;
				Debug.Log("DoubleValue = " + doubleValue.ToString());
			}
		}

		[SerializeField, SetProperty("StringValue")]
		private string stringValue;
		public string StringValue {
			get {
				return stringValue;
			}
			set {
				stringValue = value;
				Debug.Log("StringValue = " + stringValue.ToString());
			}
		}

		[SerializeField, SetProperty("AnimationCurveValue")]
		private AnimationCurve animationCurveValue;
		public AnimationCurve AnimationCurveValue {
			get {
				return animationCurveValue;
			}
			set {
				animationCurveValue = value;
				Debug.Log("AnimationCurveValue.length = " + animationCurveValue.length);
			}
		}

		[SerializeField, SetProperty("BoundsValue")]
		private Bounds boundsValue;
		public Bounds BoundsValue {
			get {
				return boundsValue;
			}
			set {
				boundsValue = value;
				Debug.Log("BoundsValue = " + boundsValue.ToString());
			}
		}

		[SerializeField, SetProperty("ColorValue")]
		private Color colorValue;
		public Color ColorValue {
			get {
				return colorValue;
			}
			set {
				colorValue = value;
				Debug.Log("ColorValue = " + colorValue.ToString());
			}
		}

		public enum EnumType {
			OptionOne,
			OptionTwo,
			OptionThree
		};

		[SerializeField, SetProperty("EnumValue")]
		private EnumType enumValue;
		public EnumType EnumValue {
			get {
				return enumValue;
			}
			set {
				enumValue = value;
				Debug.Log("EnumValue = " + enumValue.ToString());
			}
		}

		[SerializeField, SetProperty("ObjectValue")]
		private Object objectValue;
		public Object ObjectValue {
			get {
				return objectValue;
			}
			set {
				objectValue = value;
				Debug.Log("ObjectValue = " + objectValue.ToString());
			}
		}

		[SerializeField, SetProperty("QuaternionValue")]
		private Quaternion quaternionValue;
		public Quaternion QuaternionValue {
			get {
				return quaternionValue;
			}
			set {
				quaternionValue = value;
				Debug.Log("QuaternionValue = " + quaternionValue.ToString());
			}
		}

		[SerializeField, SetProperty("RectValue")]
		private Rect rectValue;
		public Rect RectValue {
			get {
				return rectValue;
			}
			set {
				rectValue = value;
				Debug.Log("RectValue = " + rectValue.ToString());
			}
		}

		[SerializeField, SetProperty("Vector2Value")]
		private Vector2 vector2Value;
		public Vector2 Vector2Value {
			get {
				return vector2Value;
			}
			set {
				vector2Value = value;
				Debug.Log("Vector2Value = " + vector2Value.ToString());
			}
		}

		[SerializeField, SetProperty("Vector3Value")]
		private Vector3 vector3Value;
		public Vector3 Vector3Value {
			get {
				return vector3Value;
			}
			set {
				vector3Value = value;
				Debug.Log("Vector3Value = " + vector3Value.ToString());
			}
		}

		[SerializeField, SetProperty("Vector4Value")]
		private Vector4 vector4Value;
		public Vector4 Vector4Value {
			get {
				return vector4Value;
			}
			set {
				vector4Value = value;
				Debug.Log("Vector4Value = " + vector4Value.ToString());
			}
		}
	}

	[SerializeField]
	private PropertyClass[] classAsArrayValue;

	[SerializeField]
	private List<PropertyClass> classAsListValue;

}
