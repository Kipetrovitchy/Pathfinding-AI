using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Cell
    {
    #region Attributes
        private static int m_lastId = 0;

        // Id of the Cell
        private int m_id;
        // Terrain type of the Cell
        private Province m_provData;

        /*
        * Cells adjacent to(accessible from) this Cell
        * <other Cell, distance between the two cells>
        */
        private Dictionary<Cell, float> m_adjacentCells;

        private GameObject m_object;
        private Entity m_occupant;
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        public Cell()
        {
            m_id = m_lastId++;
            
            int rand = (int)Random.Range(0, ((int)Terrain.Type.Count) - 1);
            Terrain.Type type = (Terrain.Type)rand;
            
            m_provData = new Province(m_id.ToString(), type);

            m_adjacentCells = new Dictionary<Cell, float>();
        }
        // Constructor by value
        public Cell(int id, string name, Terrain.Type type)
        {
            if (type == Terrain.Type.Count)
            {
                int rand = (int)Random.Range(0, ((int)Terrain.Type.Count) - 1);
                type = (Terrain.Type)rand;
            }
            
            if (id == -1 || id < m_lastId)
                m_id = m_lastId++;
            else
                m_id = id;

            if (name == "")
                name = m_id.ToString();
            m_provData = new Province(name, type);

            m_adjacentCells = new Dictionary<Cell, float>();
        }
        // Constructor by copy
        public Cell(Cell model)
        {
            m_id = model.m_id;
            m_provData = model.m_provData;

            m_adjacentCells = model.m_adjacentCells;
        }
    #endregion

    #region Accessors
        public int Id { get => m_id; set => m_id = value; }
        public Province ProvData { get => m_provData; set => m_provData = value; }
        public Dictionary<Cell, float> AdjacentCells { get => m_adjacentCells; set => m_adjacentCells = value; }
        public GameObject Object { get => m_object; set => m_object = value; }
        public Entity Occupant { get => m_occupant; set => m_occupant = value; }
        #endregion

        #region Functions
        // Return if the cell is occupied or not
        public bool IsOccupied()
        {
            return (m_occupant != null ? true : false);
        }

        // Add a new adjacent Cell to this Cell
        public bool AddAdjacentCell(Cell cell, float dist = 0)
        {
            // Check if is not trying to link to itself
            if (cell.Id != m_id)
            {
                if (dist == 0)
                    dist = (cell.ProvData.Terrain.MovMod + m_provData.Terrain.MovMod) / 2;

                if (!IsAdjacentTo(cell))
                {
                    m_adjacentCells.Add(cell, dist);
                    cell.m_adjacentCells.Add(this, dist);
                }
                return true;
            }

            return false;
        }

        // Remove a Cell from the dictionnary of adjacent Cells
        public bool RemoveAdjacentCell(Cell cell)
        {
            return m_adjacentCells.Remove(cell);
        }

        // Check if a Cell is adjacent to this Cell
        public bool IsAdjacentTo(Cell cell)
        {
            return m_adjacentCells.ContainsKey(cell);
        }

        // Get the distance between this Cell and an other if they are adjacent
        public float GetDistanceToAdjacent(Cell cell)
        {
            if (IsAdjacentTo(cell))
                return m_adjacentCells[cell];
            
            return -1f;
        }
    
        public void UpdateVisuals()
        {
            m_object.GetComponent<MeshRenderer>().material = m_provData.Terrain.Color;
        }
    #endregion
    } // Cell
}
