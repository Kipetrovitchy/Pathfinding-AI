using System.Collections;
using System.Collections.Generic;
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

        private Sprite m_typeSprite;
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Terrain()
        {
            m_type = Type.Plains;
            InitModifiers(1f, 1f, 1f);
        }
        // Constructor by value
        public Terrain(Type type)
        {
            switch(type)
            {
                case Type.Desert:    Init(type, 1f, 1f, 0.5f, "ill_prov_desert");       break;
                case Type.Forest:    Init(type, 1.25f, 1f, 0.75f, "ill_prov_forest");   break;
                case Type.Hills:     Init(type, 1.5f, 0.75f, 0.75f, "ill_prov_hills");  break;
                case Type.Jungle:    Init(type, 1.75f, 0.75f, 0.5f, "ill_prov_jungle"); break;
                case Type.Mountains: Init(type, 2f, 0.5f, 0.25f, "ill_prov_mountains"); break;
                case Type.Plains:    Init(type, 1f, 1f, 1f, "ill_prov_plains");         break;
                case Type.Sea:       Init(type, 1f, 1f, 1f, "ill_prov_sea");            break;
                case Type.Swamp:     Init(type, 1.25f, 0.75f, 0.5f, "ill_prov_swamp");  break;
                case Type.Urban:     Init(type, 1.25f, 0.75f, 1f, "ill_prov_urban");    break;
            }
        }
        public Terrain(Type type, float atk, float def, float mov)
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
        public Type GetType() { return m_type; }
        public void SetType(Type type) { m_type = type; }
        public float DefMod { get => m_defMod; set => m_defMod = value; }
        public float AtkMod { get => m_atkMod; set => m_atkMod = value; }
        public float MovMod { get => m_movMod; set => m_movMod = value; }
        public Sprite TypeSprite {get => m_typeSprite; set => m_typeSprite = value; }
    #endregion // Accessors

    #region Functions
        // Create a desert terrain
        private Terrain CreateDesert()
        {
            Terrain ter = new Terrain(Type.Desert, 1f, 1f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_desert");

            return ter;
        }
        // Create a forest terrain
        private Terrain CreateForest()
        {
            Terrain ter = new Terrain(Type.Forest, 1.25f, 1f, 0.75f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_forest");
            
            return ter;
        }
        // Create hills terrain
        private Terrain CreateHills()
        {
            Terrain ter = new Terrain(Type.Hills, 1.5f, 0.75f, 0.75f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_hills");

            return ter;
        }
        // Create a jungle terrain
        private Terrain CreateJungle()
        {
            Terrain ter = new Terrain(Type.Jungle, 1.75f, 0.75f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_jungle");

            return ter;
        }
        // Create a mountain terrain
        private Terrain CreateMountains()
        {
            Terrain ter = new Terrain(Type.Mountains, 2f, 0.5f, 0.25f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_mountains");

            return ter;
        }
        // Create plains terrain
        private Terrain CreatePlains()
        {
            Terrain ter = new Terrain(Type.Plains, 1f, 1f, 1f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_plains");

            return ter;
        }
        // Create a swamp terrain
        private Terrain CreateSwamp()
        {
            Terrain ter = new Terrain(Type.Swamp, 1.25f, 0.75f, 0.5f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_swamp");

            return ter;
        }
        // Create a urban terrain
        private Terrain CreateUrban()
        {
            Terrain ter = new Terrain(Type.Urban, 1.25f, 0.75f, 1f);
            ter.m_typeSprite = Resources.Load<Sprite>("Sprites/Terrain/ill_prov_urban");

            return ter;
        }
        // Init
        private void Init(Type type, float atk, float def, float mov, string sprite)
        {
            m_type = type;

            InitModifiers(atk, def, mov);

            string path = "Sprites/Terrain/" + sprite;
            m_typeSprite = Resources.Load<Sprite>(path);
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