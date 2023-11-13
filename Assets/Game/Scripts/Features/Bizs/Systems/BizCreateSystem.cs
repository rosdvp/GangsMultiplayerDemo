using System;
using System.Collections.Generic;
using Blah.Ecs;
using Blah.Systems;
using Game.Globals.Context;
using Game.Types;
using Object = UnityEngine.Object;

namespace Game.Features.Bizs
{
public class BizCreateSystem : IBlahInitSystem, IBlahRunSystem
{
	private BlahEcs _ecs;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context     = (IContextForSystems)initData;
		var configsBizs = context.Configs.Bizs;

		var assetMap = context.Assets.Map;

		for (var i = 0; i < assetMap.Bizs.Count; i++)
		{
			var asset  = assetMap.Bizs[i];
			var config = configsBizs.Find(c => c.Type == asset.Type);

			var     ent = _ecs.CreateEntity();
			ref var biz = ref ent.Add<BizComp>();
			biz.Id                    = i;
			biz.Type                  = config.Type;
			biz.OwnerPlayer           = asset.OwnerPlayer;
			biz.OwnerPlayerUnitsCount = asset.OwnerPlayer == 0 ? 5 : 0;

			var allowedRes = new HashSet<EResourceType>();
			if (biz.Type == EBizType.Mansion)
			{
				var allResTypes = (EResourceType[])Enum.GetValues(typeof(EResourceType));
				foreach (var resType in allResTypes)
					if (resType != EResourceType.Invalid)
						allowedRes.Add(resType);
			}
			else
			{
				foreach (var consRes in config.ConsumingRes)
					allowedRes.Add(consRes.Type);
				foreach (var prodRes in config.ProducingRes)
					allowedRes.Add(prodRes.Type);
			}
			biz.Storage = new ResourcesStorage(allowedRes);

			biz.Config = config;

			var go = Object.Instantiate(context.Assets.Common.PrefBiz, context.View.BizsHolder);
			go.transform.localPosition = asset.Pos;
			biz.View                   = go.GetComponent<ViewBiz>();
		}
	}

	public void Run() { }
}
}