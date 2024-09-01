using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public IEnumerable<DamageUnit> DamageUnits { get; private set; }
    public Vector3 Source { get; private set; }
    public GameObject SourceObject { get; private set; }

    public Damage(IEnumerable<DamageUnit> damageUnits, Vector3 source, GameObject sourceObject)
	{
		DamageUnits = damageUnits;
		Source = source;
		SourceObject = sourceObject;
	}
}
