using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PathfinderAStar
    {
        [HideInInspector]
        public List<int> reachableCells = new List<int>();

        private List<int> m_processedCells = new List<int>();

        public void GetReachableCells(Cell start, int range)
        {
            if (start == null || range == 0)
                return;

            if (start.AdjacentCells.Count != 0)
            {
                foreach (KeyValuePair<Cell, float> pair in start.AdjacentCells)
                {
                    // Check if the Cell as already been processed
                    if (!m_processedCells.Contains(pair.Key.Id))
                    {
                        // If not add it to the list of processed cells
                        m_processedCells.Add(pair.Key.Id);
                        
                        // Check if the Cell is occupied
                        if (!pair.Key.IsOccupied())
                        {
                            // If not add it to the list of reachable cells
                            reachableCells.Add(pair.Key.Id);

                            // Check the adjacent cells too
                            GetReachableCells(pair.Key, range - 1);
                        }
                    }
                }
            }
        }

        public void ResetLists()
        {
            m_processedCells.Clear();
            reachableCells.Clear();
        }
    }
}
