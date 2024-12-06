using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowPool", menuName = "FlowSystem/FlowPool")]
public class FlowPool : ScriptableObject
{
	[SerializeField] private List<FlowMember> flowMembers;
	private Dictionary<Type, FlowMember> flowMembersByType;
	private void ConvertToDict()
	{
		foreach (var item in flowMembers)
		{
			flowMembersByType.Add(item.GetType(), item);
		}
	}
	private void Awake()
	{
		if (flowMembersByType == null)
		{
			ConvertToDict();
		};
	}
}