using BlahEditor.Attributes;
using Game.Types;
using UnityEngine;

namespace Game.Configs
{
[CreateAssetMenu(menuName = "Configs/Biz")]
public class ConfigBiz : ScriptableObject
{
	public EBizType Type;

	public float GenIntervalSecs;

	[TableArray]
	public Resource[] ConsumingRes;
	[TableArray]
	public Resource[] ProducingRes;
}
}