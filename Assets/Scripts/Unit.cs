using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Unit : Entity
    {
    #region Enumerations
        public enum UnitType
        {
            NONE = 0,
            INFANTRY,
            VEHICLE,
            PLANE,
            HELICOPTER,
            BOAT,

            COUNT,
        }
    #endregion // Enumeration

    #region Attributes
        // Type of the unit
        protected UnitType m_unitType;

        // Ability of the unit to attack or not each EntityType
        protected Dictionary<EntityType, bool> m_canAttack;
        // Efficiency of the Unit against each UnitType
        protected Dictionary<UnitType, float> m_efficiency;

        // Movement range of the unit
        protected int m_movRange;
        // Attack range of of the unit
        protected int m_atkRange;
        // Vision range of the unit
        protected int m_visRange;
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Unit()
        {
        }
        // Constructor by value
        public Unit(string name = "", EntityType eType = 0, int cellId = 0, Sprite skin = null, UnitType uType = 0, int movR = 0, int atkR = 1, int visR = 1)
        {
            m_unitType = uType;

            m_canAttack.Add(EntityType.TERRESTRIAL, true);
            m_canAttack.Add(EntityType.AERIAL, false);
            m_canAttack.Add(EntityType.NAVAL, false);

            m_efficiency.Add(UnitType.INFANTRY, 1.00f);
            m_efficiency.Add(UnitType.VEHICLE, 1.00f);
            m_efficiency.Add(UnitType.PLANE, 0.00f);
            m_efficiency.Add(UnitType.HELICOPTER, 0.00f);
            m_efficiency.Add(UnitType.BOAT, 0.00f);

            m_movRange = movR;
            m_atkRange = atkR;
            m_visRange = visR;
        }
        // Constructor by copy
        public Unit(Unit model)
        {
            m_unitType = model.m_unitType;

            m_canAttack = model.m_canAttack;
            m_efficiency = model.m_efficiency;

            m_movRange = model.m_movRange;
            m_atkRange = model.m_atkRange;
            m_visRange = model.m_visRange;
        }
    #endregion // Constructors

    #region Accessors
        public UnitType GetUnitType()
        {
            return m_unitType;
        }
        public void SetUnitType(UnitType type)
        {
            m_unitType = type;
        }
        public Dictionary<EntityType, bool> CanAttack { get => m_canAttack; set => m_canAttack = value; }
        public Dictionary<UnitType, float> Efficiency { get => m_efficiency; set => m_efficiency = value; }
        public int MovRange { get => m_movRange; set => m_movRange = value; }
        public int AtkRange { get => m_atkRange; set => m_atkRange = value; }
        public int VisRange { get => m_visRange; set => m_visRange = value; }
    #endregion // Accessors

    #region Functions
    // Check if the Unit can attack this Entity
    public bool CanUnitAttack(Entity e)
    {
        if (e == null)
            return false;

        if (m_canAttack[e.GetEntityType()])
            return true;

        return false;
    }
    // Change the capabily of the Unit to attack or not a certain EntityType
    public bool ModifyAttackAbilities(EntityType type, bool canAttack)
    {
        if (m_canAttack.ContainsKey(type))
        {
            m_canAttack[type] = canAttack;
            return true;
        }

        return false;
    }

    // Get Unit efficiency against a certain UnitType
    public float GetUnitEfficiency(UnitType type)
    {
        if (m_efficiency.ContainsKey(type))
            return m_efficiency[type];
        
        return -1f;
    }
    // Change the efficiency of the Unit against a certain UnitType
    public bool UpdateUnitEfficiency(UnitType type, float eff)
    {
        if (m_efficiency.ContainsKey(type))
        {
            m_efficiency[type] = eff;
            return true;
        }

        return false;
    }
    
    // Handle Unit movement
    public void MoveTo(Cell target)
    {
        if (target == null)
            return;

        /* TODO : 
            Get Path to target
            Move the Unit to the target following the path
        */
    }
    // Handle Unit attacks
    public void Attack(Entity target)
    {
        if (target == null || !CanUnitAttack(target))
            return;

        /*TODO : 
            Check efficiency and terrain modifiers 
            Compute damages 
            Apply them
            If the opponent isn't dead it fights back
        */
    }
    public float ComputeDamages()
    {
        return 0.00f;
    }
    public float ApplyDamages()
    {
        return 0.00f;
    }
    public void FightBack(Entity e)
    {

    }

    #endregion // Functions
    } // Unit
}
