// Copyright (c) 2014 Luminary LLC
// Copyright (c) 2016 Botskiz
// Licensed under The MIT License (See LICENSE for full text)
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;

public class SetPropertyAttribute : PropertyAttribute {

	public string Name { get; private set; }
	public bool IsDirty { get; set; }
	public SerializedProperty Property { get; set; }

	public SetPropertyAttribute(string name) {
		this.Name = name;
	}
}
