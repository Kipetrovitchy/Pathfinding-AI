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
    #endregion // Attributes

    #region Constructors
        // Constructor by default
        // public Cell()
        // {
        //     m_id = m_lastId++;
        //     m_provData = new Province();

        //     m_adjacentCells = new Dictionary<Cell, float>();
        // }
        // Constructor by value
        public Cell(string name = "", Game.Terrain.Type type = Terrain.Type.Count)
        {
            if (type == Terrain.Type.Count)
            {
                int rand = (int)Random.Range(0, ((int)Terrain.Type.Count) - 1);
                type = (Terrain.Type)rand;
            }
            
            m_id = m_lastId++;
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
    #endregion

    #region Functions
        // Add a new adjacent Cell to this Cell
        public bool AddAdjacentCell(Cell cell, float dist)
        {
            // Check if is not trying to link to itself
            if (cell.Id != m_id)
            {
                m_adjacentCells.Add(cell, dist);
                cell.m_adjacentCells.Add(this, dist);
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
    #endregion
    } // Cell
}
