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

        public void SetupBoard()
        {
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

        public void PlayTurn(Player player, int rowIndex, int pitIndex)
        {
            Row selectedRow = Rows[rowIndex];
            Pits selectedPit = selectedRow.Pits[pitIndex];
            int seedsToDistribute = selectedPit.Seeds.Count;
            selectedPit.Seeds.RemoveAllSeeds();

            int currentRowIndex = rowIndex;
            int currentPitIndex = pitIndex;

            while (seedsToDistribute > 0)
            {
                currentPitIndex++; // Move to the next pit
                if (currentPitIndex >= selectedRow.Pits.Length)
                {
                    currentPitIndex = 0; // Reset to first pit if end of row is reached
                    currentRowIndex = (currentRowIndex + 1) % Rows.Length; // Switch row
                }

                Rows[currentRowIndex].Pits[currentPitIndex].Seeds.AddSeeds(1);
                seedsToDistribute--;
            }

            // Capture logic, if the last seed ends on opponent's row
            if (currentRowIndex != rowIndex && (Rows[currentRowIndex].Pits[currentPitIndex].Seeds.Count == 2 || Rows[currentRowIndex].Pits[currentPitIndex].Seeds.Count == 3))
            {
                CaptureSeeds(player, currentRowIndex, currentPitIndex);
            }

            DisplayBoard();
        }

        private void CaptureSeeds(Player player, int rowIndex, int pitIndex)
        {
            int seedsCaptured = 0;
            // Start capturing from the next pit where the last seed was placed
            pitIndex--;
            while (pitIndex >= 0 && (Rows[rowIndex].Pits[pitIndex].Seeds.Count == 2 || Rows[rowIndex].Pits[pitIndex].Seeds.Count == 3))
            {
                seedsCaptured += Rows[rowIndex].Pits[pitIndex].Seeds.Count;
                Rows[rowIndex].Pits[pitIndex].Seeds.RemoveAllSeeds();
                pitIndex--;
            }
            player.UpdateScore(seedsCaptured);
            Console.WriteLine($"{player.Gamertag} captures {seedsCaptured} seeds!");
        }

    }


}