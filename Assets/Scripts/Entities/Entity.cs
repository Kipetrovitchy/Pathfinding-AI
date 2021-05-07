using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour
    {
    #region Enumerations
        public enum EntityType
        {
            NONE = 0,
            TERRESTRIAL,
            AERIAL,
            NAVAL,

            COUNT,
        }
    #endregion // Enumerations
    
    #region Attributes
        protected static int m_lastId = 0;

        // Id of the Entity
        protected int m_id;
        // Name of the Entity
        protected string m_name;
        // Type of the Entity
        protected EntityType m_entityType;

        // Health points of the Entity
        protected float m_health;

        // Position of the Entity (id of the Cell)
        protected int m_cellId;

        // Sprite of the Entity
        protected Sprite m_skin;

    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Entity()
        {
            m_id = m_lastId++;
            m_name = "No Name";
            m_entityType = 0;

            m_health = 0;
            
            m_cellId = 0;

            m_skin = null;
        }
        // Constructor by value
        public Entity(string name = "", EntityType eType = 0, float health = 0, int cellId = 0, Sprite skin = null)
        {
            m_id = m_lastId++;
            m_name = name;
            m_entityType = eType;

            m_health = health;

            m_cellId = cellId;

            m_skin = skin;
        }
        // Constructor by copy
        public Entity(Entity model)
        {
            m_id = model.m_id;
            m_name = model.m_name;
            m_entityType = model.m_entityType;

            m_health = model.m_health;

            m_cellId = model.m_cellId;

            m_skin = model.m_skin;
        }
    #endregion

    #region Accessors
        // Properties
        public int Id { get => m_id; set => m_id = value; }
        public string Name { get => m_name; set => m_name = value; }
        public int CellId { get => m_cellId; set => m_cellId = value; }
        public Sprite Skin { get => m_skin; set => m_skin = value; }
        
        // Getters/Setters
        public EntityType GetEntityType()
        {
            return m_entityType;
        }
        public void SetMainType(EntityType type)
        {
            m_entityType = type;
        }
    #endregion

    #region Functions
    #endregion
    } // Entity
}
