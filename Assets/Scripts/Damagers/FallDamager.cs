using UnityEngine;

public class FallDamager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField, Min(0)] private float _minVelocityWithoutDamage = 10f;
    [SerializeField, Min(0)] private float _damageMultiplier = 1f;

    [Header("Components")]
    [SerializeField] private DamageProviderBase _damageProvider;
    [SerializeField] private CharacterGravityController _gravityController;

    private void OnEnable()
	{
		_gravityController.Landed += OnLanded;
	}

    private void OnDisable()
	{
		_gravityController.Landed -= OnLanded;
	}

    private void OnLanded(object _, LandEventArgs args)
	{
        if (args.Velocity >= -_minVelocityWithoutDamage)
            return;

        DamageUnit damageUnit = new()
        {
            Amount = (Mathf.Abs(args.Velocity) - _minVelocityWithoutDamage) * _damageMultiplier,
            Type = DamageType.Fall
        };

        Damage damage = new(new[] { damageUnit }, transform.position, gameObject);
        _damageProvider.Hit(damage);
	}
}
