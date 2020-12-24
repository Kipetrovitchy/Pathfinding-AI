using UnityEngine;

namespace Game
{
    public class Terrain
    {
        public enum Type
        {
            Desert,
            Forest,
            Hills,
            Jungle,
            Mountains,
            Plains,
            Sea,
            Swamp,
            Urban,

            Count,
        }
    #region Attributes
        private Type m_type;
        private float m_defMod;
        private float m_atkMod;
        private float m_movMod;

        private Material m_color;
        private Sprite m_typeSprite;
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Terrain()
        {
            Init(Type.Plains, 1.00f, 1.00f, 1.00f, "Plains");
        }
        // Constructor by value
        public Terrain(Type type)
        {
            switch(type)
            {
                case Type.Desert:    Init(type, 1.00f, 1.00f, 0.50f, "Desert");      break;
                case Type.Forest:    Init(type, 1.25f, 1.00f, 0.75f, "Forest");      break;
                case Type.Hills:     Init(type, 1.50f, 0.75f, 0.75f, "Hills");       break;
                case Type.Jungle:    Init(type, 1.75f, 0.75f, 0.50f, "Jungle");      break;
                case Type.Mountains: Init(type, 2.00f, 0.50f, 0.25f, "Mountains");   break;
                case Type.Plains:    Init(type, 1.00f, 1.00f, 1.00f, "Plains");      break;
                case Type.Sea:       Init(type, 1.00f, 1.00f, 1.00f, "Sea");         break;
                case Type.Swamp:     Init(type, 1.25f, 0.75f, 0.50f, "Swamp");       break;
                case Type.Urban:     Init(type, 1.25f, 0.75f, 1.00f, "Urban");       break;
            }
        }
        public Terrain(Type type, float def, float atk, float mov)
        {
            m_type = type;
            m_defMod = def;
            m_atkMod = atk;
            m_movMod = mov;
        }
        // Constructor by copy
        public Terrain(Terrain model)
        {
            m_type = model.m_type;
            m_defMod = model.m_atkMod;
            m_atkMod = model.m_defMod;
            m_movMod = model.m_movMod;
        }
    #endregion // Constructors

    #region Accessors
        public Type TerrainType { get => m_type; set => m_type = value; }
        public float DefMod { get => m_defMod; set => m_defMod = value; }
        public float AtkMod { get => m_atkMod; set => m_atkMod = value; }
        public float MovMod { get => m_movMod; set => m_movMod = value; }
        public Material Color { get => m_color; set => m_color = value; }
        public Sprite TypeSprite {get => m_typeSprite; set => m_typeSprite = value; }
    #endregion // Accessors

    #region Functions
        // Create a desert terrain
        private Terrain CreateDesert()
        {
            Terrain ter = new Terrain(Type.Desert, 1f, 1f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Desert");
            ter.m_color = Resources.Load<Material>("Materials/Desert");

            return ter;
        }
        // Create a forest terrain
        private Terrain CreateForest()
        {
            Terrain ter = new Terrain(Type.Forest, 1.25f, 1f, 0.75f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Forest");
            ter.m_color = Resources.Load<Material>("Materials/Forest");
            
            return ter;
        }
        // Create hills terrain
        private Terrain CreateHills()
        {
            Terrain ter = new Terrain(Type.Hills, 1.5f, 0.75f, 0.75f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Hills");
            ter.m_color = Resources.Load<Material>("Materials/Hills");

            return ter;
        }
        // Create a jungle terrain
        private Terrain CreateJungle()
        {
            Terrain ter = new Terrain(Type.Jungle, 1.75f, 0.75f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Jungle");
            ter.m_color = Resources.Load<Material>("Materials/Jungle");

            return ter;
        }
        // Create a mountain terrain
        private Terrain CreateMountains()
        {
            Terrain ter = new Terrain(Type.Mountains, 2f, 0.5f, 0.25f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Mountains");
            ter.m_color = Resources.Load<Material>("Materials/Mountains");

            return ter;
        }
        // Create plains terrain
        private Terrain CreatePlains()
        {
            Terrain ter = new Terrain(Type.Plains, 1f, 1f, 1f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Plains");
            ter.m_color = Resources.Load<Material>("Materials/Plains");

            return ter;
        }
        // Create a swamp terrain
        private Terrain CreateSwamp()
        {
            Terrain ter = new Terrain(Type.Swamp, 1.25f, 0.75f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Swamp");
            ter.m_color = Resources.Load<Material>("Materials/Swamp");

            return ter;
        }
        // Create a urban terrain
        private Terrain CreateUrban()
        {
            Terrain ter = new Terrain(Type.Urban, 1.25f, 0.75f, 1f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/Urban");
            ter.m_color = Resources.Load<Material>("Materials/Urban");

            return ter;
        }
        
        // Init
        private void Init(Type type, float def, float atk, float mov, string fileName)
        {
            m_type = type;

            InitModifiers(atk, def, mov);

            m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/" + fileName);
            m_color = Resources.Load<Material>("Materials/" + fileName);

            Debug.Log(m_color);
        }
        // Init combat modifiers
        private void InitModifiers(float def, float atk, float mov)
        {
            m_defMod = def;
            m_atkMod = atk;
            m_movMod = mov;
        }
    #endregion // Functions
    }
}