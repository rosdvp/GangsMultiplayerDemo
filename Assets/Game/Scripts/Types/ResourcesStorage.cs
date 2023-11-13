using System;
using System.Collections.Generic;

namespace Game.Types
{
public class ResourcesStorage
{
	public readonly IReadOnlyList<EResourceType> Types;

	public ResourcesStorage(HashSet<EResourceType> allowedRes)
	{
		Types = new List<EResourceType>(allowedRes);
		
		foreach (var type in Types)
			_map[type] = new Resource(type);
	}
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private Dictionary<EResourceType, Resource> _map = new();

	public bool IsAllowed(EResourceType type) => _map.TryGetValue(type, out _);
	
	public Resource Get(EResourceType type) 
		=> _map.TryGetValue(type, out var res) ? res : throw Exc(type);

	public bool Has(Resource res)
		=> _map.TryGetValue(res.Type, out var currRes) && currRes.Amount >= res.Amount;
	
	public void Change(Resource delta)
	{
		if (_map.TryGetValue(delta.Type, out var currRes))
			_map[delta.Type] = currRes + delta;
		else
			throw Exc(delta.Type);
	}
	
	
	private Exception Exc(EResourceType type) => new($"{type} is not allowed");
}
}