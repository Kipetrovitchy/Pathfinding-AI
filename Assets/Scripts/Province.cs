using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Province
    {
    #region Attributes
        private string m_name;
        private Terrain m_terrain;
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Province()
        {
            m_name = "NoName";
            m_terrain = new Terrain();
        }
        // Constructor by value
        public Province(string name, Terrain.Type type)
        {
            m_name = name;
            m_terrain = new Terrain(type);
        }
        // Constructor by copy
        public Province(Province model)
        {
            m_name = model.m_name;
            m_terrain = model.m_terrain;
        }
    #endregion // Constructors

    #region Accessors
        public string Name { get => m_name; set => m_name = value; }
        public Terrain Terrain { get => m_terrain; set => m_terrain = value; }
    #endregion // Accessors

    #region Functions
    #endregion // Function
    }
}
