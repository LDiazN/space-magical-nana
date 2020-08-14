using UnityEngine;

public class HealSkill : InstantSkill
{
    [SerializeField]
    [Min(1)]
    [Max(1)]
    private int _toHeal;

    private BaseHealthSystem _healthSystem;

    public override void Init(GameObject ship)
    {
        _healthSystem = ship.GetComponent<BaseHealthSystem>();

        if (_healthSystem == null)
            throw new System.Exception("Skill incompatible with the ship");
    }


    protected override void DoSkill()
    {
        _healthSystem.TakeHeal(_toHeal);
    }
}
