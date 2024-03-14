using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2
{
    public class Board
    {
        public Row[] Rows { get; set; }

        public Board()
        {
            Rows = new Row[2]; // Assuming 2 rows for simplicity
            for (int i = 0; i < Rows.Length; i++)
            {
                Rows[i] = new Row();
            }
        }

        // Methods
        public void SetupBoard()
        {
            // Initialize board setup with 4 seeds in each pit
            foreach (var row in Rows)
            {
                foreach (var pit in row.Pits)
                {
                    pit.Seeds = new Seeds(4);
                }
            }
        }

        public void DisplayBoard()
        {
            // Logic to display the board's current state
            for (int i = 0; i < Rows.Length; i++)
            {
                Console.WriteLine($"Row {i + 1}: ");
                foreach (var pit in Rows[i].Pits)
                {
                    Console.Write($"{pit.Seeds.Count} ");
                }
                Console.WriteLine();
            }
        }

        // method to play a turn
        public void PlayTurn(Player player, int rowIndex, int pitIndex)
        {
            // Get the selected row and pit
            Row selectedRow = Rows[rowIndex];
            Pits selectedPit = selectedRow.Pits[pitIndex];

            // Distribute the seeds
            int seedsToDistribute = selectedPit.Seeds.Count;
            selectedPit.Seeds.RemoveAllSeeds();

            int currentRowIndex = rowIndex;
            int currentPitIndex = pitIndex;
            while (seedsToDistribute > 0)
            {
                // Move to the next pit
                currentPitIndex++;

                // Check if we need to switch rows
                if (currentPitIndex >= selectedRow.Pits.Length)
                {
                    currentPitIndex = 0;
                    currentRowIndex = (currentRowIndex + 1) % Rows.Length;
                }

                // Add a seed to the next pit
                Rows[currentRowIndex].Pits[currentPitIndex].Seeds.AddSeeds(1);
                seedsToDistribute--;

            }
            // Display the updated board state
            DisplayBoard();
        }
    }

}