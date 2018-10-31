using UnityEngine;
using UnityEditor.ShaderGraph;
using System.Reflection;


[Title("Custom", "In Steps")]
public class InStepsNode : CodeFunctionNode
{
	public InStepsNode()
	{
		name = "In Steps Node";
	}

	protected override MethodInfo GetFunctionToConvert()
	{
		return GetType().GetMethod("InStepsFunction",
			BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string InStepsFunction(
	[Slot(0, Binding.None)] DynamicDimensionVector A,
	[Slot(1, Binding.None)] Vector1 B,
	[Slot(2, Binding.None)] out DynamicDimensionVector Out)
	{
		return
			@"{ Out = A - fmod(A, B); }";
	}
}
